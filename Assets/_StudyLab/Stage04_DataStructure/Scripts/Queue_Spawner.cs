using System.Collections.Generic;
using UnityEngine;

// S04_02 씬 전용: Queue 웨이브 스폰
public class Queue_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 1f;

    // [S04-02-01] WHY: List 대신 Queue를 쓴다 - 선입선출 웨이브에 적합
    // RemoveAt(0)은 O(n) 이동 발생, Dequeue()는 O(1)
    // README [S04-02-01], DeepDive [S04-02-01] 참조
    private Queue<string> waveQueue = new Queue<string>();

    // [S04-02-02] NOTE: 중간 취소/우선순위 필요시 Queue 불가
    // 중간 삭제 필요 -> List, 우선순위 필요 -> PriorityQueue로 교체
    private float timer;

    void Start()
    {
        waveQueue.Enqueue("Wave1_Slime x3");
        waveQueue.Enqueue("Wave2_Slime x5");
        waveQueue.Enqueue("Wave3_Boss x1");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && waveQueue.Count > 0)
        {
            timer = 0f;
            // [S04-02-01] WHY: Dequeue는 head 이동만, 데이터 복사 없음
            string wave = waveQueue.Dequeue();
            Debug.Log($"[S04-02-01] Spawn {wave}, 남은 웨이브={waveQueue.Count}");
            // 실제 스폰: Instantiate(enemyPrefab, ...);
        }
    }
}
