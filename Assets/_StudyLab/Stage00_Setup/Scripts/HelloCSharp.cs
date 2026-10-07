using UnityEngine;

// S00_01 씬 전용: Unity에서 C#이 실행되는 흐름 학습
public class HelloCSharp : MonoBehaviour
{
    // [S00-01-01] WHY: 데이터는 순수 C# 클래스로 분리한다
    // MonoBehaviour에 전부 넣으면 재사용/테스트가 어려움
    // 로직은 분리, MonoBehaviour는 연결 담당
    private PlayerData data = new PlayerData { name = "Hero", hp = 100 };

    // [S00-01-02] WHY: Awake/Start/Update 순서를 의식한다
    // Unity가 자동으로 호출, 생성자 대신 Awake/Start에 초기화
    void Awake()
    {
        Debug.Log("[S00-01-02] Awake: 최초 1회, 오브젝트 활성화 시");
    }

    void Start()
    {
        Debug.Log($"[S00-01-01] Hello {data.name}, HP={data.hp}");
    }

    void Update()
    {
        // [S00-01-03] WHY: Update에 무거운 new/Find 금지 (매 프레임 호출)
        // 60fps = 초당 60회 호출, GC/성능 직결
    }
}

// [S00-01-01] 부연: MonoBehaviour를 상속하지 않은 순수 C# 클래스
// 위치 표시: 씬 README [S00-01-01], DeepDive [S00-01-01] 참조
[System.Serializable]
public class PlayerData
{
    public string name;
    public int hp;
}
