# Stage03 DeepDive — C# 고급 (제네릭/LINQ/인터페이스/비동기)

## 목표
Unity 게임 코드에서 반복되는 C# 고급 패턴 4종을 익힌다.

### [S03-01-01] 심화: 왜 Dictionary<Type, Delegate>인가
- 제네릭 메서드 `Subscribe<T>`의 T를 키로 쓰면 타입별 저장소가 하나로 통일됨
- 대안 비교: 타입별 클래스 분리는 이벤트 추가마다 클래스 추가 (개방-폐쇄 위반), object 박싱은 캐스팅+박싱 비용
- 초급: 발행/구독 동작 확인 / 중급: Delegate.Combine/Remove 직접 구현 이해 / 고급: 스레드 안전 버전 (lock + ConcurrentDictionary, Unity 메인 스레드 전제면 불필요)

### [S03-01-02] 심화: 구독 해제를 잊으면
- 파괴된 MonoBehaviour를 참조한 델리게이트가 남으면 MissingReferenceException
- 정석: OnEnable/OnDisable 쌍으로 관리, 씬 전환 시 정적 이벤트는 반드시 해제 (정적은 씬 언로드 후에도 살아남음)
- 초급: 쌍으로 쓰기 / 중급: 약한 참조(WeakReference) 구독 / 고급: UniRx/MessagePipe 같은 전용 라이브러리 비교

### [S03-01-03] 심화: Func vs event vs UnityEvent
- Func<T,R>: 값을 돌려받는 질의 위임 (포매터, 데미지 계산식 교체)
- event Action: 외부에서 Invoke 차단, += / -=만 허용 (캡슐화)
- UnityEvent: 인스펙터 연결 가능, 직렬화 지원, 호출 비용은 C# event보다 큼
- 초급: 셋 다 호출해보기 / 중급: 호출 비용 측정 / 고급: 이벤트 소싱/리플레이 관점에서 선택

### [S03-02-01] 심화: LINQ 연산자 지도
- 필터: Where / 정렬: OrderBy, ThenBy / 투영: Select / 존재: Any, FirstOrDefault
- 체이닝 순서가 성능: Where로 먼저 줄인 뒤 OrderBy (정렬 대상 축소)
- 초급: 체이닝 읽기 / 중급: 실행 순서와 지연 실행 디버깅 / 고급: Span/수동 루프로의 최적화 시점 판단

### [S03-02-02] 심화: LINQ와 GC
- ToList/ToArray는 매번 할당, Where 체인은 이터레이터 객체 할당
- 60fps 매 프레임 LINQ는 Gen0 GC 압박 -> 0.25초 캐싱, 변경 시에만 재계산 (dirty flag)
- Profiler 확인: Stage07 연계, Deep Profile에서 LINQ 할당 추적
- 초급: 캐싱 간격 조절 실험 / 중급: dirty flag 패턴 / 고급: Burst/Jobs와 LINQ 불가 이유

### [S03-02-03] 심화: 클로저 캡처의 정체
- 람다가 외부 변수를 참조하면 컴파일러가 숨은 클래스 생성 (힙 할당)
- 루프 변수 캡처: C# 5 이전엔 전원 같은 변수 공유, 이후엔 반복마다 새 변수 (그래도 장기 구독은 누수)
- 대책: 지역 복사 후 캡처, 장기 구독은 명명 메서드로 분리해 해제 가능하게
- 초급: 캡처 동작 실험 / 중급: 클로저 할당 확인 / 고급: static 람다(C# 9)로 캡처 금지 강제

### [S03-03-01] 심화: 인터페이스로 끊는 의존성
- DamageSystem은 IDamageable만 의존 (DIP: 추상에 의존), 새 적 추가가 기존 코드 수정 없음 (OCP)
- TryGetComponent+is 패턴으로 컴포넌트 유무에 따른 분기 제거
- 초급: 새 구현체 추가해보기 / 중급: 의존성 역전 그림 그리기 / 고급: Zenject 같은 DI와 인터페이스 바인딩

### [S03-03-02] 심화: interface vs abstract class vs virtual
- interface: 계약만, 다중 구현, 상태(필드) 불가
- abstract class: 공통 상태+미구현 메서드 강제, 단일 상속
- virtual in concrete base: 기본 동작 제공 + 선택적 재정의 (DamageableBase 패턴)
- MonoBehaviour는 단일 상속이므로: 공유 코드는 base 클래스, 횡단 계약은 인터페이스
- 초급: 셋 다 작성해보기 / 중급: 선택 기준 문서화 / 고급: default interface methods와 다이아몬드 문제

### [S03-03-03] 심화: override 호출 체인 설계
- base 호출 위치가 의미: 먼저 호출 = 기본 처리 후 확장, 나중 호출 = 선처리 후 기본
- Template Method 패턴: base가 순서 고정, 파생이 단계만 채움 (Die 연출 변형)
- 주의: base 호출 누락 버그는 조용히 동작 변경 -> 가상함수엔 주석으로 호출 의무 명시
- 초급: 호출 순서 바꿔보기 / 중급: Template Method 적용 / 고급: sealed override로 체인 종결 시점 설계

### [S03-04-01] 심화: Task.Delay의 동작 원리
- await는 타이머 콜백에 연속(continuation)을 등록하고 제어권 반환, 스레드를 잡지 않음
- Thread.Sleep은 현재 스레드 블로킹, 메인 스레드에서 호출 시 게임 전체 프리즈
- UnitySynchronizationContext가 있으면 await 이후 메인 스레드로 복귀 (Unity API 호출 가능 조건)
- 초급: 둘 다 호출해 프리즈 차이 체감 / 중급: SynchronizationContext 확인 / 고급: ConfigureAwait(false)와 Unity API 금지 구간

### [S03-04-02] 심화: async void 예외 소실
- async void의 예외는 호출자에게 전달 불가, UnhandledException으로 직행하거나 소실
- 대책: 진입점 내부 try/catch 필수, 로직은 Task 반환 메서드로 분리해 테스트 가능하게
- 버튼 onClick에는 async void 래퍼 + 내부 Task 메서드 호출 구조 권장
- 초급: 예외 던져보기 / 중급: Task 분리 리팩터링 / 고급: 전역 예외 로거와 크래시 리포트 연계

### [S03-04-03] 심화: 코루틴 vs Task vs UniTask
- 코루틴: 메인 스레드 보장, yield로 프레임 분할, 반환값/예외 처리 약함
- Task: .NET 표준, 스레드풀 경유 가능, Unity API 호출 시 스레드 주의
- UniTask(외부 패키지): 할당 없는 await + 메인 스레드 복귀 + 취소 토큰, 실전 표준이나 본 스테이지은 패키지 없이 학습
- 선택 기준: 단순 연출=코루틴, IO/계산 병렬=Task, 실전 게임 비동기=UniTask
- 초급: 같은 로딩을 둘 다 작성 / 중급: 취소(CancellationToken) 구현 / 고급: 프레임 예산 내 분할 로딩 설계
