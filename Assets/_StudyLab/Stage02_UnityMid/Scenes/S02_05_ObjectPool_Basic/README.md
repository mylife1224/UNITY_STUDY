# S02_05_ObjectPool_Basic

## 학습목표
`Queue<GameObject>` 풀로 Instantiate/Destroy 반복을 SetActive 재사용으로 바꾼다.

## 조작법
1. 빈 오브젝트에 `SimpleObjectPool.cs` 부착, bulletPrefab 연결 후 Play
2. `Get(위치)`으로 꺼내고 `Release(오브젝트)`로 반환하는 흐름 테스트
3. Profiler/Console로 반복 발사 시 GC 스파이크 감소 개념 확인

## 태그 찾아보기
### [S02-05-01] Queue 선입선출 재사용
위치: `Scripts/SimpleObjectPool.cs:10`
> Dequeue O(1), 편향 없는 순환. 상세는 `Stage02 DeepDive [S02-05-01]` 참조.

### [S02-05-02] SetActive 재사용
위치: `Scripts/SimpleObjectPool.cs:35`
> 생성/파괴 대신 활성 토글, 빈 풀은 신규 생성. 상세는 `Stage02 DeepDive [S02-05-02]` 참조.

### [S02-05-03] 반환 시 null 가드
위치: `Scripts/SimpleObjectPool.cs:51`
> 파괴된 객체 재등록 방지. 상세는 `Stage02 DeepDive [S02-05-03]` 참조.

## 확인문제
1. 매번 Instantiate/Destroy하면 성능상 어떤 문제가 생기는가?
2. 풀 자료구조로 List 선두 삭제 대신 Queue를 쓰는 이유는?
3. 풀에서 꺼낸 오브젝트의 위치/상태를 초기화해야 하는 이유는?
