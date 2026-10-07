using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

// S07_04_ex 실습: 진짜 IJobParallelFor + Burst (개념씬 S07_04와 비교 학습용)
// 전제: Burst + Collections 패키지 설치됨. 첫 실행 시 Burst 컴파일로 수 초 소요(웜업).
public class ParallelMoveJob_Real : MonoBehaviour
{
    [SerializeField] private int count = 10000;
    [SerializeField] private bool useBurstJob = true;

    private NativeArray<Vector3> pos;
    private NativeArray<Vector3> vel;

    // [S07-04-EX-01] WHY: 잡 구조체는 값형식 + blittable 필드만
    // 참조형식(class, string) 포함 시 Burst 컴파일 불가
    // README [S07-04-EX-01] 참조
    [BurstCompile]
    private struct MoveJob : IJobParallelFor
    {
        public NativeArray<Vector3> pos;
        [ReadOnly] public NativeArray<Vector3> vel;
        public float dt;

        public void Execute(int i)
        {
            pos[i] = pos[i] + vel[i] * dt;
        }
    }

    void Start()
    {
        pos = new NativeArray<Vector3>(count, Allocator.Persistent);
        vel = new NativeArray<Vector3>(count, Allocator.Persistent);
        for (int i = 0; i < count; i++)
        {
            pos[i] = Random.insideUnitSphere * 10f;
            vel[i] = Random.onUnitSphere;
        }
        Debug.Log($"[S07-04-EX-01] {count}개 할당 완료. Space: Job/Burst <-> 메인루프 전환");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) useBurstJob = !useBurstJob;

        // [S07-04-EX-02] WHY: Schedule+Complete로 잡 실행, 배치 64는 경험적 분할 단위
        // Complete()까지 같은 프레임에 호출 — 결과 즉시 사용 데모용
        float t0 = Time.realtimeSinceStartup;
        if (useBurstJob)
        {
            new MoveJob { pos = pos, vel = vel, dt = Time.deltaTime }.Schedule(count, 64).Complete();
        }
        else
        {
            for (int i = 0; i < count; i++)
                pos[i] = pos[i] + vel[i] * Time.deltaTime;
        }
        float ms = (Time.realtimeSinceStartup - t0) * 1000f;
        if (Time.frameCount % 60 == 0)
            Debug.Log($"[S07-04-EX-02] {(useBurstJob ? "Job+Burst" : "메인루프")}: {ms:F2}ms ({count}개)");
    }

    void OnDestroy()
    {
        // [S07-04-EX-01] 부연: Persistent 할당은 반드시 Dispose, IsCreated 가드
        if (pos.IsCreated) pos.Dispose();
        if (vel.IsCreated) vel.Dispose();
    }
}
