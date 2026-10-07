using System;
using System.Collections.Generic;
using UnityEngine;

// S08_01 씬 전용: 5대 패턴 한 파일 데모
// 조작: Play 후 Space(데모 실행), Z(마지막 Command Undo)
public class PatternsDemo : MonoBehaviour
{
    private readonly Stack<ICommand> history = new Stack<ICommand>();
    private Health playerHp;

    void Start()
    {
        playerHp = new Health(100);
        playerHp.OnChanged += hp => Debug.Log($"[S08-01-02] HP 변경 통지: {hp}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RunDemo();
        if (Input.GetKeyDown(KeyCode.Z) && history.Count > 0) { history.Pop().Undo(); Debug.Log($"[S08-01-05] Undo 후 Gold={GameManager.Instance.Gold}"); }
    }

    void RunDemo()
    {
        ICommand cmd = new EarnGoldCommand(10); // [S08-01-05]
        cmd.Execute();
        history.Push(cmd);
        playerHp.Damage(15); // [S08-01-02]
        Minion m = EnemyFactory.Create("Slime"); // [S08-01-04]
        m.Tick(5f); // [S08-01-03]
        Debug.Log($"[S08-01-01] Gold={GameManager.Instance.Gold}, [S08-01-03] State={m.Current}");
    }
}

// [S08-01-01] WHY: Singleton은 전역 단일 접점이 필요할 때만
// 남용 시 결합도 상승, 테스트 어려움. 1개만 존재 보장할 때 사용
// README [S08-01-01], DeepDive [S08-01-01] 참조
public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ?? (instance = new GameManager());
    public int Gold { get; private set; }
    private GameManager() { }
    public void Earn(int amount) { Gold += amount; }
}

// [S08-01-02] WHY: Observer(event)로 발행-구독 분리
// UI가 Health를 매 프레임 감시(polling)할 필요 없음. 변경 시에만 통지
// README [S08-01-02], DeepDive [S08-01-02] 참조
public class Health
{
    public event Action<int> OnChanged;
    private int hp;
    public Health(int max) { hp = max; }
    public void Damage(int amount) { hp = Math.Max(0, hp - amount); if (OnChanged != null) OnChanged(hp); }
    public int Current => hp;
}

// [S08-01-03] WHY: State는 if-else 분기 폭발 방지
// 전이 규칙을 Tick 한 곳에 모으고, 전이는 Change()로만 수행
// README [S08-01-03], DeepDive [S08-01-03] 참조
public class Minion
{
    public enum State { Idle, Chase, Attack }
    public State Current { get; private set; } = State.Idle;
    public void Change(State next) { Current = next; }
    public void Tick(float dist)
    {
        if (Current == State.Idle && dist < 8f) Change(State.Chase);
        else if (Current == State.Chase && dist < 2f) Change(State.Attack);
        else if (Current == State.Attack && dist > 3f) Change(State.Chase);
    }
}

// [S08-01-04] WHY: Factory는 생성 조건 분기를 한 곳에 모은다
// 스폰 코드가 적 타입을 몰라도 됨. 신적 추가 시 팩토리만 수정 (OCP)
// README [S08-01-04], DeepDive [S08-01-04] 참조
public static class EnemyFactory
{
    public static Minion Create(string type)
    {
        Minion m = new Minion();
        if (type == "Boss") m.Change(Minion.State.Chase); // 보스는 등장 즉시 추격
        return m;
    }
}

// [S08-01-05] WHY: Command는 요청을 객체로 캡슐화
// 실행/취소(Undo)/큐잉/리플레이 가능. 입력-로직 분리에도 유리
// README [S08-01-05], DeepDive [S08-01-05] 참조
public interface ICommand { void Execute(); void Undo(); }

public class EarnGoldCommand : ICommand
{
    private readonly int amount;
    public EarnGoldCommand(int amount) { this.amount = amount; }
    public void Execute() { GameManager.Instance.Earn(amount); }
    public void Undo() { GameManager.Instance.Earn(-amount); }
}
