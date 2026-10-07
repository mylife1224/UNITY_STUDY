# S07_01_URP_Optimization

## 학습목표
DrawCall/배칭 개념 이해: 머티리얼 공유가 렌더링 비용에 미치는 영향 체감

## 조작법
1. Play 후 Space: Shared <-> Unique 머티리얼 전환
2. Window > Analysis > Frame Debugger > Enable로 실제 DrawCall 수 비교
3. Profiler Rendering 모듈의 Set Pass Calls / Batches와 대조
4. `Report Estimate` ContextMenu로 추정치 로그 출력

## 태그 찾아보기
### [S07-01-01] 머티리얼 공유 = 배칭 첫 조건
위치: `Scripts/DrawCallProfiler.cs:18`
> 머티리얼이 다르면 같은 메시라도 별도 DrawCall 발생.
> 상세: `Stage07 DeepDive [S07-01-01]` 참조.

### [S07-01-02] material vs sharedMaterial
위치: `Scripts/DrawCallProfiler.cs:43`
> `renderer.material`은 자동 복제(배칭 깨짐), `sharedMaterial`은 공유.
> 상세: `Stage07 DeepDive [S07-01-02]` 참조.

### [S07-01-03] 실측은 Frame Debugger
위치: `Scripts/DrawCallProfiler.cs:60`
> 스크립트 로그는 추정치만. GPU 수치는 에디터 계측기로 확인.
> 상세: `Stage07 DeepDive [S07-01-03]` 참조.

## 확인문제
1. 같은 큐브 200개인데 Unique 모드에서 DrawCall이 늘어나는 이유는?
2. `material`과 `sharedMaterial`의 차이는?
3. SRP Batcher와 동적 배칭의 공통 전제조건은?
