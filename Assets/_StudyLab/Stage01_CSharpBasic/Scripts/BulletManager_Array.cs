using System.Collections.Generic;
using UnityEngine;

// S01_02 씬 전용: 배열 vs List 총알 관리 비교
public class BulletManager_Array : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxBullets = 20;
    [SerializeField] private float fireInterval = 0.3f;

    // [S01-02-01] WHY: 고정 배열은 크기 고정, 꽉 차면 가장 오래된 자리 재사용
    // 추가/삭제 없음, 인덱스 순환이라 GC 없음
    // README [S01-02-01], DeepDive [S01-02-01] 참조
    private GameObject[] bulletArray;
    private int arrayCursor;

    // [S01-02-02] WHY: List는 가변 개수, 빈 자리 없이 늘고 줄기 쉬움
    // RemoveAt 호출 시 뒤 원소 이동 비용 있음
    // README [S01-02-02], DeepDive [S01-02-02] 참조
    private List<GameObject> bulletList = new List<GameObject>();

    private float timer;

    void Awake()
    {
        bulletArray = new GameObject[maxBullets];
        arrayCursor = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < fireInterval)
        {
            return;
        }
        timer = 0f;

        if (bulletPrefab == null)
        {
            Debug.LogWarning("[S01-02-01] bulletPrefab 미연결. 인스펙터에 연결하세요.");
            return;
        }
        FireArray();
        FireList();
    }

    private void FireArray()
    {
        // [S01-02-03] WHY: null/파괴 체크 후 재사용, 배열이라 Count 개념 없음
        // Destroy된 자리는 null이므로 커서 위치에 덮어쓰기
        GameObject old = bulletArray[arrayCursor];
        if (old != null)
        {
            Destroy(old);
        }
        bulletArray[arrayCursor] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        arrayCursor = (arrayCursor + 1) % maxBullets;
    }

    private void FireList()
    {
        for (int i = bulletList.Count - 1; i >= 0; i--)
        {
            if (bulletList[i] == null)
            {
                bulletList.RemoveAt(i);
            }
        }
        if (bulletList.Count < maxBullets)
        {
            bulletList.Add(Instantiate(bulletPrefab, transform.position, Quaternion.identity));
        }
    }
}
