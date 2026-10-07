# S07_05_Profiler_GC

## 학습목표
GC.Alloc 유발 3대 패턴 식별 + StringBuilder/캐싱 처방 체감

## 조작법
1. Play 후 B: 나쁜 패턴 (할당량 KB 로그)
2. G: 개선 패턴 (할당량 KB 로그, B와 비교)
3. H: 캐싱된 WaitForSeconds 코루틴 3회 박동
4. Profiler Memory/CPU 모듈의 GC Alloc 열과 대조

## 태그 찾아보기
### [S07-05-01] GC 유발 3대 패턴
위치: `Scripts/GCFreeDemo.cs:32`
> string += 반복, 매 프레임 GetComponent, 박싱/클로저 남발.
> 상세: `Stage07 DeepDive [S07-05-01]` 참조.

### [S07-05-02] 3대 처방
위치: `Scripts/GCFreeDemo.cs:47`
> StringBuilder 재사용, 컴포넌트 캐싱, yield 캐싱.
> 상세: `Stage07 DeepDive [S07-05-02]` 참조.

## 확인문제
1. `s += ...` 반복이 O(n^2) 할당을 유발하는 이유는?
2. `new WaitForSeconds`를 매번 생성하면 안 되는 이유는?
3. `GC.GetTotalMemory` 전후 비교가 정확한 실측인가? (근사치인 이유)
