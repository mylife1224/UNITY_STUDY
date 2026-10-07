using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// S06_01 씬 전용: 타워디펜스 통합 데모 (Queue 웨이브 + 우선순위 타겟팅 + 풀링)
public class TowerDefense_Main : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 0.8f;
    [SerializeField] private float towerRange = 6f;
    [SerializeField] private int towerDamage = 2;

    private class Enemy
    {
        public GameObject go; public int hp; public float dist; public bool active;
    }

    private Queue<string> waveQueue = new Queue<string>();
    private List<Enemy> pool = new List<Enemy>();
    private List<Enemy> actives = new List<Enemy>();
    private System.Random rng = new System.Random();

    void Start()
    {
        waveQueue.Enqueue("Wave1 x3"); waveQueue.Enqueue("Wave2 x5"); waveQueue.Enqueue("Boss x1");
        if (enemyPrefab == null)
        {
            enemyPrefab = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            enemyPrefab.SetActive(false);
        }
        StartCoroutine(WaveRoutine());
        StartCoroutine(TowerRoutine());
    }

    private Enemy GetFromPool()
    {
        // [S06-01-03] WHY: Instantiate는 GC/로딩 spikes 유발, 풀에서 재사용
        // 비활성 객체를 찾아 hp/dist만 초기화 — Destroy/생성 반복 없음
        foreach (var e in pool)
            if (!e.active) { e.active = true; return e; }
        var go = Instantiate(enemyPrefab, transform);
        var fresh = new Enemy { go = go, active = true };
        pool.Add(fresh);
        return fresh;
    }

    private IEnumerator WaveRoutine()
    {
        // [S06-01-01] WHY: 웨이브는 순서 보장 FIFO -> Queue
        // Dequeue O(1), 웨이브 스킵/삽입이 필요해지면 List로 교체
        while (waveQueue.Count > 0)
        {
            string wave = waveQueue.Dequeue();
            int n = wave.Contains("x5") ? 5 : wave.Contains("Boss") ? 1 : 3;
            Debug.Log($"[S06-01-01] {wave} 출격 (남은 웨이브 {waveQueue.Count})");
            for (int i = 0; i < n; i++)
            {
                var e = GetFromPool();
                e.hp = wave.Contains("Boss") ? 20 : 5;
                e.dist = 10f + rng.Next(0, 5);
                e.go.SetActive(true);
                e.go.transform.position = transform.position + new Vector3(rng.Next(-3, 4), 0, 8);
                actives.Add(e);
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator TowerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            // [S06-01-02] WHY: 타겟은 우선순위(거리 최소) — PriorityQueue 개념
            // 소규모라 OrderBy/선형탐색이면 충분, 수백기면 힙 구조로 교체
            var target = actives.Where(e => e.active && e.dist <= towerRange)
                .OrderBy(e => e.dist).FirstOrDefault();
            if (target == null) continue;
            target.hp -= towerDamage;
            target.dist -= 1f;
            if (target.hp <= 0)
            {
                target.active = false; target.go.SetActive(false); actives.Remove(target);
                Debug.Log($"[S06-01-02] 처치! 남은 활성 {actives.Count}, 풀 크기 {pool.Count}");
            }
        }
    }
}
