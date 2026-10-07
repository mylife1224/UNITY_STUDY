# S02_04_ScriptableObject

## 학습목표
ScriptableObject로 데이터를 에셋 분리하고 인벤토리가 참조만 보관하게 한다.

## 조작법
1. Project 우클릭 > Create > StudyLab > Item Data 로 아이템 에셋 2개 생성
2. 빈 오브젝트에 `Inventory_Basic.cs` 부착, items에 에셋 드래그 후 Play
3. `AddItem`/`PrintAll` 호출(테스트 버튼/Console)해 획득 로그 확인

## 태그 찾아보기
### [S02-04-01] 데이터 에셋 분리
위치: `Scripts/ItemDataSO.cs:18`
> 코드 수정 없이 수치 조정. 상세는 `Stage02 DeepDive [S02-04-01]` 참조.

### [S02-04-02] OnValidate 값 검증
위치: `Scripts/ItemDataSO.cs:26`
> 음수 가격 등 실수 사전 차단. 상세는 `Stage02 DeepDive [S02-04-02]` 참조.

### [S02-04-03] SO 참조만 보관
위치: `Scripts/Inventory_Basic.cs:10`
> 복사본 금지, 원본 수정이 그대로 반영. 상세는 `Stage02 DeepDive [S02-04-03]` 참조.

## 확인문제
1. 아이템 가격을 코드에 하드코딩하면 밸런스 패치 때마다 무슨 작업이 필요한가?
2. 인벤토리가 아이템 복사본을 가지면 SO 원본 수정 시 어떤 문제가 생기는가?
3. `OnValidate`는 언제 호출되며 왜 검증용으로 적합한가?
