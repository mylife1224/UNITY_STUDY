using System.Collections.Generic;
using UnityEngine;

// S01_05 씬 전용: 프리팹 Instantiate/Destroy + null 주의 예제
public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxAlive = 10;

    private float timer;
    private readonly List<GameObject> aliveList = new List<GameObject>();

    // [S01-05-01] WHY: 프리팹 미지정 시 Instantiate에서 예외, 사전 null 체크 필수
    // 인스펙터 미할당이 초급 1순위 에러 원인
    // README [S01-05-01], DeepDive [S01-05-01] 참조
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < spawnInterval)
        {
            return;
        }
        timer = 0f;

        if (enemyPrefab == null)
        {
            Debug.LogWarning("[S01-05-01] enemyPrefab이 비어 있음. 인스펙터에 연결하세요.");
            return;
        }
        Spawn();
    }

    private void Spawn()
    {
        // [S01-05-02] WHY: 파괴된 참조는 null 취급, 리스트에서 먼저 정리
        // Destroy 후 변수 재사용 시 MissingReference 예외 유발
        for (int i = aliveList.Count - 1; i >= 0; i--)
        {
            if (aliveList[i] == null)
            {
                aliveList.RemoveAt(i);
            }
        }

        // [S01-05-03] WHY: Find 계열 대신 리스트 개수로 상한 관리, 성능 안전
        // 개수 초과 시 가장 오래된 것부터 파괴
        if (aliveList.Count >= maxAlive)
        {
            Destroy(aliveList[0]);
            aliveList.RemoveAt(0);
        }
        Vector3 pos = transform.position + new Vector3(Random.Range(-3f, 3f), 0f, 0f);
        aliveList.Add(Instantiate(enemyPrefab, pos, Quaternion.identity));
    }
}
