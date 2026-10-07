# S05_01_Sort_Bar

## 학습목표
버블/삽입 정렬 과정을 코루틴 스텝 + Gizmos 막대로 눈으로 이해

## 조작법
1. 빈 GameObject에 `SortVisualizer.cs`를 붙이고 Play
2. Scene뷰에서 빨간 막대(비교 중)가 움직이며 정렬되는지 확인, Console에서 swaps/shifts 로그 확인
3. Inspector에서 `Use Insertion` 체크 후 다시 Play → 두 방식 로그 비교

## 태그 찾아보기
### [S05-01-01] 코루틴 스텝
위치: `Scripts/SortVisualizer.cs:27`
> `WaitForSeconds`로 한 스텝씩 끊으면 비교→교환→대기 순서가 눈에 보인다.
> 상세: `Stage05 DeepDive [S05-01-01]` 참조.

### [S05-01-02] 버블 vs 삽입
위치: `Scripts/SortVisualizer.cs:40`
> 버블은 인접 교환이라 매 스텝 변화가 보이고, 삽입은 정렬 구간에선 비교만 하고 넘어간다.
> 상세: `Stage05 DeepDive [S05-01-02]` 참조.

### [S05-01-03] Gizmos 막대
위치: `Scripts/SortVisualizer.cs:71`
> 큐브 Instantiate 대신 Gizmos로 그리면 생성/정리 작업 없이 Scene뷰 확인 가능.
> 상세: `Stage05 DeepDive [S05-01-03]` 참조.

## 확인문제
1. 코루틴 대신 Update 플래그로 스텝을 끊으면 순서 표현이 왜 어려워지는가?
2. 이미 정렬된 배열에서 버블과 삽입 중 어느 쪽이 빨리 끝나는가? (이유 포함)
3. 막대 개수를 100개로 늘리면 Gizmos 방식과 Instantiate 방식 중 어느 쪽이 유리한가?
