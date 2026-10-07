using UnityEngine;

// S00_02 씬 전용: 값형식 vs 참조형식 + SerializeField
public class VariablesMemory : MonoBehaviour
{
    // [S00-02-02] WHY: private + SerializeField로 캡슐화 유지
    // public은 외부 접근 허용 -> 의도치 않은 수정 위험
    [SerializeField] private int hp = 100;
    [SerializeField] private Vector3 spawnPos = new Vector3(0, 1, 0);

    void Start()
    {
        Debug.Log($"[S00-02-02] hp={hp} (인스펙터에서 바꿔도 Play마다 이 값으로 시작)");
        // [S00-02-01] WHY: struct는 값이 복사된다
        // Vector3는 struct -> 대입 시 통째로 복사, 원본에 영향 없음
        Vector3 a = spawnPos;
        Vector3 b = a;
        b.x = 999f;
        Debug.Log($"[S00-02-01] a={a}, b={b} (a는 그대로)");

        // [S00-02-01] WHY: class는 참조가 복사된다 (대조용)
        // 같은 인스턴스를 가리키므로 한쪽 수정이 반대쪽에 영향
        DummyRef r1 = new DummyRef { value = 10 };
        DummyRef r2 = r1;
        r2.value = 999;
        Debug.Log($"[S00-02-01] r1={r1.value}, r2={r2.value} (둘 다 바뀜)");
    }

    private class DummyRef { public int value; }
}
