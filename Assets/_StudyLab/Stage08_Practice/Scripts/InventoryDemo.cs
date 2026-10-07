using System.Collections.Generic;
using UnityEngine;

// S08_03 씬 전용: EditMode 테스트 가능한 순수 로직 분리
// 조작: Play 후 A(획득), U(사용). 로직은 UnityEngine 미의존이라 단독 테스트 가능
public class InventoryDemo : MonoBehaviour
{
    private InventoryLogic inventory = new InventoryLogic(8);

    void Start()
    {
        Debug.Log("[S08-03-01] A=획득(Potion), U=사용, C=개수 출력.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log(inventory.Add("Potion") ? "[S08-03-01] 획득 성공" : "[S08-03-01] 가득 참");
        if (Input.GetKeyDown(KeyCode.U))
            Debug.Log(inventory.Remove("Potion") ? "[S08-03-01] 사용 성공" : "[S08-03-01] 없음");
        if (Input.GetKeyDown(KeyCode.C))
            Debug.Log($"[S08-03-01] Potion x{inventory.Count("Potion")}, 종류={inventory.SlotCount}");
    }

    // [S08-03-EX-01] WHY: Play 없이 로직만 검증 — ContextMenu에서 즉시 실행
    // 별도 테스트 어셈블리 없이 경계값 검증 가능, Test Runner 전환 전 단계
    [ContextMenu("Run Self Tests")]
    public void RunSelfTests()
    {
        int pass = 0, fail = 0;
        var inv = new InventoryLogic(1);
        Check(inv.Add("A"), ref pass, ref fail, "Add ok");
        Check(!inv.Add("B") && inv.Count("B") == 0, ref pass, ref fail, "AddBeyondCapacity_Fails");
        Check(!new InventoryLogic(4).Remove("Ghost"), ref pass, ref fail, "RemoveMissing_ReturnsFalse");
        var s = new InventoryLogic(1);
        s.Add("Potion"); s.Add("Potion");
        Check(s.Count("Potion") == 2 && s.SlotCount == 1, ref pass, ref fail, "AddSameId_StacksWithoutNewSlot");
        var c = new InventoryLogic(4);
        c.Add("Potion");
        Check(c.Remove("Potion") && c.SlotCount == 0, ref pass, ref fail, "RemoveLast_ClearsSlot");
        Debug.Log($"[S08-03-EX-02] 자가테스트: {pass} 통과, {fail} 실패");
    }

    private void Check(bool cond, ref int pass, ref int fail, string name)
    {
        if (cond) { pass++; Debug.Log($"[S08-03-EX-02] PASS {name}"); }
        else { fail++; Debug.LogError($"[S08-03-EX-02] FAIL {name}"); }
    }
}

// [S08-03-01] WHY: 로직을 MonoBehaviour에서 분리하면 테스트 가능해진다
// UnityEngine을 참조하지 않으므로 NUnit EditMode에서 Play 없이 검증 가능
// README [S08-03-01], DeepDive [S08-03-01] 참조
public class InventoryLogic
{
    private readonly Dictionary<string, int> items = new Dictionary<string, int>();
    private readonly int capacity;

    public InventoryLogic(int capacity) { this.capacity = capacity; }
    public int SlotCount => items.Count;

    public bool Add(string id)
    {
        if (items.ContainsKey(id))
        {
            items[id]++;
            return true;
        }
        if (items.Count >= capacity)
            return false;
        items[id] = 1;
        return true;
    }

    public bool Remove(string id)
    {
        if (!items.ContainsKey(id))
            return false;
        items[id]--;
        if (items[id] <= 0)
            items.Remove(id);
        return true;
    }

    public int Count(string id) => items.ContainsKey(id) ? items[id] : 0;

    // [S08-03-02] NOTE: NUnit 전환 시 이 4건을 [Test]로 옮기면 됨 (Test Runner EditMode)
    // using NUnit.Framework; + 별도 Tests 어셈블리 필요, asmdef 참조 해결 후 전환
    // 지금은 위 RunSelfTests() ContextMenu로 동일 케이스 실행
}
