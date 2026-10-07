using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// S04_04 씬 전용: Dictionary<string,int> O(1) 인벤토리
public class Dict_Inventory : MonoBehaviour
{
    // [S04-04-01] WHY: 이름->개수 조회는 Dictionary로 O(1)
    // List 선형 탐색 O(n)은 아이템 수백 개면 매번 전수 조사
    // README [S04-04-01], DeepDive [S04-04-01] 참조
    private readonly Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(string id, int count = 1)
    {
        if (items.ContainsKey(id)) items[id] += count;
        else items[id] = count;
        Debug.Log($"[S04-04-01] {id} x{items[id]}");
    }

    // [S04-04-02] WHY: 없는 키 인덱서는 예외, TryGetValue가 정석
    // ContainsKey+인덱서는 해시를 두 번, TryGetValue는 한 번
    // README [S04-04-02], DeepDive [S04-04-02] 참조
    public bool UseItem(string id)
    {
        if (!items.TryGetValue(id, out int count) || count <= 0)
        {
            Debug.LogWarning($"[S04-04-02] {id} 없음");
            return false;
        }
        items[id] = count - 1;
        if (items[id] == 0) items.Remove(id);
        Debug.Log($"[S04-04-02] {id} 사용, 잔여 x{GetCount(id)}");
        return true;
    }

    public int GetCount(string id)
    {
        return items.TryGetValue(id, out int c) ? c : 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) AddItem("Potion");
        if (Input.GetKeyDown(KeyCode.Alpha2)) AddItem("Sword");
        if (Input.GetKeyDown(KeyCode.Q)) UseItem("Potion");
        if (Input.GetKeyDown(KeyCode.Tab)) PrintAll();
    }

    private void PrintAll()
    {
        foreach (var kv in items.OrderBy(kv => kv.Key))
            Debug.Log($"[S04-04-01] {kv.Key} x{kv.Value}");
    }
}
