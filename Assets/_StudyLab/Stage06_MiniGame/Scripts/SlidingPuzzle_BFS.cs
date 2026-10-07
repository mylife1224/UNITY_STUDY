using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// S06_03 씬 전용: 3x3 슬라이딩 퍼즐 + BFS 힌트 (깊이 제한)
public class SlidingPuzzle_BFS : MonoBehaviour
{
    [SerializeField] private int shuffleMoves = 20;
    [SerializeField] private int maxDepth = 14;
    [SerializeField] private int maxNodes = 30000;

    private int[] board = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
    private const string Solved = "123456780";

    void Start()
    {
        Shuffle();
        Print("섞은 뒤");
        string hint = GetHint();
        Debug.Log($"[S06-03-02] 힌트: {hint} (빈칸 이동 방향, maxDepth={maxDepth})");
    }

    private int Blank() { return Array.IndexOf(board, 0); }

    private IEnumerable<string> Moves(int blank)
    {
        int r = blank / 3, c = blank % 3;
        if (r > 0) yield return "Up";
        if (r < 2) yield return "Down";
        if (c > 0) yield return "Left";
        if (c < 2) yield return "Right";
    }

    [ContextMenu("Shuffle")]
    public void Shuffle()
    {
        var rng = new System.Random();
        board = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        // [S06-03-03] WHY: 랜덤 배치가 아니라 정답에서 유효 이동만 섞는다
        // 3x3 중 절반은 풀 수 없는 배치 — 무작정 섞으면 불가 판정 필요
        for (int i = 0; i < shuffleMoves; i++)
        {
            var opts = new List<string>(Moves(Blank()));
            ApplyMove(board, opts[rng.Next(opts.Count)]);
        }
    }

    private static void ApplyMove(int[] b, string move)
    {
        int bl = Array.IndexOf(b, 0), t = bl;
        if (move == "Up") t = bl - 3; else if (move == "Down") t = bl + 3;
        else if (move == "Left") t = bl - 1; else if (move == "Right") t = bl + 1;
        b[bl] = b[t]; b[t] = 0;
    }

    public string GetHint()
    {
        // [S06-03-01] WHY: 보드 상태를 문자열 키+HashSet 방문으로 BFS
        // 9! = 362880 전체 탐색 대신 깊이/노드 제한으로 힌트 1수만 계산
        string startKey = string.Concat(board.Select(v => v.ToString()).ToArray());
        var q = new Queue<(string key, string first, int depth)>();
        var visited = new HashSet<string> { startKey };
        q.Enqueue((startKey, null, 0));
        int nodes = 0;
        while (q.Count > 0)
        {
            var (key, first, depth) = q.Dequeue();
            if (key == Solved) return first ?? "이미 정답";
            // [S06-03-02] WHY: 깊이/노드 상한으로 프레임 프리즈 방지
            // 제한 초과면 "너무 멀다" 반환 — 모바일 저사양 대응
            if (depth >= maxDepth || ++nodes > maxNodes)
                return nodes > maxNodes ? "탐색 상한 초과 (셔플을 줄이세요)" : "해답이 깊음 (여러 수 필요)";
            int[] b = key.Select(ch => ch - '0').ToArray();
            int bl = Array.IndexOf(b, 0);
            foreach (var m in Moves(bl))
            {
                var nb = (int[])b.Clone(); ApplyMove(nb, m);
                string nk = string.Concat(nb.Select(v => v.ToString()).ToArray());
                if (visited.Add(nk)) q.Enqueue((nk, first ?? m, depth + 1));
            }
        }
        return "해답 없음";
    }

    private void Print(string title)
    {
        string rows = "";
        for (int r = 0; r < 3; r++) rows += $"\n{board[r * 3]} {board[r * 3 + 1]} {board[r * 3 + 2]}";
        Debug.Log($"[S06-03-03] [{title}]{rows}");
    }
}
