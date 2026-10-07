# S06_02_Roguelike_Inventory

## 학습목표
Dictionary 인벤토리 가감 + JsonUtility/PlayerPrefs 저장→로드 복구 흐름 이해

## 조작법
1. 빈 GameObject에 `Roguelike_Inventory.cs`를 붙이고 Play
2. Console 순서 확인: 획득 후 → 사용 후 → 로드 후(복구) 3단계 로그
3. 두 번째 Play에서도 로드 로그가 복구되는지 확인 (PlayerPrefs 유지). 초기화하려면 `PlayerPrefs.DeleteKey("S06_02_Inventory")` 호출

## 태그 찾아보기
### [S06-02-01] Dictionary O(1)
위치: `Scripts/Roguelike_Inventory.cs:19`
> 아이템 조회/가감 O(1). List 선형탐색은 인벤이 커질수록 느려진다.
> 상세: `Stage06 DeepDive [S06-02-01]` 참조.

### [S06-02-02] JSON 세이브
위치: `Scripts/Roguelike_Inventory.cs:57`
> JsonUtility+PlayerPrefs면 파일 경로 고민 없이 저장. Dictionary 직렬화 불가 → List 래퍼 변환.
> 상세: `Stage06 DeepDive [S06-02-02]` 참조.

### [S06-02-03] 로드 실패 대비
위치: `Scripts/Roguelike_Inventory.cs:70`
> 키 없음/깨진 JSON이면 빈 인벤으로 시작. 크래시 대신 기본값 복구가 원칙.
> 상세: `Stage06 DeepDive [S06-02-03]` 참조.

## 확인문제
1. Dictionary를 JsonUtility로 바로 저장하면 왜 안 되는가?
2. 수량이 0이 되면 Remove하는 이유는? (저장 크기/조회 관점)
3. PlayerPrefs 대신 파일 저장이 필요한 경우는? (용량/보안 관점)
