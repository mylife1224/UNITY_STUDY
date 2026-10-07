# S05_05_AStar_GridMove

## 학습목표
맨해튼 휴리스틱 A*로 경로를 구하고 유닛(자기 자신)을 코루틴으로 이동

## 조작법
1. 씬에 큐브 1개를 놓고 `AStar_Pathfinding.cs`를 붙인 뒤 Play
2. Scene뷰 색상 확인: 초록=시작, 빨강=목표, 노랑=경로, 파랑=유닛, 검정=벽
3. 경로가 0칸이면 벽에 막힌 것 — Inspector 시드(코드 내 Random 값)를 바꾸거나 벽 확률 조정

## 태그 찾아보기
### [S05-05-01] 맨해튼 휴리스틱
위치: `Scripts/AStar_Pathfinding.cs:55`
> 4방향 이동의 정확한 하한이라 과대추정하지 않음(admissible) → 최단경로 보장.
> 상세: `Stage05 DeepDive [S05-05-01]` 참조.

### [S05-05-02] F=g+h 최소 우선
위치: `Scripts/AStar_Pathfinding.cs:63`
> BFS(전부 펼침)와 탐욕(휴리스틱만) 사이의 균형이 A* 핵심.
> 상세: `Stage05 DeepDive [S05-05-02]` 참조.

### [S05-05-03] 경로와 이동 분리
위치: `Scripts/AStar_Pathfinding.cs:96`
> 경로 재계산 없이 `moveDelay`만 바꾸면 유닛 속도 조절. 경로=데이터, 이동=표현.
> 상세: `Stage05 DeepDive [S05-05-03]` 참조.

## 확인문제
1. 대각선 이동을 허용하면 맨해튼 대신 어떤 휴리스틱을 써야 하는가?
2. 휴리스틱을 과대추정하면(예: 2배) 속도와 정확도에 무슨 일이 생기는가?
3. open 목록을 List+Sort 대신 힙으로 바꾸면 복잡도가 어떻게 달라지는가?
