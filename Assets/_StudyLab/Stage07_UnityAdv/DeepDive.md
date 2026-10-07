# Stage07 UnityAdv DeepDive

## [S07-01-01] 심화: 왜 머티리얼이 DrawCall을 가르는가
- GPU는 "같은 상태(셰이더+머티리얼+메시)" 단위로 묶어 한 번에 그린다. 이 한 번이 DrawCall.
- 머티리얼 인스턴스가 다르면 상태가 다르다고 판단해 묶지 못한다.
- URP에서 SRP Batcher가 켜져 있어도 머티리얼이 다르면 CBUFFER 바인딩이 달라져 배치가 깨진다.

## [S07-01-02] 심화: material vs sharedMaterial
- `renderer.material`: 내부적으로 머티리얼을 복제(Get)해서 반환. 같은 값을 넣어도 인스턴스 분리.
- `renderer.sharedMaterial`: 에셋 참조 그대로 공유. 런타임에 값을 바꾸면 그 머티리얼을 쓰는 전부가 바뀜(주의).
- 개별 색이 필요하면 복제가 아니라 MaterialPropertyBlock 사용이 정석 (이 프로젝트 범위 밖, 주석 언급만).

## [S07-01-03] 심화: Frame Debugger 읽는 법
- Enable 후 Game 뷰를 한 프레임씩 stepping하며 draw 이벤트 나열 확인.
- 같은 셰이더/머티리얼끼리 연속되면 배치 성공, 중간에 상태 변경이 끼면 실패 지점.
- Profiler의 Batches와 Frame Debugger 이벤트 수를 함께 보면 원인 추적이 빠르다.

## [S07-02-01] 심화: 동기 vs 비동기 로드
- `Resources.Load`: 호출 프레임에 디스크 IO + 역직렬화 + GPU 업로드까지 한 번에. 큰 에셋이면 수백 ms 멈춤.
- `LoadAsync`: `isDone`까지 여러 프레임에 분할. `allowSceneActivation`처럼 씬 로드와도 같은 원리.
- Addressables의 `LoadAssetAsync`는 여기에 의존성 다운로드 + 캐시 + 참조 카운팅까지 얹은 상위 호환.

## [S07-02-02] 심화: 누가 메모리를 들고 있는가
- `Instantiate` 결과물(Destroy 대상)과 원본 에셋(메모리 상주)은 별개.
- `Resources.UnloadUnusedAssets`: 참조 없는 에셋만 정리. 어딘가에서 참조 중이면 남는다.
- Addressables는 핸들 참조 카운팅이라 `Release` 누락 = 영구 누수. Resources로 "해제 습관"을 먼저 들이는 이유.

## [S07-02-03] 심화: 경로 문자열의 한계
- 경로 변경/오타가 런타임에야 드러난다. 리팩터링 내성 제로.
- Addressables는 Address 키 + 어드레스어블 그룹으로 빌드 타깃별 에셋 분리, 원격(CDN) 교체까지 가능.
- 마이그레이션 순서: 경로 상수화 -> 키화 -> 그룹 분리. 이 씬은 1단계까지 실습.

## [S07-03-01] 심화: ease가 연출인 이유
- 선형 Lerp는 시작/끝에서 속도가 불연속 (급출발-급정지). 인간의 눈은 가속도 변화에 민감.
- Smoothstep(`t*t*(3-2t)`)은 양 끝 기울기 0, 가장 저렴한 시네마틱 곡선.
- 더 과격한 연출은 easeOutBack(오버슛) 등. 카메라는 과하면 멀미 유발이라 완만이 정석.

## [S07-03-02] 심화: Cinemachine 개념 대응
- Virtual Camera = 이 스크립트의 shots 한 개 + (follow/lookAt + noise).
- Brain = 이 스크립트의 Update 블렌딩 부분. 우선순위 높은 vcam으로 자동 블렌드.
- 직접 구현해 보면 "Cinemachine은 샷 관리 + 블렌딩 자동화 도구"라는 본질이 보인다.

## [S07-04-01] 심화: Parallel이 빨라지는 조건
- 순수 CPU 연산 + 데이터 독립(인덱스 간 의존성 없음) + 충분한 양. 3 조건이 핵심.
- `count`가 작으면 스레드풀 스케줄링 비용이 이득을 잠식 (실습에서 1k vs 100k 비교).
- Unity API는 네이티브-매니지드 브리지 + 스레드 검사가 있어 워커스레드에서 호출 불가. 예외 발생.

## [S07-04-02] 심화: Job/Burst가 한 걸음 더 나가는 법
- `Parallel.For`는 CLR 스레드풀. Job System은 Unity 전용 워커스레드 + 작업 도용(work stealing).
- Burst는 SIMD 벡터화 + 네이티브 코드 생성. 단순 덧셈 루프가 수 배 빨라지는 원리.
- 제약: 값타입 + NativeContainer만. 참조타입/UnityEngine.Object 금지. 이 제약이 설계 훈련이 된다.

## [S07-05-01] 심화: 3대 유발 패턴의 내부 동작
- `string +=`: string 불변이라 매번 새 할당 + 이전 내용 복사. 루프면 O(n^2).
- 매 프레임 `GetComponent`: 내부 탐색 비용 + (제네릭/박싱 시) 할당. 캐싱하면 0.
- `new WaitForSeconds` 매 yield: 작은 힙 할당의 반복. GC 압박은 양이 아니라 빈도.

## [S07-05-02] 심화: 3대 처방의 원리
- StringBuilder: 내부 char 버퍼 확장(2배 증설)으로 복사 최소화. `Length=0`으로 재사용.
- 캐싱: Start/Awake에서 1회 조회, 필드에 보관. 프레임당 탐색 0회.
- `GC.GetTotalMemory(false)`: 강제 수집 없는 근사치. 정확한 비교는 Profiler의 GC Alloc 열 + Deep Profile.
