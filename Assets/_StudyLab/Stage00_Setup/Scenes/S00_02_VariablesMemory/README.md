# S00_02_VariablesMemory

## 학습목표
값형식(struct) vs 참조형식(class), SerializeField 이해

## 조작법
1. `VariablesMemory.cs` 부착 후 Play, Console 확인

## 태그 찾아보기
### [S00-02-01] 값 vs 참조 복사
위치: `Scripts/VariablesMemory.cs:14`
> Vector3(struct)는 복사, class는 참조 공유. DeepDive [S00-02-01] 참조.

### [S00-02-02] SerializeField 이유
위치: `Scripts/VariablesMemory.cs:8`
> private 유지 + 인스펙터 노출. public 남발 금지. DeepDive [S00-02-02] 참조.

## 확인문제
1. Vector3 b를 바꿔도 a가 안 바뀌는 이유는?
2. r1, r2가 같이 바뀌는 이유는?
3. public int hp와 [SerializeField] private int hp 차이는?
