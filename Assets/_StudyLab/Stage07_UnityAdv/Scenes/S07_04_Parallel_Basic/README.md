# S07_04_Parallel_Basic

## 학습목표
10k 이동 연산으로 순차 vs `Tasks.Parallel` 비교 + Job/Burst 전환 개념

## 조작법
1. Play 후 Space: 순차 for 실행 (ms 로그)
2. P: `Parallel.For` 실행 (ms 로그)
3. `count`를 1000 / 10000 / 100000으로 바꿔 차이 비교
4. 너무 작으면 병렬이 느릴 수 있음 (오버헤드 체감 포인트)

## 태그 찾아보기
### [S07-04-01] Parallel.For 분할
위치: `Scripts/ParallelMoveDemo.cs:48`
> 인덱스 구간을 스레드에 분할 후 합류. Unity API는 Parallel 안에서 호출 금지.
> 상세: `Stage07 DeepDive [S07-04-01]` 참조.

### [S07-04-02] Job/Burst 매핑표
위치: `Scripts/ParallelMoveDemo.cs:61`
> Parallel 본문 -> IJobParallelFor, 배열 -> NativeArray, 계산은 Job/적용은 메인.
> 상세: `Stage07 DeepDive [S07-04-02]` 참조.

## 확인문제
1. 데이터가 작을 때 병렬이 오히려 느린 이유는? (스레드풀 오버헤드)
2. Parallel 안에서 `transform.position`을 건드리면 안 되는 이유는?
3. 계산 결과를 Transform에 반영하는 올바른 순서는?
