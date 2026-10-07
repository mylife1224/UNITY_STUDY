# S01_01_Movement_IfLoop

## 학습목표
if/else와 for로 캐릭터 이동 + 영역 제한을 구현한다.

## 조작법
1. 빈 오브젝트에 `PlayerMover_Basic.cs` 부착 후 Play
2. W/A/S/D 키로 이동, ±4 범위를 벗어나지 않는지 확인
3. 인스펙터에서 speed/range 값을 바꿔 동작 비교

## 태그 찾아보기
### [S01-01-01] 프레임 독립 이동
위치: `Scripts/PlayerMover_Basic.cs:9`
> 키 입력 누적 + `Time.deltaTime` 이동. 상세는 `Stage01 DeepDive [S01-01-01]` 참조.

### [S01-01-02] if 경계 고정
위치: `Scripts/PlayerMover_Basic.cs:25`
> 물리 없이 if로 위치 클램프. 상세는 `Stage01 DeepDive [S01-01-02]` 참조.

### [S01-01-03] for 축별 클램프
위치: `Scripts/PlayerMover_Basic.cs:30`
> 배열+for로 축 확장 가능 구조. 상세는 `Stage01 DeepDive [S01-01-03]` 참조.

## 확인문제
1. `Time.deltaTime`을 곱하지 않으면 프레임마다 속도가 어떻게 달라지는가?
2. `else if` 대신 `if`를 2개 쓰면 A+D 동시 입력 시 결과는?
3. 이동 범위를 원형으로 바꾸려면 `ClampInside`를 어떻게 고쳐야 하는가?
