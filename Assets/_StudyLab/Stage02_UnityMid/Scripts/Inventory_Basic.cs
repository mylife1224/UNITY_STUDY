using System.Collections.Generic;
using UnityEngine;

// S02_04 씬 전용: ItemDataSO를 참조하는 초급 인벤토리
public class Inventory_Basic : MonoBehaviour
{
    [SerializeField] private List<ItemDataSO> items = new List<ItemDataSO>();
    [SerializeField] private int capacity = 12;

    // [S02-04-03] WHY: 인벤토리는 SO 참조만 보관, 데이터 원본은 에셋에 유지
    // 복사본을 만들면 원본 수정이 인벤토리에 반영 안 됨
    // README [S02-04-03], DeepDive [S02-04-03] 참조
    public bool AddItem(ItemDataSO item)
    {
        if (item == null)
        {
            Debug.LogWarning("[S02-04-03] null 아이템은 추가 불가.");
            return false;
        }
        if (items.Count >= capacity)
        {
            Debug.Log($"[S02-04-03] 인벤토리 가득 참 ({capacity}칸)");
            return false;
        }
        items.Add(item);
        Debug.Log($"[S02-04-03] 획득: {item.GetLabel()}, {items.Count}/{capacity}");
        return true;
    }

    public void PrintAll()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                Debug.Log($"[슬롯{i}] {items[i].GetLabel()}");
            }
        }
    }
}
