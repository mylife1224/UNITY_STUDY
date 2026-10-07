using UnityEngine;

// S02_01 씬 전용: Rigidbody + Collision vs Trigger 비교
// Rigidbody 필수, Collider의 isTrigger 체크에 따라 분기
[RequireComponent(typeof(Rigidbody))]
public class Bouncer : MonoBehaviour
{
    [SerializeField] private float bounceForce = 8f;
    [SerializeField] private int hitCount;

    private Rigidbody rb;

    // [S02-01-01] WHY: 물리 이동은 힘/속도로 수행, transform 직접 이동 금지
    // Rigidbody와 Transform 혼용 시 터널링/진동 발생
    // README [S02-01-01], DeepDive [S02-01-01] 참조
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.AddForce(Vector3.forward * bounceForce, ForceMode.Impulse);
    }

    // [S02-01-02] WHY: Collision은 튕김/반발(물리 반응), 접촉 지점 정보 포함
    // 상대 Collider의 isTrigger가 false일 때만 호출됨
    // README [S02-01-02], DeepDive [S02-01-02] 참조
    void OnCollisionEnter(Collision collision)
    {
        hitCount++;
        Vector3 normal = collision.contacts[0].normal;
        rb.AddForce(normal * bounceForce, ForceMode.Impulse);
        Debug.Log($"[S02-01-02] Collision: {collision.gameObject.name}, 누적={hitCount}");
    }

    // [S02-01-03] WHY: Trigger는 물리 반응 없이 통과 감지, 이벤트 존에 사용
    // 코인/골대 같은 감지용, 상대 중 하나는 Rigidbody 필요
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[S02-01-03] Trigger 감지: {other.gameObject.name}");
    }
}
