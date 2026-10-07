using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// S05_01 씬 전용: 정렬 시각화 (버블/삽입, 코루틴 스텝 + Gizmos 막대)
public class SortVisualizer : MonoBehaviour
{
    [SerializeField] private int count = 12;
    [SerializeField] private float stepDelay = 0.15f;
    [SerializeField] private bool useInsertion = false;

    private int[] values;
    private int cursorA = -1;
    private int cursorB = -1;
    private int swapCount;

    void Start()
    {
        ShuffleAndSort();
    }

    [ContextMenu("ShuffleAndSort")]
    public void ShuffleAndSort()
    {
        StopAllCoroutines();
        values = new int[count];
        for (int i = 0; i < count; i++) values[i] = i + 1;
        var rng = new System.Random();
        for (int i = 0; i < count; i++) Swap(i, rng.Next(0, count));
        swapCount = 0;
        // [S05-01-01] WHY: 코루틴+WaitForSeconds로 한 스텝씩 끊는다
        // Update 플래그 방식보다 순서(비교->교환->대기)가 눈에 보인다
        // README [S05-01-01], DeepDive [S05-01-01] 참조
        StartCoroutine(useInsertion ? InsertionRoutine() : BubbleRoutine());
    }

    private IEnumerator BubbleRoutine()
    {
        for (int i = 0; i < values.Length - 1; i++)
            for (int j = 0; j < values.Length - 1 - i; j++)
            {
                cursorA = j; cursorB = j + 1;
                if (values[j] > values[j + 1]) { Swap(j, j + 1); swapCount++; }
                yield return new WaitForSeconds(stepDelay);
            }
        cursorA = cursorB = -1;
        // [S05-01-02] WHY: 버블은 인접 교환이라 매 스텝 변화가 보인다
        // 삽입은 이미 정렬된 구간에선 비교만 하고 넘어간다 (로그로 확인)
        Debug.Log($"[S05-01-02] Bubble done: swaps={swapCount}");
    }

    private IEnumerator InsertionRoutine()
    {
        for (int i = 1; i < values.Length; i++)
        {
            int key = values[i], j = i - 1;
            cursorA = i; cursorB = j;
            yield return new WaitForSeconds(stepDelay);
            while (j >= 0 && values[j] > key)
            {
                values[j + 1] = values[j]; j--;
                cursorA = j + 1; swapCount++;
                yield return new WaitForSeconds(stepDelay);
            }
            values[j + 1] = key;
        }
        cursorA = cursorB = -1;
        Debug.Log($"[S05-01-02] Insertion done: shifts={swapCount}");
    }

    private void Swap(int a, int b)
    {
        int t = values[a]; values[a] = values[b]; values[b] = t;
    }

    void OnDrawGizmos()
    {
        if (values == null) return;
        // [S05-01-03] WHY: Gizmos는 Play 없이도 Scene뷰 확인 가능
        // 큐브 Instantiate 대신 scale 막대로 그리면 정리 작업이 없다
        for (int i = 0; i < values.Length; i++)
        {
            Gizmos.color = (i == cursorA || i == cursorB) ? Color.red : Color.cyan;
            Vector3 p = transform.position + new Vector3(i * 1.1f, values[i] * 0.5f, 0);
            Gizmos.DrawCube(p, new Vector3(0.9f, values[i], 0.9f));
        }
    }
}
