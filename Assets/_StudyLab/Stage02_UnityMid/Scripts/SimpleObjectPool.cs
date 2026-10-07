using System.Collections.Generic;
using UnityEngine;

// S02_05 씬 전용: Queue 기반 초급 오브젝트 풀
public class SimpleObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int preloadCount = 10;

    // [S02-05-01] WHY: Queue는 선입선출, 꺼낸 순서대로 돌아와 편향 없이 재사용
    // List.RemoveAt(0)은 O(n), Dequeue는 O(1)
    // README [S02-05-01], DeepDive [S02-05-01] 참조
    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("[S02-05-01] bulletPrefab 미연결. Get 호출 전 연결하세요.");
            return;
        }
        for (int i = 0; i < preloadCount; i++)
        {
            pool.Enqueue(CreateNew());
        }
    }

    private GameObject CreateNew()
    {
        GameObject go = Instantiate(bulletPrefab, transform);
        go.SetActive(false);
        return go;
    }

    // [S02-05-02] WHY: Instantiate/Destroy 대신 SetActive 재사용, GC 스파이크 방지
    // 빈 풀에서는 새로 생성해 게임이 멈추지 않게 함
    // README [S02-05-02], DeepDive [S02-05-02] 참조
    public GameObject Get(Vector3 position)
    {
        if (pool.Count == 0 && bulletPrefab == null)
        {
            Debug.LogWarning("[S02-05-02] bulletPrefab 미연결.");
            return null;
        }
        GameObject go = pool.Count > 0 ? pool.Dequeue() : CreateNew();
        go.transform.position = position;
        go.SetActive(true);
        return go;
    }

    // [S02-05-03] WHY: 반환 시 비활성화+풀로 복귀, 파괴된 객체는 다시 담지 않음
    // null 체크 없이 Enqueue하면 Get에서 예외 발생
    public void Release(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        go.SetActive(false);
        pool.Enqueue(go);
    }
}
