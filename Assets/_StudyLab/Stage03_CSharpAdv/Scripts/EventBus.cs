using System;
using System.Collections.Generic;
using UnityEngine;

// S03_01 씬 전용: 제네릭 이벤트 버스 (발행/구독 분리)
public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    // [S03-01-01] WHY: Dictionary<Type, Delegate> 하나로 모든 이벤트 타입 보관
    // 타입별 클래스를 만들면 클래스 폭발, object 박싱은 캐스팅 지옥
    // README [S03-01-01], DeepDive [S03-01-01] 참조
    private readonly Dictionary<Type, Delegate> bus = new Dictionary<Type, Delegate>();

    // [S03-01-02] WHY: Action<T> 구독은 발행자-구독자 결합도를 끊는다
    // 발행자는 구독자가 누군지 몰라도 됨, 해제는 OnDisable에서 필수
    // 파괴된 객체 참조가 남으면 MissingReferenceException 발생
    public void Subscribe<T>(Action<T> handler)
    {
        if (bus.TryGetValue(typeof(T), out Delegate d))
            bus[typeof(T)] = Delegate.Combine(d, handler);
        else
            bus[typeof(T)] = handler;
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        if (bus.TryGetValue(typeof(T), out Delegate d))
        {
            Delegate rest = Delegate.Remove(d, handler);
            if (rest == null) bus.Remove(typeof(T));
            else bus[typeof(T)] = rest;
        }
    }

    public void Publish<T>(T evt)
    {
        if (bus.TryGetValue(typeof(T), out Delegate d))
            ((Action<T>)d)?.Invoke(evt);
    }

    // [S03-01-03] WHY: 값을 되돌려받을 땐 Func, 알림만 보낼 땐 event
    // Func는 질의(계산 위임)용, event는 외부 Invoke 차단용 캡슐화
    public event Action<int> OnScoreChanged;

    [SerializeField] private int score;
    private Func<int, string> scoreFormatter;

    void Awake()
    {
        Instance = this;
        scoreFormatter = s => $"SCORE {s:0000}";
    }

    void OnEnable()
    {
        Subscribe<DamageEvent>(OnDamaged);
    }

    void OnDisable()
    {
        Unsubscribe<DamageEvent>(OnDamaged);
    }

    private void OnDamaged(DamageEvent e)
    {
        score += e.amount;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"[S03-01-03] {scoreFormatter(score)} (+{e.amount})");
    }

    [ContextMenu("Test Publish")]
    private void TestPublish()
    {
        Publish(new DamageEvent { amount = 10 });
    }
}

// [S03-01-01] 부연: 이벤트 페이로드는 struct로, 값 복사로 전달
// 위치 표시: 씬 README [S03-01-01], DeepDive [S03-01-01] 참조
[Serializable]
public struct DamageEvent
{
    public int amount;
}
