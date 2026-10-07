using System.Collections;
using UnityEngine;

// S07_02 씬 전용: Resources 비동기 로드 + Addressables 전환 개념
// 조작: Resources 폴더에 프리팹 배치 후 assetPath 입력, Play 후 Space 로드, R 해제
public class AddressableLoader_Concept : MonoBehaviour
{
    [SerializeField] private string assetPath = "Enemies/Slime";
    [SerializeField] private Transform spawnPoint;

    private GameObject loadedInstance;

    // [S07-02-01] WHY: 비동기 로드로 프레임 멈춤 방지
    // Resources.Load는 동기(끊김), LoadAsync는 프레임 분할 로드
    // Addressables 전환 시 이 코루틴 구조 그대로 LoadAssetAsync로 교체
    // README [S07-02-01], DeepDive [S07-02-01] 참조
    public void LoadAsync()
    {
        if (loadedInstance != null)
        {
            Debug.Log("[S07-02-01] 이미 로드됨. R으로 해제한 뒤 다시 시도.");
            return;
        }
        StartCoroutine(LoadRoutine());
    }

    IEnumerator LoadRoutine()
    {
        ResourceRequest req = Resources.LoadAsync<GameObject>(assetPath);
        while (!req.isDone)
        {
            Debug.Log($"[S07-02-01] 로딩 중... {req.progress:P0}");
            yield return null;
        }
        GameObject prefab = req.asset as GameObject;
        if (prefab == null)
        {
            Debug.LogError($"[S07-02-01] 경로 확인 필요: Resources/{assetPath}");
            yield break;
        }
        Vector3 pos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        loadedInstance = Instantiate(prefab, pos, Quaternion.identity);
        Debug.Log("[S07-02-01] 로드+스폰 완료.");
    }

    // [S07-02-02] WHY: 로드한 에셋은 명시적 해제까지 메모리에 잔류
    // Destroy(인스턴스)만으로 에셋 메모리 해제 안 됨. Unload 계열 호출 필요
    // Addressables에서는 Addressables.Release(handle)로 1:1 대응
    public void Release()
    {
        if (loadedInstance == null)
            return;
        Destroy(loadedInstance);
        loadedInstance = null;
        Resources.UnloadUnusedAssets();
        Debug.Log("[S07-02-02] 인스턴스 파괴 + 미사용 에셋 해제 요청.");
    }

    // [S07-02-03] NOTE: Addressables 전환 매핑표 (패키지 없이 주석으로만 학습)
    // Resources.LoadAsync(path)      -> Addressables.LoadAssetAsync<T>(address)
    // Resources.UnloadUnusedAssets() -> Addressables.Release(handle)
    // 경로 문자열 의존                 -> Address string 키 + 그룹/라벨 관리
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadAsync();
        if (Input.GetKeyDown(KeyCode.R))
            Release();
    }
}
