# S07_04_ex_JobBurst

## 학습목표
IJobParallelFor + Burst 실측. 개념씬 S07_04(Tasks.Parallel)와 비교.

## 조작법
1. Play (첫 실행 수 초 정지 = Burst 웜업 컴파일, 정상)
2. Console 60프레임 간격 로그에서 Job+Burst ms 확인
3. Space → 메인루프 모드 전환 후 ms 비교

## 태그 찾아보기
### [S07-04-EX-01] 잡 구조체 규칙
위치: `Scripts/ParallelMoveJob_Real.cs:18`
> 값형식+blittable만. class/string 포함 시 Burst 불가. Persistent는 반드시 Dispose.

### [S07-04-EX-02] Schedule+Complete
위치: `Scripts/ParallelMoveJob_Real.cs:52`
> 배치 64로 분할 실행. Complete까지 같은 프레임 호출(데모용).

## 확인문제
1. 첫 실행만 느린 이유는? (웜업)
2. count를 100으로 줄이면 Job이 오히려 느린 이유는? (스케줄 오버헤드)
3. Dispose를 빼먹으면 에디터에 무슨 경고가 뜨는가?
