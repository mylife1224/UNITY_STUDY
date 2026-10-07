using System.Collections;
using UnityEngine;

// S07_05 씬 전용: GC.Alloc 유발 패턴 vs 개선 데모
// 조작: Play 후 B(나쁜 패턴), G(개선 패턴), Console + Profiler CPU/Memory로 비교
public class GCFreeDemo : MonoBehaviour
{
    [SerializeField] private int iterations = 5000;

    private readonly System.Text.StringBuilder cachedBuilder = new System.Text.StringBuilder(512);
    private WaitForSeconds cachedWait;
    private Transform cachedTransform;

    void Start()
    {
        cachedWait = new WaitForSeconds(0.5f);
        cachedTransform = transform;
        Debug.Log("[S07-05-01] B=나쁜 패턴, G=개선 패턴. Profiler에서 GC Alloc 열 확인.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            BadPattern();
        if (Input.GetKeyDown(KeyCode.G))
            GoodPattern();
        if (Input.GetKeyDown(KeyCode.H))
            StartCoroutine(HeartbeatGood());
    }

    // [S07-05-01] WHY: 아래 3줄이 대표적 GC 유발 패턴이다
    // string += 반복, GetComponent 매 프레임, 박싱/클로저 남발
    // README [S07-05-01], DeepDive [S07-05-01] 참조
    void BadPattern()
    {
        long before = System.GC.GetTotalMemory(false);
        string s = "";
        for (int i = 0; i < iterations; i++)
        {
            s += i.ToString() + ",";               // 반복 문자열 할당
            GetComponent<Transform>().position += Vector3.zero; // 매번 조회
        }
        long after = System.GC.GetTotalMemory(false);
        Debug.Log($"[S07-05-01] BAD: +{(after - before) / 1024}KB, len={s.Length}");
    }

    // [S07-05-02] WHY: 할당을 안 하면 GC가 돌 일이 없다
    // StringBuilder 재사용, 컴포넌트 캐싱, yield 캐싱이 3대 처방
    // README [S07-05-02], DeepDive [S07-05-02] 참조
    void GoodPattern()
    {
        long before = System.GC.GetTotalMemory(false);
        cachedBuilder.Length = 0;
        for (int i = 0; i < iterations; i++)
        {
            cachedBuilder.Append(i).Append(',');
            cachedTransform.position += Vector3.zero; // 캐싱된 참조
        }
        string s = cachedBuilder.ToString();
        long after = System.GC.GetTotalMemory(false);
        Debug.Log($"[S07-05-02] GOOD: +{(after - before) / 1024}KB, len={s.Length}");
    }

    IEnumerator HeartbeatGood()
    {
        Debug.Log("[S07-05-02] 캐싱된 WaitForSeconds로 3회 박동 (new 할당 없음)");
        for (int i = 0; i < 3; i++)
            yield return cachedWait;
        Debug.Log("[S07-05-02] 박동 종료.");
    }
}
