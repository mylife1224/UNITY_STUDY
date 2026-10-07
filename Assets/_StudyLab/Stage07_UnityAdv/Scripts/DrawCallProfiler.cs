using System.Collections.Generic;
using UnityEngine;

// S07_01 씬 전용: DrawCall / 배칭 개념 프로파일러
// 조작: Play 후 Space(공유/개별 머티리얼 전환), C(리빌드)
public class DrawCallProfiler : MonoBehaviour
{
    [SerializeField] private int objectCount = 200;
    [SerializeField] private bool shareMaterial = true;
    [SerializeField] private float spacing = 1.2f;

    private Material sharedMat;
    private readonly List<GameObject> spawned = new List<GameObject>();

    // [S07-01-01] WHY: 머티리얼 공유가 배칭의 첫 조건이다
    // 머티리얼이 다르면 같은 메시라도 별도 DrawCall 발생
    // README [S07-01-01], DeepDive [S07-01-01] 참조
    void Start()
    {
        sharedMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        BuildGrid();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shareMaterial = !shareMaterial;
            BuildGrid();
        }
        if (Input.GetKeyDown(KeyCode.C))
            BuildGrid();
    }

    void BuildGrid()
    {
        foreach (GameObject go in spawned)
            Destroy(go);
        spawned.Clear();

        int side = Mathf.CeilToInt(Mathf.Sqrt(objectCount));
        for (int i = 0; i < objectCount; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3((i % side) * spacing, 0f, (i / side) * spacing);
            // [S07-01-02] WHY: renderer.material은 자동 복제, sharedMaterial은 공유
            // 개별 new Material()은 배칭 그룹 분리 -> DrawCall 증가
            // 공유 모드에서는 sharedMat 하나만 사용
            if (shareMaterial)
                cube.GetComponent<Renderer>().sharedMaterial = sharedMat;
            else
                cube.GetComponent<Renderer>().material = new Material(sharedMat);
            spawned.Add(cube);
        }
        Debug.Log($"[S07-01-01] mode={(shareMaterial ? "Shared" : "Unique")}, objects={spawned.Count}");
    }

    // [S07-01-03] NOTE: 진짜 DrawCall 수는 Frame Debugger에서 확인
    // Window > Analysis > Frame Debugger > Enable 후 draw 순서 추적
    // Profiler Rendering 모듈의 Set Pass Calls / Batches와 대조할 것
    [ContextMenu("Report Estimate")]
    void ReportEstimate()
    {
        int uniqueMats = shareMaterial ? 1 : spawned.Count;
        Debug.Log($"[S07-01-03] materials={uniqueMats}, est DrawCalls>={uniqueMats} (실측은 Frame Debugger)");
    }

    void OnDestroy()
    {
        if (sharedMat != null)
            Destroy(sharedMat);
    }
}
