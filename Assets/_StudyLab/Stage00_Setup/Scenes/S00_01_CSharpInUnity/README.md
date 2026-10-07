# S00_01_CSharpInUnity

## 학습목표
Unity 생명주기(Awake/Start/Update) + 순수 C# 클래스 분리

## 조작법
1. 빈 오브젝트에 `HelloCSharp.cs` 부착 후 Play
2. Console에서 로그 순서 확인

## 태그 찾아보기
### [S00-01-01] 데이터 분리 이유
위치: `Scripts/HelloCSharp.cs:9`
> MonoBehaviour에 전부 넣으면 재사용 불가. 상세는 `Stage00 DeepDive [S00-01-01]` 참조.

### [S00-01-02] 생명주기 순서
위치: `Scripts/HelloCSharp.cs:16`
> Awake -> Start -> Update. 생성자 대신 사용.

### [S00-01-03] Update 주의점
위치: `Scripts/HelloCSharp.cs:28`
> 매 프레임 호출되므로 new/Find 금지.

## 확인문제
1. Start와 Awake 차이는?
2. PlayerData를 MonoBehaviour로 만들면 안 되는 이유는?
3. Update에 Debug.Log를 넣으면 안 되는 이유는?
