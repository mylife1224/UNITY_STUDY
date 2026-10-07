using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// S06_02 씬 전용: 로그라이크 인벤토리 (Dictionary + JSON 세이브)
public class Roguelike_Inventory : MonoBehaviour
{
    [Serializable]
    private class Entry { public string id; public int count; }
    [Serializable]
    private class SaveData { public List<Entry> items = new List<Entry>(); public int gold; }

    private const string SaveKey = "S06_02_Inventory";
    // [S06-02-01] WHY: 아이템 조회/가감은 Dictionary로 O(1)
    // List 선형탐색은 인벤이 커질수록 매번 느려진다
    private Dictionary<string, int> inv = new Dictionary<string, int>();
    private int gold;

    void Start()
    {
        Add("Potion", 3); Add("Sword", 1); Add("Arrow", 20);
        gold = 150;
        Print("획득 후");
        Save();
        Remove("Potion", 3); Remove("Arrow", 25);
        Print("사용 후");
        Load();
        Print("로드 후(복구 확인)");
    }

    public void Add(string id, int n)
    {
        if (inv.ContainsKey(id)) inv[id] += n; else inv[id] = n;
        Debug.Log($"[S06-02-01] +{id} x{n} = {inv[id]}");
    }

    public void Remove(string id, int n)
    {
        if (!inv.TryGetValue(id, out int cur) || cur < n)
        {
            Debug.Log($"[S06-02-01] {id} 부족 (보유 {cur})");
            return;
        }
        inv[id] = cur - n;
        if (inv[id] == 0) inv.Remove(id);
    }

    public void Save()
    {
        // [S06-02-02] WHY: JsonUtility+PlayerPrefs면 파일 경로 고민 없이 저장
        // Dictionary는 직렬화 불가 -> List<Entry> 래퍼로 변환해서 저장
        var data = new SaveData { gold = gold };
        foreach (var kv in inv) data.items.Add(new Entry { id = kv.Key, count = kv.Value });
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
        Debug.Log($"[S06-02-02] 저장 완료: {data.items.Count}종, gold={gold}");
    }

    public void Load()
    {
        // [S06-02-03] WHY: 로드 실패(키 없음/깨진 JSON)에 기본값으로 복구
        // 크래시 대신 빈 인벤 시작 — 세이브 호환성 깨짐 대비
        if (!PlayerPrefs.HasKey(SaveKey)) { Debug.Log("[S06-02-03] 세이브 없음"); return; }
        try
        {
            var data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SaveKey));
            inv.Clear();
            foreach (var e in data.items) inv[e.id] = e.count;
            gold = data.gold;
        }
        catch (Exception e) { Debug.Log($"[S06-02-03] 로드 실패, 초기화: {e.Message}"); inv.Clear(); gold = 0; }
    }

    private void Print(string title)
    {
        string list = inv.Count == 0 ? "(빈 인벤)" :
            string.Join(", ", inv.Select(kv => $"{kv.Key}x{kv.Value}").ToArray());
        Debug.Log($"[S06-02-01] [{title}] gold={gold} | {list}");
    }
}
