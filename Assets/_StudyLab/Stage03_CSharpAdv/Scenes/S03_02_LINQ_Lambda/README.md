# S03_02_LINQ_Lambda

## 학습목표
LINQ Where/OrderBy + 람다로 적 필터링 선언형 작성

## 조작법
1. 빈 오브젝트에 `EnemyFilter_LINQ.cs` 부착 후 Play
2. `Seed Dummy Enemies` (ContextMenu)로 더미 5기 생성
3. Console에서 0.25초 간격 타겟 로그 확인

## 태그 찾아보기
### [S03-02-01] 선언형 필터 선택 이유
위치: `Scripts/EnemyFilter_LINQ.cs:13`
> Where+OrderBy 체이닝은 의도가 코드에 드러남. for+if+Sort 중첩보다 읽기 쉽고 조건 추가가 한 줄.
> 상세: `Stage03 DeepDive [S03-02-01]` 참조.

### [S03-02-02] 지연 실행과 GC 주의
위치: `Scripts/EnemyFilter_LINQ.cs:25`
> LINQ는 지연 실행, ToList() 시점에 한 번만 돎. 매 프레임 ToList()는 GC 유발이므로 0.25초 캐싱.
> 상세: `Stage03 DeepDive [S03-02-02]` 참조.

### [S03-02-03] 람다 캡처 주의
위치: `Scripts/EnemyFilter_LINQ.cs:44`
> 루프 변수 직접 캡처는 전원 같은 값 참조. 지역 복사 후 캡처로 클로저 누수 방지.
> 상세: `Stage03 DeepDive [S03-02-03]` 참조.

## 확인문제
1. LINQ 지연 실행이란 무엇이며 ToList()는 언제 실행되는가?
2. 매 프레임 ToList()를 호출하면 안 되는 이유는?
3. 람다가 루프 변수를 캡처할 때 생기는 문제는?
