# S01_03_ClassStruct

## 학습목표
class(참조 복사)와 struct(값 복사)의 차이를 로그로 확인한다.

## 조작법
1. 빈 오브젝트에 `EnemyData.cs` 부착 후 Play
2. Console에서 `[S01-03-01]`(struct 원본 유지)와 `[S01-03-02]`(class 원본 변경) 로그 비교
3. 인스펙터에서 structA/classA 초기값을 바꿔 재실행

## 태그 찾아보기
### [S01-03-01] struct 값 복사
위치: `Scripts/EnemyData.cs:24`
> 대입 시 값 전체 복사, 복사본 수정이 원본에 무영향. 상세는 `Stage01 DeepDive [S01-03-01]` 참조.

### [S01-03-02] class 참조 복사
위치: `Scripts/EnemyData.cs:36`
> 대입 시 참조만 복사, 공유 상태 주의. 상세는 `Stage01 DeepDive [S01-03-02]` 참조.

### [S01-03-03] MonoBehaviour는 class
위치: `Scripts/EnemyData.cs:43`
> 동작은 MonoBehaviour, 데이터는 struct/class 분리. 상세는 `Stage01 DeepDive [S01-03-03]` 참조.

## 확인문제
1. struct 복사본의 hp를 바꿔도 원본이 유지되는 이유는?
2. class 복사본 수정이 원본에 반영되면 생길 수 있는 버그 예시는?
3. 적 HP/이름 같은 작은 데이터 묶음에 struct가 적합한 이유는?
