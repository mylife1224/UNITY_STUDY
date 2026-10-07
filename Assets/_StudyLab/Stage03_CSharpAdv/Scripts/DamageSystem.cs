using UnityEngine;

// S03_03 씬 전용: IDamageable 인터페이스 + 가상함수 다형성
public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;

    // [S03-03-01] WHY: IDamageable로 받으면 구체 클래스를 몰라도 된다
    // 적/플레이어/통이 달라도 TakeDamage 한 줄로 통일 호출
    // README [S03-03-01], DeepDive [S03-03-01] 참조
    [ContextMenu("Deal 10 Damage")]
    public void DealDamageToAll()
    {
        foreach (var go in targets)
        {
            if (go != null && go.TryGetComponent<IDamageable>(out var d))
                d.TakeDamage(10);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DealDamageToAll();
    }
}

// [S03-03-02] WHY: 인터페이스는 계약만, 구현은 각자 (다중 구현 가능)
// MonoBehaviour는 단일 상속이라 공유 코드는 base 클래스+virtual로
// README [S03-03-02], DeepDive [S03-03-02] 참조
public interface IDamageable
{
    void TakeDamage(int amount);
    bool IsDead { get; }
}

// [S03-03-03] WHY: virtual+override는 기본 동작 공유 + 개체별 변형
// base.TakeDamage() 호출 후 고유 연출 추가가 정석 패턴
// README [S03-03-03], DeepDive [S03-03-03] 참조
public class DamageableBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected int hp = 30;
    public bool IsDead => hp <= 0;

    public virtual void TakeDamage(int amount)
    {
        if (IsDead) return;
        hp -= amount;
        Debug.Log($"[S03-03-03] {name} HP={hp}");
        if (IsDead) Die();
    }

    protected virtual void Die()
    {
        Debug.Log($"[S03-03-03] {name} 사망");
    }
}

public class ExplodingBarrel : DamageableBase
{
    protected override void Die()
    {
        Debug.Log($"[S03-03-03] {name} 폭발! (override 변형)");
        base.Die();
        Destroy(gameObject, 0.1f);
    }
}
