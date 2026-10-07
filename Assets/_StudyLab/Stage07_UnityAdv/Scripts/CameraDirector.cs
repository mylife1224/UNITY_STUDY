using UnityEngine;

// S07_03 씬 전용: 빌트인 카메라 블렌딩 연출 (Cinemachine 없이)
// 조작: shots에 카메라 위치 Transform을 순서대로 등록, Play 후 Space 다음 샷
public class CameraDirector : MonoBehaviour
{
    [SerializeField] private Transform[] shots;
    [SerializeField] private float blendTime = 1.5f;
    [SerializeField] private float holdTime = 2f;

    private int currentIndex;
    private int targetIndex;
    private float blendT;
    private float holdT;
    private Vector3 blendStartPos;
    private Quaternion blendStartRot;

    // [S07-03-01] WHY: 샷 전환은 즉시 점프가 아니라 블렌딩으로
    // Lerp/Slerp + ease로 가속-감속, 컷의 어색함 제거
    // README [S07-03-01], DeepDive [S07-03-01] 참조
    void Start()
    {
        if (shots == null || shots.Length == 0)
        {
            Debug.LogWarning("[S07-03-01] shots가 비어 있음. 카메라 위치들을 등록하세요.");
            enabled = false;
            return;
        }
        SnapTo(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayNext();

        if (blendT < 1f)
        {
            blendT = Mathf.Min(1f, blendT + Time.deltaTime / blendTime);
            float eased = EaseInOut(blendT);
            Transform target = shots[targetIndex];
            transform.position = Vector3.Lerp(blendStartPos, target.position, eased);
            transform.rotation = Quaternion.Slerp(blendStartRot, target.rotation, eased);
            if (blendT >= 1f)
                holdT = holdTime;
        }
        else if (holdT > 0f)
        {
            holdT -= Time.deltaTime;
        }
    }

    public void PlayNext()
    {
        targetIndex = (currentIndex + 1) % shots.Length;
        blendStartPos = transform.position;
        blendStartRot = transform.rotation;
        blendT = 0f;
        currentIndex = targetIndex;
        Debug.Log($"[S07-03-01] 샷 전환 -> {currentIndex}");
    }

    void SnapTo(int index)
    {
        currentIndex = targetIndex = index;
        transform.position = shots[index].position;
        transform.rotation = shots[index].rotation;
        blendT = 1f;
    }

    static float EaseInOut(float t) => t * t * (3f - 2f * t);

    // [S07-03-02] NOTE: Cinemachine 전환 매핑표 (패키지 없이 주석으로만 학습)
    // 이 스크립트의 shots[]      -> Cinemachine Path / Dolly Track
    // blendTime + EaseInOut      -> vcams 간 Default Blend (Ease In Out)
    // PlayNext()                 -> 카메라 우선순위(Priority) 전환
    // holdTime                   -> 타임라인 클립 길이 / Hold 설정
}
