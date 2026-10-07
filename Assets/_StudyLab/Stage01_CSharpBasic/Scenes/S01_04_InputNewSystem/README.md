# S01_04_InputNewSystem

## 학습목표
새 Input System(`UnityEngine.InputSystem`)으로 WASD 이동을 구현한다.

## 조작법
1. Package Manager에서 Input System 패키지 설치 확인 후 Play
2. 빈 오브젝트에 `InputReader.cs` 부착, W/A/S/D + 방향키로 이동
3. 대각선 이동 속도가 직선과 같은지 확인, Space 입력 로그 확인

## 태그 찾아보기
### [S01-04-01] Keyboard 직접 폴링
위치: `Scripts/InputReader.cs:10`
> 기존 Input 대신 InputSystem 사용, 리바인딩 용이. 상세는 `Stage01 DeepDive [S01-04-01]` 참조.

### [S01-04-02] 대각선 속도 보정
위치: `Scripts/InputReader.cs:39`
> 정규화 없이 대각선은 루트2배 빠름. 상세는 `Stage01 DeepDive [S01-04-02]` 참조.

### [S01-04-03] 단발 입력 edge 감지
위치: `Scripts/InputReader.cs:48`
> 점프 등은 wasPressedThisFrame 권장. 상세는 `Stage01 DeepDive [S01-04-03]` 참조.

## 확인문제
1. `Keyboard.current`가 null일 수 있는 상황은?
2. 대각선 이동이 빨라지는 수학적 이유는?
3. 이동(지속)과 점프(단발)의 입력 처리 방식이 달라야 하는 이유는?
