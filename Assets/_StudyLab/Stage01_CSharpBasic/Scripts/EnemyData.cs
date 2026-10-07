using UnityEngine;

// S01_03 씬 전용: class(참조) vs struct(값) 차이 실험
// 값 복사 후 원본이 바뀌는지 로그로 확인하는 초급 예제
[System.Serializable]
public struct EnemyStatStruct
{
    public string enemyName;
    public int hp;
}

[System.Serializable]
public class EnemyStatClass
{
    public string enemyName;
    public int hp;
}

public class EnemyData : MonoBehaviour
{
    [SerializeField] private EnemyStatStruct structA;
    [SerializeField] private EnemyStatClass classA;

    // [S01-03-01] WHY: struct 대입은 값 복사, 복사본 수정이 원본에 영향 없음
    // 작은 데이터 묶음(HP, 이름)에 적합
    // README [S01-03-01], DeepDive [S01-03-01] 참조
    void Start()
    {
        structA = new EnemyStatStruct { enemyName = "Slime", hp = 30 };
        classA = new EnemyStatClass { enemyName = "Slime", hp = 30 };

        EnemyStatStruct structB = structA;
        structB.hp = 1;
        Debug.Log($"[S01-03-01] struct 원본 hp={structA.hp}, 복사본 hp={structB.hp}");

        // [S01-03-02] WHY: class 대입은 참조 복사, 복사본 수정이 원본도 변경
        // 공유 상태라서 실수하면 버그, 의도적 공유에만 사용
        // README [S01-03-02], DeepDive [S01-03-02] 참조
        EnemyStatClass classB = classA;
        classB.hp = 1;
        Debug.Log($"[S01-03-02] class 원본 hp={classA.hp}, 복사본 hp={classB.hp}");

        // [S01-03-03] WHY: MonoBehaviour는 class 강제, 씬에 붙어 Unity가 관리
        // 데이터 자체는 struct/class 분리, 동작은 MonoBehaviour 담당
        Debug.Log($"[S01-03-03] {name} 준비 완료. 인스펙터에서 structA/classA 확인");
    }
}
