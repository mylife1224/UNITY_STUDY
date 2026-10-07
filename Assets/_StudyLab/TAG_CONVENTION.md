# 태그 컨벤션

## 형식
`[SXX-YY-ZZ]` = [Stage-Scene-일련번호]
예: `[S04-02-01]` = Stage04, 2번씬, 1번 주석

## 사용법
1. 코드에 태그 부착:
```csharp
// [S04-02-01] WHY: List 대신 Queue 사용
```
2. 씬 README에서 같은 태그로 설명:
```md
### [S04-02-01] Queue 선택 이유
위치: `Scripts/Queue_Spawner.cs:18`
```
3. DeepDive에서 심화 설명:
```md
### [S04-02-01] 심화: 원형버퍼
```

## 규칙
- 태그는 삭제/재사용 금지, 줄번호가 바뀌어도 ID 유지
- WHY는 3줄 이내, 상세는 README/DeepDive에
- 검색: Visual Studio / Rider에서 `Ctrl+Shift+F` 로 `[S04-02-01]` 검색
