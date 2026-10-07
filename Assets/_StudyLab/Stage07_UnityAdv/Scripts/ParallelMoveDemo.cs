using System.Threading.Tasks;
using UnityEngine;

// S07_04 씬 전용: Tasks.Parallel 10k 이동 연산 비교
// 조작: Play 후 Space(순차), P(병렬) 실행, Console에서 ms 비교
public class ParallelMoveDemo : MonoBehaviour
{
    [SerializeField] private int count = 10000;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dt = 0.016f;

    private Vector3[] positions;
    private Vector3[] velocities;

    void Start()
    {
        positions = new Vector3[count];
        velocities = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            positions[i] = new Vector3(i % 100, 0f, i / 100);
            velocities[i] = new Vector3(0f, 0f, speed);
        }
        Debug.Log($"[S07-04-01] {count}개 이동 데이터 준비 완료. Space=순차, P=병렬");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RunSequential();
        if (Input.GetKeyDown(KeyCode.P))
            RunParallel();
    }

    void RunSequential()
    {
        float start = Time.realtimeSinceStartup;
        for (int i = 0; i < count; i++)
            positions[i] += velocities[i] * dt;
        float ms = (Time.realtimeSinceStartup - start) * 1000f;
        Debug.Log($"[S07-04-01] 순차 for: {ms:F2} ms ({count}개)");
    }

    // [S07-04-01] WHY: CPU 코어를 나누면 순수 연산이 빨라진다
    // 인덱스 구간을 스레드에 분할, 합류(join) 후 결과 동일
    // 단, Unity API(Transform 등)는 메인 스레드 전용이라 Parallel 안에서 호출 금지
    // README [S07-04-01], DeepDive [S07-04-01] 참조
    void RunParallel()
    {
        float start = Time.realtimeSinceStartup;
        Parallel.For(0, count, i =>
        {
            positions[i] += velocities[i] * dt;
        });
        float ms = (Time.realtimeSinceStartup - start) * 1000f;
        Debug.Log($"[S07-04-01] Parallel.For: {ms:F2} ms ({count}개)");
    }

    // [S07-04-02] NOTE: Job System / Burst 전환 매핑표 (패키지 없이 주석으로만 학습)
    // Parallel.For 본문             -> IJobParallelFor.Execute(index)
    // positions/velocities 배열      -> NativeArray (구조체 값타입만)
    // Task 스레드풀 오버헤드         -> Job은 워커스레드 + Burst SIMD로 추가 가속
    // Transform 접근 금지(양쪽 공통) -> 계산은 Job, 적용은 메인스레드에서 일괄 반영
}
