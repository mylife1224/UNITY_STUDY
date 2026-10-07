using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// S05_04 씬 전용: 피보나치 재귀 vs 메모이제이션(DP) 비교
public class DP_Fibonacci : MonoBehaviour
{
    [SerializeField] private int n = 20;

    private int naiveCalls;
    private int memoCalls;
    private Dictionary<int, long> memo = new Dictionary<int, long>();

    void Start()
    {
        float t0 = Time.realtimeSinceStartup;
        naiveCalls = 0;
        long naive = FibNaive(n);
        float naiveTime = Time.realtimeSinceStartup - t0;

        t0 = Time.realtimeSinceStartup;
        memoCalls = 0; memo.Clear();
        long fast = FibMemo(n);
        float memoTime = Time.realtimeSinceStartup - t0;

        Debug.Log($"[S05-04-01] Fib({n})={naive}, 호출 {naiveCalls}회, {naiveTime * 1000f:F1}ms");
        Debug.Log($"[S05-04-02] Fib({n})={fast}, 호출 {memoCalls}회, {memoTime * 1000f:F1}ms");
        Debug.Log($"[S05-04-03] n을 25/30으로 키워보면 격차가 기하급수적으로 벌어짐");
    }

    private long FibNaive(int k)
    {
        naiveCalls++;
        // [S05-04-01] WHY: 순수 재귀는 같은 값을 반복 계산한다 (O(2^n))
        // Fib(5)도 Fib(3)을 2번 구함 — 호출 횟수 로그로 중복을 확인
        if (k <= 1) return k;
        return FibNaive(k - 1) + FibNaive(k - 2);
    }

    private long FibMemo(int k)
    {
        memoCalls++;
        // [S05-04-02] WHY: Dictionary에 계산 결과를 저장해 재사용 O(n)
        // 같은 k는 두 번 계산하지 않음 — 호출 횟수 비교가 핵심
        if (k <= 1) return k;
        if (memo.TryGetValue(k, out long cached)) return cached;
        long v = FibMemo(k - 1) + FibMemo(k - 2);
        memo[k] = v;
        return v;
    }

    void OnDrawGizmos()
    {
        // [S05-04-03] WHY: 호출 횟수를 막대 높이로 그리면 체감 차이가 보인다
        // 빨강=순수재귀, 초록=메모이제이션 (로그 스케일 아님, n=20 기준)
        if (naiveCalls > 0)
        {
            Gizmos.color = Color.red;
            Vector3 p = transform.position;
            Gizmos.DrawCube(p, new Vector3(1f, Mathf.Log(naiveCalls), 1f));
            Gizmos.color = Color.green;
            Gizmos.DrawCube(p + new Vector3(2f, 0, 0), new Vector3(1f, Mathf.Log(memoCalls + 1), 1f));
        }
    }
}
