using UnityEngine;
using System.Collections.Generic;

// S04_01 씬 전용: Stack<Vector3> 이동 되돌리기
public class Stack_Undo : MonoBehaviour
{
    [SerializeField] private float moveStep = 1f;
    [SerializeField] private int maxHistory = 50;

    // [S04-01-01] WHY: 되돌리기는 후입선출, Stack이 정석
    // List 직접 인덱스 관리보다 Pop() 한 줄이 실수 여지 제거
    // README [S04-01-01], DeepDive [S04-01-01] 참조
    private readonly Stack<Vector3> history = new Stack<Vector3>();

    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W)) dir = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S)) dir = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A)) dir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D)) dir = Vector3.right;

        if (dir != Vector3.zero)
            Move(dir * moveStep);

        if (Input.GetKeyDown(KeyCode.Z))
            Undo();
    }

    // [S04-01-02] WHY: 이동 전 위치를 Push, 무한 적재는 상한으로 차단
    // 상한 없는 Push는 메모리 누수, TrimExcess보다 개수 제한이 단순
    // README [S04-01-02], DeepDive [S04-01-02] 참조
    private void Move(Vector3 delta)
    {
        if (history.Count >= maxHistory)
        {
            Debug.LogWarning($"[S04-01-02] 히스토리 상한 {maxHistory} 도달, 이동 미기록");
            transform.position += delta;
            return;
        }
        history.Push(transform.position);
        transform.position += delta;
        Debug.Log($"[S04-01-01] 이동 -> {transform.position}, 기록={history.Count}");
    }

    [ContextMenu("Undo")]
    public void Undo()
    {
        if (history.Count == 0)
        {
            Debug.Log("[S04-01-01] 되돌릴 기록 없음");
            return;
        }
        transform.position = history.Pop();
        Debug.Log($"[S04-01-01] Undo -> {transform.position}, 남은 기록={history.Count}");
    }

    [ContextMenu("Clear History")]
    public void ClearHistory()
    {
        history.Clear();
        Debug.Log("[S04-01-02] 히스토리 초기화");
    }
}
