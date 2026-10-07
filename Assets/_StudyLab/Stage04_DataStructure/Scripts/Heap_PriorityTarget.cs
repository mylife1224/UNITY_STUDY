using UnityEngine;
using System.Collections.Generic;

// S04_06 씬 전용: 최소힙으로 가장 가까운 적 타겟팅
public class Heap_PriorityTarget : MonoBehaviour
{
    [SerializeField] private List<Transform> enemies = new List<Transform>();
    [SerializeField] private float scanInterval = 0.5f;
    private float timer;

    // [S04-06-01] WHY: 전수 Sort O(n log n) 대신 힙이 효율적
    // Enqueue/Dequeue O(log n), Dequeue가 항상 최소 우선순위 반환
    private readonly MinHeap heap = new MinHeap();
    public Transform CurrentTarget { get; private set; }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= scanInterval) { timer = 0f; Retarget(); }
    }

    // [S04-06-02] WHY: 우선순위=거리, 작은 값부터 꺼내니 1순위가 최단거리 적
    // 파괴된 적(null)은 꺼낼 때 버림, 스캔마다 재구축해 순서 갱신
    public void Retarget()
    {
        heap.Clear();
        foreach (var e in enemies)
        {
            if (e == null) continue;
            heap.Enqueue(e, Vector3.Distance(transform.position, e.position));
        }
        CurrentTarget = null;
        while (heap.Count > 0)
        {
            Transform cand = heap.Dequeue();
            if (cand != null) { CurrentTarget = cand; break; }
        }
        Debug.Log(CurrentTarget != null ? $"[S04-06-02] 타겟={CurrentTarget.name}" : "[S04-06-02] 타겟 없음");
    }

    [ContextMenu("Seed Dummy Enemies")]
    private void SeedDummyEnemies()
    {
        enemies.Clear();
        for (int i = 0; i < 4; i++)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = transform.position + new Vector3((i + 1) * 3f, 0f, i);
            go.name = $"Enemy_{i}";
            enemies.Add(go.transform);
        }
        Retarget();
    }
}

// [S04-06-01] 부연: 완전이진트리를 List에 저장한 최소힙 (sift-up/down)
// .NET PriorityQueue는 Unity 6000(.NET Standard 2.1) 미지원이라 직접 구현
public class MinHeap
{
    private readonly List<HeapNode> data = new List<HeapNode>();
    public int Count => data.Count;
    public void Clear() { data.Clear(); }
    public void Enqueue(Transform t, float prio)
    {
        data.Add(new HeapNode(t, prio));
        int c = data.Count - 1;
        while (c > 0 && data[(c - 1) / 2].priority > data[c].priority) { Swap((c - 1) / 2, c); c = (c - 1) / 2; }
    }
    public Transform Dequeue()
    {
        Transform top = data[0].target;
        data[0] = data[data.Count - 1];
        data.RemoveAt(data.Count - 1);
        int p = 0;
        while (true)
        {
            int m = p, l = p * 2 + 1, r = l + 1;
            if (l < data.Count && data[l].priority < data[m].priority) m = l;
            if (r < data.Count && data[r].priority < data[m].priority) m = r;
            if (m == p) break;
            Swap(p, m); p = m;
        }
        return top;
    }
    private void Swap(int a, int b) { HeapNode t = data[a]; data[a] = data[b]; data[b] = t; }
    private struct HeapNode
    {
        public Transform target; public float priority;
        public HeapNode(Transform t, float p) { target = t; priority = p; }
    }
}
