# S02_03_UI_Event

## 학습목표
Button onClick 연결과 `Action` 이벤트 분리로 UI-로직 결합도를 낮춘다.

## 조작법
1. Canvas + Button + Text 구성, 오브젝트에 `UIManager_Basic.cs` 부착 후 연결
2. Play 후 Button 클릭마다 Score +10 및 Text 갱신 확인
3. 연결을 일부러 빼고 Play해 경고 로그 확인

## 태그 찾아보기
### [S02-03-01] Action 이벤트 분리
위치: `Scripts/UIManager_Basic.cs:13`
> UI와 게임 로직 결합도 감소. 상세는 `Stage02 DeepDive [S02-03-01]` 참조.

### [S02-03-02] 코드 AddListener
위치: `Scripts/UIManager_Basic.cs:25`
> 인스펙터 연결 대신 코드 등록, 누락 즉시 발견. 상세는 `Stage02 DeepDive [S02-03-02]` 참조.

### [S02-03-03] 구독 해제
위치: `Scripts/UIManager_Basic.cs:39`
> 미해제 시 누수/중복 호출/예외. 상세는 `Stage02 DeepDive [S02-03-03]` 참조.

## 확인문제
1. 점수 변경을 직접 Text에 쓰지 않고 이벤트로 알리는 이유는?
2. `OnScoreChanged?.Invoke`에서 `?.`가 필요한 이유는?
3. `OnDestroy`에서 구독 해제를 빼먹으면 씬 전환 시 무슨 일이 생기는가?
