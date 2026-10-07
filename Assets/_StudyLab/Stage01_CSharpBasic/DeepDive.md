# Stage01 DeepDive — C# 기초 체력

## 목표
if/for, 배열/List, class/struct, 새 Input System, 프리팹 생성을 손에 익힌다.

### [S01-01-01] 심화: 프레임 독립 이동
- `위치 += 방향 * 속도 * Time.deltaTime`: 프레임 시간만큼만 이동해 30/60/120fps 동일 속도
- 입력은 매 프레임 읽기(이벤트 누락 방지), 물리 적용은 Stage02 Rigidbody로 이관
- 초급: WASD 이동 / 중급: 가속·감속 추가 / 고급: FixedUpdate와 Update 역할 분리

### [S01-01-02] 심화: if 경계 고정
- Rigidbody 없이 위치를 강제 고정하는 가장 단순한 방법
- 한계: 빠른 속도에서 경계 밖으로 튀었다 돌아옴(터널링 유사), 물리 반발 없음
- 반발/튕김이 필요하면 S02_01 Collision으로 교체

### [S01-01-03] 심화: for 축별 클램프
- 축을 배열로 묶으면 x/z/y 추가 시 루프만으로 확장, 조건 중복 제거
- `Mathf.Clamp` 숙달 전 단계로 조건문 감각 학습용
- 성능: 축 2~3개 수준은 분기 예측 비용 무시 가능

### [S01-02-01] 심화: 고정 배열 순환
- 크기가 고정·개수가 일정(총알 상한)하면 배열+커서 `%` 순환이 최단·GC 제로
- 단점: 삭제/삽입/탐색 불가, 꽉 차면 오래된 것 강제 폐기 정책 필요
- 초급: 고정 슬롯 / 중급: 링버퍼 일반화 / 고급: Stage02 오브젝트 풀과 결합

### [S01-02-02] 심화: List 가변 관리
- 개수가 변하면 List, `RemoveAt`은 뒤 원소 앞으로 이동 O(n)
- 삭제가 잦으면 뒤에서 앞으로 순회(인덱스 밀림 방지), 대안은 S02_05 풀링
- 초급: Count 상한 / 중급: Capacity 사전 확보로 재할당 방지 / 고급: Span/풀 비교

### [S01-02-03] 심화: 배열 null/파괴 체크
- Unity에서 Destroy된 참조는 `== null`이 true(MissingReference), 사용 전 검사 필수
- 배열은 Count가 없어 슬롯별 null 검사가 곧 유효성 검사
- 습관: Instantiate 전 프리팹 null 체크, 사용 전 슬롯 null 체크

### [S01-03-01] 심화: struct 값 복사
- struct 대입 = 메모리 통째 복사, 이후 서로 독립
- 16바이트 이하 작은 묶음 권장, 크면 복사 비용 증가 → class 검토
- Unity 대표 struct: Vector3, Quaternion, Color

### [S01-03-02] 심화: class 참조 복사
- 변수에는 힙 주소만 복사, 두 변수가 같은 객체 공유 → 한쪽 수정이 양쪽 반영
- 의도적 공유(매니저, SO)에만 사용, 실수 공유는 인스펙터/로그로 추적 어려움
- 방어: 읽기 전용 프로퍼티, 이벤트로 변경 통지(S02_03 연계)

### [S01-03-03] 심화: MonoBehaviour는 class
- 씬에 붙어 Unity가 생명주기 호출, `new`로 직접 생성 금지(AddComponent 사용)
- 데이터(struct/class SO)와 동작(MonoBehaviour) 분리 → Stage00 [S00-01-01]과 연결
- 초급: 한 파일 한 역할 / 중급: 데이터-로직 분리 / 고급: 의존성 주입 맛보기

### [S01-04-01] 심화: 새 Input System
- 기존 Input은 전역 폴링, InputSystem은 장치(Keyboard/Gamepad) 객체 분리
- 장점: 리바인딩, 멀티 디바이스, Input Actions 에셋으로 확장(후속 학습)
- `Keyboard.current` null 가능(키보드 없는 환경) → 가드 필수

### [S01-04-02] 심화: 대각선 정규화
- (1,0,1) 벡터 크기는 √2 ≈ 1.41 → 정규화 없이 대각선이 41% 빠름
- `sqrMagnitude > 1` 체크 후 Normalize: sqrt 1회로 저렴
- 아날로그 스틱(크기<1)은 그대로 둬서 세기 반영

### [S01-04-03] 심화: 지속 vs 단발 입력
- 이동 = `isPressed`(지속), 점프/발사 = `wasPressedThisFrame`(edge 1회)
- Update 폴링에서 edge를 직접 플래그 관리하면 버그 빈번 → API 사용
- 중급: Input Actions의 performed/canceled 콜백으로 이관

### [S01-05-01] 심화: 프리팹 null 체크
- 인스펙터 미할당이 초급 런타임 에러 1순위, Instantiate 전 가드가 최선
- `LogWarning` + 조기 return으로 원인 즉시 표시, 예외 스택 대신 친절 로그
- 팀 습관: [SerializeField] + Awake null 검증 + 경고

### [S01-05-02] 심화: 파괴 참조 정리
- Destroy는 프레임 말 실제 파괴, 직후 접근도 위험 → 리스트에서 즉시 제거 패턴
- 뒤에서 앞으로 순회하며 RemoveAt: 인덱스 밀림 없이 안전 삭제
- 역순 순회가 표준 관용구, 외워둘 것

### [S01-05-03] 심화: Find 대신 개수 관리
- `Find*` 계열은 씬 전체 순회로 느림, 매 프레임 호출 금지
- 생성한 것은 직접 리스트/카운터로 소유권 관리가 정석
- 상한 초과 시 오래된 것부터 파괴(FIFO) → S02_05 풀로 발전
