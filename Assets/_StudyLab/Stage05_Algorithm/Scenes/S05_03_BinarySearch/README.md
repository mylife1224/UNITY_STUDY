# S05_03_BinarySearch

## 학습목표
UpDown 게임을 자동 탐색으로 돌려 이진탐색 O(log n) 체감

## 조작법
1. 빈 GameObject에 `BinarySearchDemo.cs`를 붙이고 Play
2. Console에서 mid가 절반씩 좁혀지는 로그 확인 (100개 → 최대 7회)
3. `Secret` 값을 바꿔 다시 Play, `Guess(n)`을 버튼/다른 스크립트에서 호출해 수동 플레이 가능

## 태그 찾아보기
### [S05-03-01] 절반씩 버리기
위치: `Scripts/BinarySearchDemo.cs:33`
> `mid=(lo+hi)/2`로 Up/Down 응답마다 탐색 구간을 절반으로 좁히는 것이 이진탐색 본질.
> 상세: `Stage05 DeepDive [S05-03-01]` 참조.

### [S05-03-02] O(log n)
위치: `Scripts/BinarySearchDemo.cs:50`
> 100개면 최대 7번(2^7=128). 선형탐색 최악 100번과 Console 스텝 수로 비교.
> 상세: `Stage05 DeepDive [S05-03-02]` 참조.

### [S05-03-03] 정렬 전제조건
위치: `Scripts/BinarySearchDemo.cs:24`
> 정렬 안 된 데이터에 쓰면 정답을 놓친다. 미정렬이면 선형탐색으로 대체.
> 상세: `Stage05 DeepDive [S05-03-03]` 참조.

## 확인문제
1. 1000개 범위면 이진탐색 최대 몇 회인가? (2의 거듭제곱으로 계산)
2. 정렬되지 않은 배열에 이진탐색을 쓰면 왜 실패하는가? (구체적 예시 포함)
3. `mid=(lo+hi)/2`에서 lo+hi가 int 범위를 넘으면? (오버플로 회피식은?)
