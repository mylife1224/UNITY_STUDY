using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// S07_02_ex 실습: 진짜 Addressables 로드 (개념씬 S07_02와 비교 학습용)
// 전제: Addressables 패키지 설치됨. Groups 초기설정은 README 조작법 참조.
public class AddressableLoader_Real : MonoBehaviour
{
    [SerializeField] private string addressKey = "Enemy_Goblin";
    [SerializeField] private Vector3 spawnPos = new Vector3(0, 1, 0);

    // [S07-02-EX-01] WHY: 핸들을 필드로 보관한다 — Release 대상 추적용
    // Load 후 Release 없이 반복하면 카운트 누적으로 메모리 해제 안 됨
    // README [S07-02-EX-01], DeepDive [S07-02-01] 참조
    private AsyncOperationHandle<GameObject> handle;
    private GameObject spawned;

    // [S07-02-EX-02] WHY: Completed 콜백에서 Status를 먼저 분기한다
    // 키 오타/미등록이 흔한 실패 원인, Failed 로그에 키를必ず 포함
    [ContextMenu("Load")]
    public void Load()
    {
        if (handle.IsValid())
        {
            Debug.Log("[S07-02-EX-01] 이미 로드된 핸들 있음. Release 후 재시도.");
            return;
        }
        handle = Addressables.LoadAssetAsync<GameObject>(addressKey);
        handle.Completed += OnLoaded;
    }

    private void OnLoaded(AsyncOperationHandle<GameObject> h)
    {
        // [S07-02-EX-02] 부연: 성공/실패 분기 위치
        if (h.Status == AsyncOperationStatus.Succeeded)
        {
            spawned = Instantiate(h.Result, spawnPos, Quaternion.identity);
            Debug.Log($"[S07-02-EX-02] 로드 성공: {addressKey}");
        }
        else
        {
            Debug.LogError($"[S07-02-EX-02] 로드 실패: 키 '{addressKey}' 확인 (Groups 등록 여부)");
            Addressables.Release(h);
            handle = default;
        }
    }

    [ContextMenu("Release")]
    public void Release()
    {
        if (spawned != null) Destroy(spawned);
        if (handle.IsValid()) Addressables.Release(handle);
        handle = default;
        Debug.Log("[S07-02-EX-01] 해제 완료");
    }

    void OnDestroy()
    {
        if (handle.IsValid()) Addressables.Release(handle);
    }
}
