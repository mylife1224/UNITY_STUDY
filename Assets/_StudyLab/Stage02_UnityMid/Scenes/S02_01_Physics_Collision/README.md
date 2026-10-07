# S02_01_Physics_Collision

## 학습목표
Rigidbody 이동과 OnCollisionEnter(물리 반응) vs OnTriggerEnter(감지) 차이를 이해한다.

## 조작법
1. 구체 오브젝트에 `Bouncer.cs` 부착(Rigidbody 자동 요구), Play하면 앞으로 발사
2. 벽(Collider, isTrigger=false)에 부딪혀 튕기는 로그 `[S02-01-02]` 확인
3. 통과 존(Collider, isTrigger=true)을 지나며 `[S02-01-03]` 감지 로그 확인

## 태그 찾아보기
### [S02-01-01] 물리는 힘/속도로
위치: `Scripts/Bouncer.cs:13`
> transform 직접 이동과 혼용 금지. 상세는 `Stage02 DeepDive [S02-01-01]` 참조.

### [S02-01-02] Collision 물리 반응
위치: `Scripts/Bouncer.cs:26`
> 튕김/반발 + 접촉점 정보. 상세는 `Stage02 DeepDive [S02-01-02]` 참조.

### [S02-01-03] Trigger 통과 감지
위치: `Scripts/Bouncer.cs:37`
> 물리 반응 없이 이벤트만. 상세는 `Stage02 DeepDive [S02-01-03]` 참조.

## 확인문제
1. Rigidbody 오브젝트를 transform으로 움직이면 안 되는 이유는?
2. 아이템 획득에는 Collision과 Trigger 중 무엇이 적합한가?
3. Trigger 이벤트가 일어나려면 필요한 최소 조건은?
