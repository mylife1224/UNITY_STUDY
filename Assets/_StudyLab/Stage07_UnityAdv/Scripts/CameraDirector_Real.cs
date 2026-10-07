using UnityEngine;
using Unity.Cinemachine;

// S07_03_ex 실습: 진짜 Cinemachine 3.x 샷 전환 (개념씬 S07_03과 비교 학습용)
// 전제: Cinemachine 패키지(3.x) 설치됨.
public class CameraDirector_Real : MonoBehaviour
{
    [SerializeField] private CinemachineCamera shotA;
    [SerializeField] private CinemachineCamera shotB;
    [SerializeField] private float blendTime = 1.5f;

    // [S07-03-EX-01] WHY: Priority로 샷을 전환한다 — 카메라를 끄고 켜지 않음
    // Brain이 Priority 최상위 2개 사이를 Blend 설정으로 섞어줌
    // README [S07-03-EX-01] 참조
    private bool useA = true;

    void Start()
    {
        EnsureBrain();
        if (shotA == null || shotB == null) AutoSetup();
        ApplyBlend();
        ApplyPriority();
    }

    // [S07-03-EX-02] WHY: Brain이 없으면 전환이 안 된다 — 사전 보장
    // Main Camera에 CinemachineBrain이 없으면 추가, Blend 시간 적용
    private void EnsureBrain()
    {
        Camera cam = Camera.main;
        if (cam == null) cam = FindFirstObjectByType<Camera>();
        if (cam == null) return;
        CinemachineBrain brain = cam.GetComponent<CinemachineBrain>();
        if (brain == null) brain = cam.gameObject.AddComponent<CinemachineBrain>();
        brain.DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.EaseInOut, blendTime);
    }

    private void AutoSetup()
    {
        shotA = CreateShot("ShotA", 10, 60f, new Vector3(0, 5, -10));
        shotB = CreateShot("ShotB", 5, 30f, new Vector3(8, 3, -6));
        Debug.Log("[S07-03-EX-01] 샷 자동 생성됨. 인스펙터에서 Follow 타겟 지정 가능.");
    }

    private CinemachineCamera CreateShot(string name, int priority, float fov, Vector3 pos)
    {
        GameObject go = new GameObject(name);
        CinemachineCamera vcam = go.AddComponent<CinemachineCamera>();
        vcam.Priority = priority;
        vcam.Lens.FieldOfView = fov;
        go.transform.position = pos;
        go.transform.LookAt(Vector3.zero);
        return vcam;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            useA = !useA;
            ApplyPriority();
            Debug.Log($"[S07-03-EX-01] 샷 전환: {(useA ? "A" : "B")} (Blend {blendTime}s)");
        }
    }

    private void ApplyPriority()
    {
        if (shotA == null || shotB == null) return;
        shotA.Priority = useA ? 10 : 5;
        shotB.Priority = useA ? 5 : 10;
    }

    private void ApplyBlend()
    {
        // blendTime은 EnsureBrain에서 Brain에 적용됨
    }
}
