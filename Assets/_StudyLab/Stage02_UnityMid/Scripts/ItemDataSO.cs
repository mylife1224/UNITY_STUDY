using UnityEngine;

// S02_04 씬 전용: 아이템 데이터 ScriptableObject
// Project 우클릭 > Create > StudyLab > Item Data 로 에셋 생성
[CreateAssetMenu(fileName = "NewItem", menuName = "StudyLab/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName = "Potion";
    [TextArea] public string description = "HP를 50 회복한다.";
    public Sprite icon;

    [Header("수치")]
    public int price = 100;
    public int healAmount = 50;
    public int maxStack = 9;

    // [S02-04-01] WHY: 데이터는 에셋으로 분리, 코드 수정 없이 수치 조정
    // 씬/프리팹에 하드코딩하면 밸런스 패치마다 코드 수정 필요
    // README [S02-04-01], DeepDive [S02-04-01] 참조
    public string GetLabel()
    {
        return $"{itemName} ({price}G)";
    }

    // [S02-04-02] WHY: 에셋 단계에서 값 검증, 음수 가격 같은 실수 사전 차단
    // OnValidate는 인스펙터 수정 시 자동 호출됨
    private void OnValidate()
    {
        if (price < 0)
        {
            price = 0;
        }
        if (maxStack < 1)
        {
            maxStack = 1;
        }
    }

    public bool CanStack(int count)
    {
        return count < maxStack;
    }
}
