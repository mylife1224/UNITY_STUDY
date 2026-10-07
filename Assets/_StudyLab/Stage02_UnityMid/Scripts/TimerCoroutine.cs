using System.Collections;
using UnityEngine;

// S02_02 씬 전용: Update 타이머 vs Coroutine 비교
public class TimerCoroutine : MonoBehaviour
{
    [SerializeField] private float delay = 2f;
    [SerializeField] private int repeatCount = 3;

    private float updateTimer;
    private bool updateDone;

    // [S02-02-01] WHY: Update 방식은 매 프레임 누적, 단순하지만 조건 분기 증가
    // 일회성 지연에는 불필요한 매 프레임 체크 발생
    // README [S02-02-01], DeepDive [S02-02-01] 참조
    void Update()
    {
        if (updateDone)
        {
            return;
        }
        updateTimer += Time.deltaTime;
        if (updateTimer >= delay)
        {
            updateDone = true;
            Debug.Log($"[S02-02-01] Update 타이머 완료 ({delay}s)");
        }
    }

    void Start()
    {
        StartCoroutine(RepeatRoutine());
    }

    // [S02-02-02] WHY: Coroutine은 yield로 대기, Update 분기 없이 시간 흐름 표현
    // WaitForSeconds는 스케일 시간 영향, 일시정지 시 함께 멈춤
    // README [S02-02-02], DeepDive [S02-02-02] 참조
    private IEnumerator RepeatRoutine()
    {
        for (int i = 1; i <= repeatCount; i++)
        {
            yield return new WaitForSeconds(delay);
            Debug.Log($"[S02-02-02] Coroutine {i}/{repeatCount}회차 완료");
        }
    }

    // [S02-02-03] WHY: 중복 실행 방지, StartCoroutine 중복 호출 시 타이머 두 배로 돎
    // 정지 필요 시 StopCoroutine/StopAllCoroutines 사용
    public void Restart()
    {
        StopAllCoroutines();
        StartCoroutine(RepeatRoutine());
        Debug.Log("[S02-02-03] Coroutine 재시작");
    }
}
