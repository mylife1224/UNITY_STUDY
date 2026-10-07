using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// S03_02 씬 전용: LINQ/람다로 적 필터링
public class EnemyFilter_LINQ : MonoBehaviour
{
    [SerializeField] private List<FilterEnemyData> enemies = new List<FilterEnemyData>();
    [SerializeField] private float aggroRange = 10f;
    [SerializeField] private int minHp = 1;

    // [S03-02-01] WHY: Where+OrderBy 선언형 필터는 의도가 코드에 드러난다
    // for+if+Sort 중첩보다 읽기 쉬움, 조건 추가가 체이닝 한 줄
    // README [S03-02-01], DeepDive [S03-02-01] 참조
    public List<FilterEnemyData> GetTargets(Vector3 from)
    {
        return enemies
            .Where(e => e != null && e.hp >= minHp)
            .Where(e => Vector3.Distance(from, e.transform.position) <= aggroRange)
            .OrderBy(e => Vector3.Distance(from, e.transform.position))
            .ToList();
    }

    // [S03-02-02] WHY: LINQ는 지연 실행, ToList() 시점에 한 번만 돈다
    // 매 프레임 ToList()는 GC 유발, 0.25초 간격 캐싱으로 분할상환
    // README [S03-02-02], DeepDive [S03-02-02] 참조
    private List<FilterEnemyData> cached = new List<FilterEnemyData>();
    private float refreshTimer;

    void Update()
    {
        refreshTimer += Time.deltaTime;
        if (refreshTimer >= 0.25f)
        {
            refreshTimer = 0f;
            cached = GetTargets(transform.position);
            FilterEnemyData first = cached.FirstOrDefault();
            if (first != null)
                Debug.Log($"[S03-02-02] 타겟 {cached.Count}기, 1순위={first.enemyName}");
        }
    }

    // [S03-02-03] WHY: 람다 캡처 변수는 지역 복사 후 캡처한다
    // 루프 변수 직접 캡처는 전원 같은 값 참조, 클로저 누수 원인
    public Func<FilterEnemyData, bool> MakeHpFilter(int threshold)
    {
        int local = threshold;
        return e => e != null && e.hp >= local;
    }

    [ContextMenu("Seed Dummy Enemies")]
    private void SeedDummyEnemies()
    {
        enemies.Clear();
        for (int i = 0; i < 5; i++)
        {
            var go = new GameObject($"Dummy_{i}");
            go.transform.position = transform.position + Vector3.right * (i * 4f);
            var d = go.AddComponent<FilterEnemyData>();
            d.enemyName = $"Dummy_{i}";
            d.hp = (i + 1) * 10;
            enemies.Add(d);
        }
    }
}

// [S03-02-01] 부연: 필터 대상 데이터, MonoBehaviour로 씬에 배치
// 위치 표시: 씬 README [S03-02-01], DeepDive [S03-02-01] 참조
public class FilterEnemyData : MonoBehaviour
{
    public string enemyName;
    public int hp = 30;
}
