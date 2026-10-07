using UnityEngine;
using System.Collections.Generic;

// S04_05 씬 전용: 트리 구조 스킬 노드 (부모 잠금/자식 해금)
public class Tree_SkillNode : MonoBehaviour
{
    [SerializeField] private int skillPoints = 5;
    private SkillNode root;

    void Start()
    {
        root = new SkillNode("Root", 0);
        var atk = new SkillNode("Attack+", 1);
        atk.children.Add(new SkillNode("Crit+", 2));
        var def = new SkillNode("Defense+", 1);
        def.children.Add(new SkillNode("Thorns", 2));
        root.children.Add(atk);
        root.children.Add(def);
        root.unlocked = true;
        PrintTree(root, 0);
    }

    // [S04-05-01] WHY: 스킬트리는 부모-자식 계층, 트리가 정석 표현
    // 평탄 List+parentId는 조상 탐색마다 전수 조사, 참조 연결은 O(깊이)
    // README [S04-05-01], DeepDive [S04-05-01] 참조
    [ContextMenu("Unlock Next")]
    public void UnlockNext()
    {
        SkillNode target = FindUnlockable(root);
        if (target == null)
        {
            Debug.Log("[S04-05-01] 해금 가능 스킬 없음");
            return;
        }
        if (skillPoints < target.cost)
        {
            Debug.Log($"[S04-05-01] {target.name} 포인트 부족 (필요 {target.cost}, 보유 {skillPoints})");
            return;
        }
        skillPoints -= target.cost;
        target.unlocked = true;
        Debug.Log($"[S04-05-01] {target.name} 해금! 남은 포인트={skillPoints}");
        PrintTree(root, 0);
    }

    // [S04-05-02] WHY: 해금 탐색은 부모부터 내려오는 DFS로 판단
    // 부모 미해금이면 자식은 시도조차 불가, 불필요 탐색 차단
    // README [S04-05-02], DeepDive [S04-05-02] 참조
    private SkillNode FindUnlockable(SkillNode node)
    {
        foreach (var child in node.children)
        {
            if (!child.unlocked && node.unlocked) return child;
            SkillNode deeper = FindUnlockable(child);
            if (deeper != null) return deeper;
        }
        return null;
    }

    private void PrintTree(SkillNode node, int depth)
    {
        string mark = node.unlocked ? "[해금]" : $"({node.cost}pt)";
        Debug.Log($"[S04-05-02] {new string(' ', depth * 2)}- {node.name} {mark}");
        foreach (var c in node.children) PrintTree(c, depth + 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnlockNext();
    }
}

// [S04-05-01] 부연: 순수 C# 노드, Unity 의존 없음 (테스트 용이)
// 위치 표시: 씬 README [S04-05-01], DeepDive [S04-05-01] 참조
public class SkillNode
{
    public string name;
    public int cost;
    public bool unlocked;
    public List<SkillNode> children = new List<SkillNode>();

    public SkillNode(string name, int cost)
    {
        this.name = name;
        this.cost = cost;
    }
}
