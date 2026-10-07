# S05_02_BFSDFS_Maze

## 학습목표
같은 미로에 BFS(Queue)와 DFS(Stack)를 돌려 경로 길이 차이로 최단보장 유무 이해

## 조작법
1. 빈 GameObject에 `Maze_BFSDFS.cs`를 붙이고 Play
2. Scene뷰 색상 확인: 초록=시작, 빨강=목표, 노랑=DFS 경로, 하늘=BFS 방문 영역
3. Console에서 BFS 방문 수 vs DFS 경로 길이 비교

## 태그 찾아보기
### [S05-02-01] BFS는 Queue
위치: `Scripts/Maze_BFSDFS.cs:48`
> FIFO라 시작점에서 가까운 순으로 퍼진다. 먼저 도착한 경로가 최단거리 보장.
> 상세: `Stage05 DeepDive [S05-02-01]` 참조.

### [S05-02-02] DFS는 Stack
위치: `Scripts/Maze_BFSDFS.cs:75`
> LIFO라 한 방향으로 끝까지 파고든다. 최단 보장 없음, 미로 생성처럼 깊이 우선이 필요할 때 사용.
> 상세: `Stage05 DeepDive [S05-02-02]` 참조.

### [S05-02-03] 방문 집합은 HashSet
위치: `Scripts/Maze_BFSDFS.cs:51`
> `List.Contains`는 O(n)이라 격자가 커지면 BFS가 느려진다. HashSet O(1) 조회 사용.
> 상세: `Stage05 DeepDive [S05-02-03]` 참조.

## 확인문제
1. BFS가 최단경로를 보장하는 이유는? (Queue 순서로 설명)
2. DFS 경로가 BFS보다 길어지는 경우는 언제인가?
3. 방문 체크를 빠뜨리면 어떤 일이 생기는가? (사이클 관점에서 설명)
