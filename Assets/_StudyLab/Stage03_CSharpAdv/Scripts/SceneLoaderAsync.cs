using System;
using System.Collections;
using UnityEngine;

// S03_04 씬 전용: async/await 비동기 로딩 연출 (Task만, 외부 패키지 없음)
public class SceneLoaderAsync : MonoBehaviour
{
    [SerializeField] private float fakeWorkSeconds = 2f;
    [Range(0f, 1f)] public float progress;

    // [S03-04-01] WHY: await Task.Delay는 대기 중에도 메인 루프가 계속 돈다
    // Thread.Sleep은 게임 전체 정지, Task.Delay는 제어권만 양보
    // README [S03-04-01], DeepDive [S03-04-01] 참조
    public async void LoadAsync()
    {
        progress = 0f;
        float elapsed = 0f;
        try
        {
            while (elapsed < fakeWorkSeconds)
            {
                await System.Threading.Tasks.Task.Delay(200);
                elapsed += 0.2f;
                progress = Mathf.Clamp01(elapsed / fakeWorkSeconds);
                Debug.Log($"[S03-04-01] 로딩 중... {progress:P0}");
            }
            progress = 1f;
            Debug.Log("[S03-04-01] 로딩 완료 (실전: SceneManager.LoadScene 호출 위치)");
        }
        catch (Exception e)
        {
            Debug.LogError($"[S03-04-02] 로딩 실패: {e.Message}");
        }
    }

    // [S03-04-02] WHY: async void는 버튼/이벤트 진입점 전용, 나머진 Task 반환
    // async void 예외는 try/catch 없으면 조용히 소실, 반드시 내부 처리
    // README [S03-04-02], DeepDive [S03-04-02] 참조
    public async System.Threading.Tasks.Task<int> FakeDownloadAsync()
    {
        await System.Threading.Tasks.Task.Delay(500);
        return 42;
    }

    // [S03-04-03] WHY: await 이후 Unity API는 메인 스레드 복귀를 전제한다
    // Task 연속은 스레드풀 경유 가능, 실전 로딩은 코루틴/UniTask 권장
    // 위치 표시: 씬 README [S03-04-03], DeepDive [S03-04-03] 참조
    private IEnumerator LoadCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < fakeWorkSeconds)
        {
            elapsed += Time.deltaTime;
            progress = Mathf.Clamp01(elapsed / fakeWorkSeconds);
            yield return null;
        }
        progress = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadAsync();
    }
}
