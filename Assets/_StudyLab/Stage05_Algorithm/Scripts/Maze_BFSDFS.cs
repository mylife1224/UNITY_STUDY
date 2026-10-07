using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// S05_02 씬 전용: 격자 미로 BFS/DFS 경로 색칠 (Queue/Stack 사용)
public class Maze_BFSDFS : MonoBehaviour
{
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 6;
    [SerializeField] private float cellSize = 1.1f;
    [SerializeField, Range(0f, 0.3f)] private float wallRatio = 0.2f;

    private bool[,] wall;
    private List<Vector2Int> bfsPath = new List<Vector2Int>();
    private List<Vector2Int> dfsPath = new List<Vector2Int>();
    private HashSet<Vector2Int> bfsVisited = new HashSet<Vector2Int>();
    private Vector2Int start = Vector2Int.zero;
    private Vector2Int goal;

    private static readonly Vector2Int[] Dirs =
        { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    void Start()
    {
        BuildMaze();
        RunBFS();
        RunDFS();
        Debug.Log($"[S05-02-01] BFS 방문={bfsVisited.Count} 경로길이={bfsPath.Count}");
        Debug.Log($"[S05-02-02] DFS 경로길이={dfsPath.Count} (BFS보다 길거나 같음)");
    }

    private void BuildMaze()
    {
        goal = new Vector2Int(width - 1, height - 1);
        wall = new bool[width, height];
        var rng = new System.Random(42);
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                wall[x, y] = rng.NextDouble() < wallRatio;
        wall[start.x, start.y] = false;
        wall[goal.x, goal.y] = false;
    }

    private bool InBounds(Vector2Int p) => p.x >= 0 && p.y >= 0 && p.x < width && p.y < height && !wall[p.x, p.y];

    private void RunBFS()
    {
        // [S05-02-01] WHY: BFS는 Queue(FIFO)라 시작점에서 가까운 순으로 퍼진다
        // 먼저 도착한 경로가 최단거리 보장 -> 미로 최단경로에 적합
        var q = new Queue<Vector2Int>();
        // [S05-02-03] WHY: 방문 집합은 HashSet O(1) 조회
        // List.Contains는 O(n)이라 격자가 커지면 BFS가 느려진다
        var prev = new Dictionary<Vector2Int, Vector2Int>();
        q.Enqueue(start); bfsVisited.Add(start);
        while (q.Count > 0)
        {
            var cur = q.Dequeue();
            if (cur == goal) break;
            foreach (var d in Dirs)
            {
                var next = cur + d;
                if (InBounds(next) && !bfsVisited.Contains(next))
                {
                    bfsVisited.Add(next); prev[next] = cur; q.Enqueue(next);
                }
            }
        }
        bfsPath = TracePath(prev, goal);
    }

    private void RunDFS()
    {
        // [S05-02-02] WHY: DFS는 Stack(LIFO)라 한 방향으로 끝까지 파고든다
        // 최단 보장 없음, 미로 생성/막다른 탐색처럼 깊이 우선이 필요할 때 사용
        var st = new Stack<Vector2Int>();
        var prev = new Dictionary<Vector2Int, Vector2Int>();
        var visited = new HashSet<Vector2Int>();
        st.Push(start); visited.Add(start);
        while (st.Count > 0)
        {
            var cur = st.Pop();
            if (cur == goal) break;
            foreach (var d in Dirs)
            {
                var next = cur + d;
                if (InBounds(next) && !visited.Contains(next))
                {
                    visited.Add(next); prev[next] = cur; st.Push(next);
                }
            }
        }
        dfsPath = TracePath(prev, goal);
    }

    private List<Vector2Int> TracePath(Dictionary<Vector2Int, Vector2Int> prev, Vector2Int end)
    {
        var path = new List<Vector2Int>();
        if (end != start && !prev.ContainsKey(end)) return path;
        for (var p = end; p != start; p = prev[p]) path.Add(p);
        path.Add(start); path.Reverse();
        return path;
    }

    void OnDrawGizmos()
    {
        if (wall == null) return;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var p = new Vector2Int(x, y);
                Vector3 c = transform.position + new Vector3(x * cellSize, 0, y * cellSize);
                if (p == start) Gizmos.color = Color.green;
                else if (p == goal) Gizmos.color = Color.red;
                else if (dfsPath.Contains(p)) Gizmos.color = Color.yellow;
                else if (bfsVisited.Contains(p)) Gizmos.color = Color.cyan;
                else Gizmos.color = wall[x, y] ? Color.black : Color.gray;
                Gizmos.DrawCube(c, Vector3.one * cellSize * 0.9f);
            }
    }
}
