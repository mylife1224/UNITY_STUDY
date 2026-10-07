using System;
using UnityEngine;

// S08_02 씬 전용: JsonUtility + PlayerPrefs + 파일 2단 저장
// 조작: Play 후 S(저장), L(불러오기), D(삭제). gold 값을 바꿔가며 확인
public class SaveSystem_JSON : MonoBehaviour
{
    private const string PREFS_KEY = "StudyLab_SaveSlot0";

    [SerializeField] private int gold = 100;
    [SerializeField] private string playerName = "Trainee";
    [SerializeField] private int stage = 8;

    // [S08-02-01] WHY: JsonUtility는 Unity 직렬화 규칙을 그대로 쓴다
    // public/SerializeField만 저장, 프로퍼티·딕셔너리 미지원. 규칙을 알면 실수 방지
    // README [S08-02-01], DeepDive [S08-02-01] 참조
    [Serializable]
    public class SaveData
    {
        public int gold;
        public string playerName;
        public int stage;
        public string savedAt;
    }

    private static string FilePath =>
        System.IO.Path.Combine(Application.persistentDataPath, "saveslot0.json");

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Save();
        if (Input.GetKeyDown(KeyCode.L))
            Load();
        if (Input.GetKeyDown(KeyCode.D))
            DeleteSave();
    }

    // [S08-02-02] WHY: 빠른 슬롯(PlayerPrefs) + 영속 백업(파일) 2단 저장
    // PlayerPrefs는 간편하지만 용량/보안 한계. 파일은 백업·공유·디버깅 용이
    // README [S08-02-02], DeepDive [S08-02-02] 참조
    [ContextMenu("Save")]
    public void Save()
    {
        SaveData data = new SaveData
        {
            gold = gold,
            playerName = playerName,
            stage = stage,
            savedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        string json = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(PREFS_KEY, json);
        PlayerPrefs.Save();
        System.IO.Directory.CreateDirectory(Application.persistentDataPath);
        System.IO.File.WriteAllText(FilePath, json);
        Debug.Log($"[S08-02-02] 저장 완료: {json}");
    }

    [ContextMenu("Load")]
    public void Load()
    {
        string json = PlayerPrefs.GetString(PREFS_KEY, null);
        if (string.IsNullOrEmpty(json) && System.IO.File.Exists(FilePath))
        {
            json = System.IO.File.ReadAllText(FilePath); // 파일 폴백
            Debug.Log("[S08-02-02] PlayerPrefs 없음, 파일에서 복구.");
        }
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("[S08-02-01] 저장 데이터 없음. S로 먼저 저장.");
            return;
        }
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        gold = data.gold;
        playerName = data.playerName;
        stage = data.stage;
        Debug.Log($"[S08-02-01] 불러오기: gold={gold}, name={playerName}, stage={stage}");
    }

    [ContextMenu("Delete Save")]
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(PREFS_KEY);
        if (System.IO.File.Exists(FilePath))
            System.IO.File.Delete(FilePath);
        Debug.Log("[S08-02-02] 슬롯 + 파일 삭제 완료.");
    }
}
