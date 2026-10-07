using UnityEngine;

// S01_01 씬 전용: if/for 이동 제어 예제 (기존 Input Manager 방식)
public class PlayerMover_Basic : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float range = 4f;

    // [S01-01-01] WHY: 매 프레임 입력 누적 이동
    // Time.deltaTime 곱으로 프레임 독립 속도 유지
    // README [S01-01-01], DeepDive [S01-01-01] 참조
    void Update()
    {
        float h = 0f;
        if (Input.GetKey(KeyCode.A)) h = -1f;
        else if (Input.GetKey(KeyCode.D)) h = 1f;

        float v = 0f;
        if (Input.GetKey(KeyCode.W)) v = 1f;
        else if (Input.GetKey(KeyCode.S)) v = -1f;

        Vector3 dir = new Vector3(h, 0f, v);
        transform.position += dir * speed * Time.deltaTime;

        // [S01-01-02] WHY: if로 경계 고정, 물리 없이 간단 클램프
        // Rigidbody 없을 때 초급용, 튕김 필요하면 Stage02 참조
        ClampInside(range);
    }

    // [S01-01-03] WHY: for로 축별 클램프, 축 추가돼도 확장 용이
    // Mathf.Clamp 이해 전 단계로 조건문 학습용
    private void ClampInside(float r)
    {
        Vector3 p = transform.position;
        float[] limits = new float[] { p.x, p.z };
        for (int i = 0; i < limits.Length; i++)
        {
            if (limits[i] > r) limits[i] = r;
            else if (limits[i] < -r) limits[i] = -r;
        }
        transform.position = new Vector3(limits[0], p.y, limits[1]);
    }
}
