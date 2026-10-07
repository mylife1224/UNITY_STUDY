# S07_02_Addressables_Concept

## 학습목표
Resources 비동기 로드 구조 이해 + Addressables 전환 매핑 학습 (패키지 없이)

## 조작법
1. `Resources/Enemies/Slime.prefab` 준비 후 assetPath에 `Enemies/Slime` 입력
2. Play 후 Space: 비동기 로드 (진행률 로그 확인)
3. R: 인스턴스 파괴 + 미사용 에셋 해제
4. 존재하지 않는 경로 입력 시 에러 로그 경로 확인

## 태그 찾아보기
### [S07-02-01] 비동기 로드
위치: `Scripts/AddressableLoader_Concept.cs:14`
> `Resources.Load`는 동기(끊김), `LoadAsync`는 프레임 분할.
> Addressables 전환 시 코루틴 구조 그대로 `LoadAssetAsync`로 교체.
> 상세: `Stage07 DeepDive [S07-02-01]` 참조.

### [S07-02-02] 명시적 해제
위치: `Scripts/AddressableLoader_Concept.cs:46`
> Destroy만으로 에셋 메모리 해제 안 됨. `UnloadUnusedAssets` 필요.
> Addressables에서는 `Release(handle)`로 1:1 대응.
> 상세: `Stage07 DeepDive [S07-02-02]` 참조.

### [S07-02-03] 전환 매핑표
위치: `Scripts/AddressableLoader_Concept.cs:60`
> 경로 문자열 의존 -> Address 키 + 그룹/라벨 관리로 전환.
> 상세: `Stage07 DeepDive [S07-02-03]` 참조.

## 확인문제
1. `Load`와 `LoadAsync`의 프레임 영향 차이는?
2. Destroy 후에도 메모리가 남는 이유는?
3. Addressables의 Address가 경로 문자열보다 나은 점은?
