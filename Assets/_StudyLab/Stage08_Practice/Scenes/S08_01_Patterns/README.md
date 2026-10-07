# S08_01_Patterns

## 학습목표
실무 5대 패턴 (Singleton, Observer, State, Factory, Command)을 한 파일 데모로 체감

## 조작법
1. Play 후 Space: 골드 획득 + 피격 + 미니언 스폰/전이 로그 확인
2. Z: 마지막 Command Undo (골드 되돌리기)
3. `Patterns_All.cs` 한 파일을 위에서 아래로 읽으며 5개 태그 위치 확인

## 태그 찾아보기
### [S08-01-01] Singleton
위치: `Scripts/Patterns_All.cs:39`
> 전역 단일 접점. 남용 시 결합도 상승. Gold 지갑처럼 1개 보장할 때만.
> 상세: `Stage08 DeepDive [S08-01-01]` 참조.

### [S08-01-02] Observer(event)
위치: `Scripts/Patterns_All.cs:54`
> polling 없이 변경 시에만 통지. UI 갱신의 정석.
> 상세: `Stage08 DeepDive [S08-01-02]` 참조.

### [S08-01-03] State
위치: `Scripts/Patterns_All.cs:72`
> if-else 분기 폭발 방지. 전이는 Change()로만.
> 상세: `Stage08 DeepDive [S08-01-03]` 참조.

### [S08-01-04] Factory
위치: `Scripts/Patterns_All.cs:90`
> 생성 조건 분기를 한 곳에. 신적 추가 시 팩토리만 수정 (OCP).
> 상세: `Stage08 DeepDive [S08-01-04]` 참조.

### [S08-01-05] Command
위치: `Scripts/Patterns_All.cs:102`
> 요청을 객체로 캡슐화. Undo/큐잉/리플레이 가능.
> 상세: `Stage08 DeepDive [S08-01-05]` 참조.

## 확인문제
1. Singleton 남용이 테스트를 어렵게 만드는 이유는?
2. event 구독 해제(`-=`)를 빼먹으면 생기는 문제는?
3. Command가 Undo를 가능하게 하는 구조적 이유는?
