# S06_01_TowerDefense

## 학습목표
타워디펜스 한 씬에 Queue 웨이브 + 우선순위 타겟팅 + 오브젝트 풀 3개 개념 통합

## 조작법
1. 빈 GameObject에 `TowerDefense_Main.cs`를 붙이고 Play (prefab 없어도 Capsule로 자동 생성)
2. Console에서 웨이브 출격 → 처치 → 풀 크기 로그 확인
3. Inspector에서 `Tower Range/Damage`를 바꿔 처치 속도 변화 확인. 풀 크기가 웨이브 후에도 유지되면 풀링 성공

## 태그 찾아보기
### [S06-01-01] Queue 웨이브
위치: `Scripts/TowerDefense_Main.cs:56`
> 순서 보장 FIFO, Dequeue O(1). 웨이브 스킵/삽입이 필요해지면 List로 교체.
> 상세: `Stage06 DeepDive [S06-01-01]` 참조.

### [S06-01-02] 우선순위 타겟팅
위치: `Scripts/TowerDefense_Main.cs:83`
> 거리 최소 타겟 = PriorityQueue 개념. 소규모는 OrderBy로 충분, 수백 기면 힙으로 교체.
> 상세: `Stage06 DeepDive [S06-01-02]` 참조.

### [S06-01-03] 오브젝트 풀
위치: `Scripts/TowerDefense_Main.cs:40`
> Instantiate GC spikes 회피, 비활성 객체 재사용. Destroy/생성 반복 없음.
> 상세: `Stage06 DeepDive [S06-01-03]` 참조.

## 확인문제
1. 보스를 중간에 끼워 넣으려면 Queue 대신 무엇을 써야 하는가?
2. 적이 500기일 때 OrderBy 매 프레임 호출의 문제점은? (대안은?)
3. 풀 크기가 무한히 커지는 것을 막으려면? (상한/프리팹 공유 관점)
