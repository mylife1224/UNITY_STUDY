# S02_02_Coroutine_Time

## 학습목표
Update 누적 타이머와 Coroutine(`yield`) 시간 표현의 차이를 이해한다.

## 조작법
1. 빈 오브젝트에 `TimerCoroutine.cs` 부착 후 Play
2. Console에서 Update 1회 완료 vs Coroutine N회 반복 로그 순서 비교
3. 인스펙터/버튼에서 `Restart()` 호출해 재시작 동작 확인

## 태그 찾아보기
### [S02-02-01] Update 누적 타이머
위치: `Scripts/TimerCoroutine.cs:13`
> 매 프레임 체크, 일회성 지연에 불필요한 분기. 상세는 `Stage02 DeepDive [S02-02-01]` 참조.

### [S02-02-02] Coroutine yield 대기
위치: `Scripts/TimerCoroutine.cs:35`
> 분기 없이 시간 흐름 표현, 스케일 시간 영향. 상세는 `Stage02 DeepDive [S02-02-02]` 참조.

### [S02-02-03] 중복 실행 방지
위치: `Scripts/TimerCoroutine.cs:47`
> 중복 Start 시 타이머 배속, Stop 후 재시작. 상세는 `Stage02 DeepDive [S02-02-03]` 참조.

## 확인문제
1. 일회성 2초 지연에 Update 방식이 비효율적인 이유는?
2. `WaitForSeconds`가 일시정지(Time.timeScale=0)에 멈추는 이유는?
3. 같은 Coroutine을 두 번 Start하면 무슨 일이 생기는가?
