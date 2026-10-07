# S08_03_Testing_Readme

## 학습목표
테스트 가능한 순수 로직 분리: UnityEngine 미의존 클래스의 가치 체감

## 조작법
1. Play 후 A: Potion 획득, U: 사용, C: 개수 출력
2. 용량(8)까지 다른 종류를 채운 뒤 초과 획득 시도 (실패 로그 확인)
3. Lab 우클릭 → `Run Self Tests` (Play 없이 경계값 4건 검증, PASS/FAIL 로그)
4. `InventoryLogic` 클래스가 `UnityEngine`을 참조하지 않음을 코드로 확인

## 태그 찾아보기
### [S08-03-01] 순수 로직 분리
위치: `Scripts/InventoryDemo.cs:30`
> UnityEngine 미참조 -> NUnit EditMode에서 Play 없이 검증 가능.
> 상세: `Stage08 DeepDive [S08-03-01]` 참조.

### [S08-03-02] 자가테스트 4건
위치: `Scripts/InventoryDemo.cs:62`
> Run Self Tests ContextMenu로 경계값 검증. NUnit 전환 시 [Test]로 이전.
> 상세: `Stage08 DeepDive [S08-03-02]` 참조.

## 확인문제
1. 로직이 MonoBehaviour 안에 있으면 테스트가 어려운 이유는? (Play 의존)
2. EditMode 테스트와 PlayMode 테스트의 차이는?
3. `InventoryDemo`와 `InventoryLogic`의 책임 분리를 한 줄로 설명하면?
