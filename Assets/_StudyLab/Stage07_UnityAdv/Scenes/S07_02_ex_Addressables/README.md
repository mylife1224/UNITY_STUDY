# S07_02_ex_Addressables

## 학습목표
실제 Addressables 로드/해제. 개념씬 S07_02와 나란히 비교.

## 사전설정 (최초 1회)
1. Window → Asset Management → Addressables → Groups
2. Groups 창에 빈 프리팹(예: Cube) 드래그 → 새 그룹 생성
3. Address를 `Enemy_Goblin`으로 변경 (Lab의 Address Key와 일치)
4. Play Mode Script는 기본 `Use Asset Database` 유지 → 빌드 없이 Play에서 바로 로드됨

## 조작법
1. Lab의 Address Key 확인 후 Play
2. Lab 우클릭 → `Load` (스폰 확인) → `Release` (해제 확인)
3. 키를 일부러 틀리게 바꾸고 Load → 실패 로그 확인

## 태그 찾아보기
### [S07-02-EX-01] 핸들 보관 이유
위치: `Scripts/AddressableLoader_Real.cs:13`
> Release 추적용. 미해제 반복 = 메모리 누수.

### [S07-02-EX-02] Status 분기
위치: `Scripts/AddressableLoader_Real.cs:30`
> 키 오타/미등록이 주 실패 원인. Failed 로그에 키 포함.

## 확인문제
1. Release 없이 Load를 반복하면 무슨 일이 생기는가?
2. `Use Asset Database` 모드와 빌드 후 로드의 차이는?
3. 같은 에셋을 두 번 Load하면 핸들은 공유되는가?
