using UnityEngine;
using System.Collections.Generic;

// S04_03 씬 전용: LinkedList 기차 꼬리 (양끝 삽입/삭제 O(1))
public class LinkedList_Train : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private int carCount = 5;
    [SerializeField] private float carGap = 1.5f;
    [SerializeField] private float moveSpeed = 5f;

    // [S04-03-01] WHY: 기차 칸은 양끝 추가/삭제 반복, LinkedList가 O(1)
    // List 맨 앞 삽입은 전체 복사 O(n), 노드 참조 연결은 포인터 교체뿐
    // README [S04-03-01], DeepDive [S04-03-01] 참조
    private readonly LinkedList<Transform> cars = new LinkedList<Transform>();

    void Start()
    {
        for (int i = 0; i < carCount; i++)
            AddCarTail();
    }

    // [S04-03-02] WHY: AddLast/RemoveLast는 노드 포인터만 교체
    // 인덱스 개념이 없어 n번째 접근은 O(n), 순회는 foreach 전용
    // README [S04-03-02], DeepDive [S04-03-02] 참조
    public void AddCarTail()
    {
        Vector3 pos = cars.Count == 0
            ? transform.position - transform.forward * carGap
            : cars.Last.Value.position - transform.forward * carGap;
        GameObject car = carPrefab != null
            ? Instantiate(carPrefab, pos, Quaternion.identity)
            : GameObject.CreatePrimitive(PrimitiveType.Cube);
        car.transform.position = pos;
        car.name = $"Car_{cars.Count}";
        cars.AddLast(car.transform);
        Debug.Log($"[S04-03-02] 칸 추가, 총 {cars.Count}칸");
    }

    public void RemoveCarTail()
    {
        if (cars.Count == 0) return;
        Transform tail = cars.Last.Value;
        cars.RemoveLast();
        Destroy(tail.gameObject);
        Debug.Log($"[S04-03-02] 칸 제거, 총 {cars.Count}칸");
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.position += new Vector3(h, 0f, v) * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.T)) AddCarTail();
        if (Input.GetKeyDown(KeyCode.G)) RemoveCarTail();

        Transform leader = transform;
        foreach (Transform car in cars)
        {
            Vector3 toLeader = leader.position - car.position;
            float dist = toLeader.magnitude;
            if (dist > carGap)
                car.position += toLeader.normalized * (dist - carGap);
            leader = car;
        }
    }
}
