# S06_03_Puzzle_BFS

## 학습목표
3x3 슬라이딩 퍼즐에 BFS 힌트(다음 1수)를 깊이/노드 제한 안에서 계산

## 조작법
1. 빈 GameObject에 `SlidingPuzzle_BFS.cs`를 붙이고 Play
2. Console에서 섞인 보드(3행 출력) + 힌트 방향(Up/Down/Left/Right) 확인
3. Inspector에서 `Shuffle Moves`를 늘리면 힌트가 "해답이 깊음"으로 바뀌는 것 확인

## 태그 찾아보기
### [S06-03-01] 상태 문자열 BFS
위치: `Scripts/SlidingPuzzle_BFS.cs:62`
> 보드를 문자열 키+HashSet 방문으로 BFS. 9! 전체 탐색 대신 제한 안에서 1수만 계산.
> 상세: `Stage06 DeepDive [S06-03-01]` 참조.

### [S06-03-02] 깊이/노드 상한
위치: `Scripts/SlidingPuzzle_BFS.cs:72`
> 상한 없이 돌리면 프레임 프리즈. 모바일 저사양 대응의 핵심.
> 상세: `Stage06 DeepDive [S06-03-02]` 참조.

### [S06-03-03] 풀 수 있게 섞기
위치: `Scripts/SlidingPuzzle_BFS.cs:45`
> 랜덤 배치는 절반이 불가 판정. 정답에서 유효 이동만 섞으면 항상 풀 수 있음.
> 상세: `Stage06 DeepDive [S06-03-03]` 참조.

## 확인문제
1. 3x3 전체 상태 수(9!)를 전부 BFS하면 메모리/시간에 무슨 일이 생기는가?
2. 풀 수 없는 배치를 판정하는 수학적 방법은? (역전 수 관점)
3. BFS 대신 A*(맨해튼 합)를 쓰면 힌트 계산이 어떻게 빨라지는가?
