# S04_02_Queue_WaveSpawn

## 학습목표
Queue(FIFO)로 웨이브 순서 보장 + O(1) 꺼내기 이해

## 조작법
Play 후 Console에서 Wave1->2->3 순서대로 스폰되는지 확인

## 태그 찾아보기
### [S04-02-01] Queue 선택 이유
위치: `Scripts/Queue_Spawner.cs:12`
> List.RemoveAt(0)은 O(n) 복사, Queue.Dequeue()는 O(1).
> 이유: Queue 내부는 배열+head/tail 원형버퍼, 꺼낼 때 데이터 이동 없음.
> 가득 차면 2배 증설만 발생 (분할상환 O(1)).
> 상세: `Stage04 DeepDive [S04-02-01]` 참조.

### [S04-02-02] Queue 한계
위치: `Scripts/Queue_Spawner.cs:19`
> 중간 삭제/인덱스 접근 불가. 필요시 List(중간삭제) / PriorityQueue(우선순위)로 교체.

## 확인문제
1. RemoveAt(0)이 O(n)인 이유는?
2. Dequeue가 O(1)인 이유는? (원형버퍼로 설명)
3. 보스를 먼저 내보내려면 무슨 구조로 바꿔야 하는가?
