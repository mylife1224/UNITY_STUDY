using UnityEngine;

// S08_04 씬 전용: 빌드 전 체크리스트 Console 출력 (에디터 겸용, UnityEditor 미의존)
// 조작: Play 후 B 또는 ContextMenu "Print Build Checklist"
public class BuildChecklist : MonoBehaviour
{
    // [S08-04-01] WHY: 빌드 실패의 8할은 체크리스트로 예방된다
    // 씬 등록/해상도/권한/심볼을 코드로 점검하면 휴먼에러 제거
    // README [S08-04-01], DeepDive [S08-04-01] 참조
    private static readonly string[] Checklist = new string[]
    {
        "Scenes In Build에 시작 씬 + 전 스테이지 씬 등록 확인",
        "Company Name / Product Name / Bundle Identifier 설정",
        "기본 화면 방향 + 지원 해상도 (가로/세로) 확정",
        "Development Build OFF 여부 (릴리스 기준) 확인",
        "Scripting Backend (Mono/IL2CPP) + Target Architecture 확정",
        "Api Compatibility / Strip Engine Code 영향 검토",
        "저장 경로 쓰기 권한 (persistentDataPath) 동작 확인",
        "외부 SDK 키·권한 (인터넷/진동 등) 최종 점검",
    };

    void Start()
    {
        PrintChecklist();
#if UNITY_EDITOR
        PrintEditorNote(); // [S08-04-02] 에디터에서만 추가 안내 (빌드 제외)
#endif
    }

    [ContextMenu("Print Build Checklist")]
    public void PrintChecklist()
    {
        Debug.Log("[S08-04-01] === Final Build Checklist ===");
        for (int i = 0; i < Checklist.Length; i++)
            Debug.Log($"[S08-04-01] [{i + 1}/{Checklist.Length}] {Checklist[i]}");
    }

    // [S08-04-02] WHY: #if UNITY_EDITOR로 에디터 전용 분기, using UnityEditor 없음
    // using을 추가하면 빌드 시 컴파일 에러. 전처리기로만 분리하는 것이 안전
    // README [S08-04-02], DeepDive [S08-04-02] 참조
#if UNITY_EDITOR
    void PrintEditorNote()
    {
        // NOTE: 실제 자동화 시 EditorBuildSettingsScene / PlayerSettings로 점검.
        // 이 스크립트는 using UnityEditor 없이 안내만 출력 (컴파일 안전 우선).
        Debug.Log("[S08-04-02] 에디터: File > Build Settings에서 씬 목록과 플랫폼 재확인.");
    }
#endif

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            PrintChecklist();
    }
}
