# S01_05_PrefabInstantiate

## 학습목표
프리팹 Instantiate/Destroy와 null 참조 주의점을 익힌다.

## 조작법
1. 빈 오브젝트에 `PrefabSpawner.cs` 부착, enemyPrefab 연결 후 Play
2. 주기적으로 스폰되고 최대 수(`maxAlive`) 유지되는지 확인
3. 일부러 프리팹을 비우고 Play해 경고 로그 확인

## 태그 찾아보기
### [S01-05-01] 프리팹 null 사전 체크
위치: `Scripts/PrefabSpawner.cs:14`
> 미지정 Instantiate는 예외, 인스펙터 미할당이 1순위 원인. 상세는 `Stage01 DeepDive [S01-05-01]` 참조.

### [S01-05-02] 파괴 참조 null 정리
위치: `Scripts/PrefabSpawner.cs:36`
> Destroy 후 변수 재사용 시 MissingReference 유발. 상세는 `Stage01 DeepDive [S01-05-02]` 참조.

### [S01-05-03] 리스트 개수 상한 관리
위치: `Scripts/PrefabSpawner.cs:46`
> Find 계열 대신 카운터로 성능 안전. 상세는 `Stage01 DeepDive [S01-05-03]` 참조.

## 확인문제
1. `enemyPrefab`이 null인데 Instantiate하면 무슨 일이 생기는가?
2. Destroy된 오브젝트를 참조하는 변수를 쓰면 왜 예외가 나는가?
3. 개수 제한에 `FindGameObjectsWithTag` 대신 리스트를 쓰는 이유는?
