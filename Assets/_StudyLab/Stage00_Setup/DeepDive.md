# Stage00 DeepDive — 환경 + C# 마인드셋

## 목표
Unity에서 C#이 어떻게 실행되는지 이해한다.

### [S00-01-01] 심화: MonoBehaviour vs 순수 C# 클래스
- MonoBehaviour는 씬에 붙어야 동작, Unity가 생명주기 함수 호출
- 순수 클래스는 new로 생성, 로직/데이터 담당
- 초급: 붙여서 실행해보기 / 중급: 의존성 분리 / 고급: 메모리 관점에서 MGMT

### [S00-02-01] 심화: 값형식 vs 참조형식
- struct(int, float, Vector3): 스택에 복사, 대입시 값이 복사됨
- class(GameObject, MonoBehaviour, string): 힙에 저장, 참조만 복사
- Unity 주의: foreach+박싱, 잦은 new는 GC 유발 -> Profiler에서 확인 (Stage07 연계)

### [S00-02-02] 심화: SerializeField
- private 유지 + 인스펙터 노출 = 캡슐화 + 편집 편의
- public 남발 금지 이유: 외부에서 마음대로 수정 가능
