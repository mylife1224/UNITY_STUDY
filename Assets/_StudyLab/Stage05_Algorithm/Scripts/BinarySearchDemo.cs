using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// S05_03 씬 전용: UpDown 숫자맞히기 이진탐색 데모
public class BinarySearchDemo : MonoBehaviour
{
    [SerializeField] private int min = 1;
    [SerializeField] private int max = 100;
    [SerializeField] private int secret = 73;

    private int[] table;
    private int steps;

    void Start()
    {
        table = Enumerable.Range(min, max - min + 1).ToArray();
        // [S05-03-03] WHY: 이진탐색 전제조건은 정렬된 배열
        // 정렬 안 된 데이터에 쓰면 정답을 놓친다 (선형탐색으로 대체)
        Array.Sort(table);
        Debug.Log($"[S05-03-03] secret={secret}, 범위 [{min},{max}] 자동 탐색 시작");
        StartCoroutine(AutoSearch());
    }

    private IEnumerator AutoSearch()
    {
        int lo = 0, hi = table.Length - 1;
        steps = 0;
        while (lo <= hi)
        {
            // [S05-03-01] WHY: mid=(lo+hi)/2 로 절반씩 버린다
            // Up/Down 응답으로 탐색 구간을 절반으로 좁히는 것이 이진탐색 본질
            int mid = (lo + hi) / 2;
            steps++;
            Debug.Log($"[S05-03-01] step{steps}: mid={table[mid]} (구간 {table[lo]}~{table[hi]})");
            yield return new WaitForSeconds(0.4f);
            if (table[mid] == secret) break;
            if (table[mid] < secret) lo = mid + 1;
            else hi = mid - 1;
        }
        // [S05-03-02] WHY: 100개면 최대 7번(2^7=128) 만에 정답
        // 선형탐색 최악 100번 대비 O(log n) — Console 스텝 수로 직접 확인
        Debug.Log($"[S05-03-02] 탐색 종료: {steps}회 (선형 최악 {table.Length}회)");
    }

    // 버튼/키 입력용 수동 추측: UpDown 게임으로 직접 플레이 가능
    public void Guess(int value)
    {
        if (value == secret) Debug.Log($"[S05-03-01] 정답! {value}");
        else if (value < secret) Debug.Log($"[S05-03-01] {value} -> UP");
        else Debug.Log($"[S05-03-01] {value} -> DOWN");
    }

    void OnDrawGizmos()
    {
        if (table == null) return;
        for (int i = 0; i < table.Length; i += 5)
        {
            Gizmos.color = table[i] == secret ? Color.red : Color.gray;
            Vector3 p = transform.position + new Vector3(i * 0.3f, 0, 0);
            Gizmos.DrawCube(p, new Vector3(0.25f, 1f, 0.25f));
        }
    }
}
