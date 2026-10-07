# S01_02_ArrayList_Bullet

## 학습목표
배열(`T[]`)과 `List<T>`의 차이를 총알 관리 예제로 이해한다.

## 조작법
1. 빈 오브젝트에 `BulletManager_Array.cs` 부착, bulletPrefab에 총알 프리팹 연결 후 Play
2. Console에서 발사 주기 확인, 총알이 20개를 넘지 않는지 관찰
3. `maxBullets`를 바꿔 배열 순환 vs 리스트 상한 동작 비교

## 태그 찾아보기
### [S01-02-01] 고정 배열 순환 재사용
위치: `Scripts/BulletManager_Array.cs:11`
> 크기 고정 + 커서 순환, GC 없음. 상세는 `Stage01 DeepDive [S01-02-01]` 참조.

### [S01-02-02] 가변 List 관리
위치: `Scripts/BulletManager_Array.cs:17`
> 개수 유연, RemoveAt 이동 비용 있음. 상세는 `Stage01 DeepDive [S01-02-02]` 참조.

### [S01-02-03] 배열 null/파괴 체크
위치: `Scripts/BulletManager_Array.cs:50`
> Destroy된 자리는 null이므로 덮어쓰기. 상세는 `Stage01 DeepDive [S01-02-03]` 참조.

## 확인문제
1. 배열 길이를 실행 중에 늘릴 수 없는 이유는?
2. `FireList`에서 뒤에서 앞으로 순회하며 삭제하는 이유는?
3. 총알 개수가 고정이라면 배열과 List 중 어느 쪽이 유리한가?
