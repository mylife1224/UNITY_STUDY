using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// S05_05 씬 전용: 격자 A* (맨해튼 휴리스틱) + 유닛 이동 코루틴
public class AStar_Pathfinding : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 8;
    [SerializeField] private float cellSize = 1.1f;
    [SerializeField] private float moveDelay = 0.2f;

    private bool[,] wall;
    private Vector2Int start = Vector2Int.zero;
    private Vector2Int goal;
    private List<Vector2Int> path = new List<Vector2Int>();
    private Vector2Int unitPos;

    private class Node
    {
        public Vector2Int pos; public int g; public int h;
        public int F => g + h;
        public Node from;
    }

    private static readonly Vector2Int[] Dirs =
        { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    void Start()
    {
        goal = new Vector2Int(width - 1, height - 1);
        wall = new bool[width, height];
        var rng = new System.Random(7);
        for (int x = 2; x < width - 2; x++)
            for (int y = 0; y < height; y++)
                if (rng.NextDouble() < 0.25) wall[x, y] = true;
        wall[start.x, start.y] = false; wall[goal.x, goal.y] = false;

        path = FindPath(start, goal);
        Debug.Log($"[S05-05-02] A* 경로 {path.Count}칸 (막히면 0칸, 시드 변경 요망)");
        if (path.Count > 0) StartCoroutine(MoveUnit());
    }

    // [S05-05-01] WHY: 맨해튼 거리(|dx|+|dy|)는 4방향 이동의 정확한 하한
    // 과대추정하지 않으므로(admissible) 최단경로가 보장된다
    private int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private List<Vector2Int> FindPath(Vector2Int s, Vector2Int g)
    {
        // [S05-05-02] WHY: open 목록에서 F=g+h 최소 노드를 먼저 펼친다
        // BFS(전부 펼침)와 탐욕(휴리스틱만) 사이의 균형이 A* 핵심
        var open = new List<Node> { new Node { pos = s, g = 0, h = Heuristic(s, g) } };
        var best = new Dictionary<Vector2Int, int> { [s] = 0 };
        Node end = null;
        while (open.Count > 0)
        {
            open.Sort((a, b) => a.F.CompareTo(b.F));
            var cur = open[0]; open.RemoveAt(0);
            if (cur.pos == g) { end = cur; break; }
            foreach (var d in Dirs)
            {
                var np = cur.pos + d;
                if (np.x < 0 || np.y < 0 || np.x >= width || np.y >= height) continue;
                if (wall[np.x, np.y]) continue;
                int ng = cur.g + 1;
                if (best.TryGetValue(np, out int old) && old <= ng) continue;
                best[np] = ng;
                open.Add(new Node { pos = np, g = ng, h = Heuristic(np, g), from = cur });
            }
        }
        var result = new List<Vector2Int>();
        for (var n = end; n != null; n = n.from) result.Add(n.pos);
        result.Reverse();
        return result;
    }

    private IEnumerator MoveUnit()
    {
        // [S05-05-03] WHY: 경로(List)와 이동(코루틴)을 분리한다
        // 경로 재계산 없이 속도(moveDelay)만 바꾸면 유닛 속도가 조절됨
        foreach (var cell in path)
        {
            unitPos = cell;
            yield return new WaitForSeconds(moveDelay);
        }
        Debug.Log($"[S05-05-03] 유닛 도착: {unitPos}");
    }

    void OnDrawGizmos()
    {
        if (wall == null) return;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var p = new Vector2Int(x, y);
                Vector3 c = transform.position + new Vector3(x * cellSize, 0, y * cellSize);
                if (p == unitPos) Gizmos.color = Color.blue;
                else if (p == start) Gizmos.color = Color.green;
                else if (p == goal) Gizmos.color = Color.red;
                else if (path.Contains(p)) Gizmos.color = Color.yellow;
                else Gizmos.color = wall[x, y] ? Color.black : Color.gray;
                Gizmos.DrawCube(c, Vector3.one * cellSize * 0.9f);
            }
    }
}
