# S08_04_FinalBuild

## 학습목표
빌드 전 체크리스트를 Console 출력으로 습관화 + 에디터 분기 안전 패턴

## 조작법
1. Play (시작 시 체크리스트 자동 출력) 또는 B: 재출력
2. Inspector 우클릭 > `Print Build Checklist` ContextMenu 실행
3. 각 항목을 실제 Build Settings / Player Settings와 대조

## 태그 찾아보기
### [S08-04-01] 체크리스트 데이터
위치: `Scripts/BuildChecklist.cs:8`
> 빌드 실패의 8할은 사전 점검으로 예방. 코드화된 습관.
> 상세: `Stage08 DeepDive [S08-04-01]` 참조.

### [S08-04-02] 에디터 분기
위치: `Scripts/BuildChecklist.cs:36`
> `using UnityEditor` 없이 `#if UNITY_EDITOR`로만 분리. 빌드 컴파일 안전.
> 상세: `Stage08 DeepDive [S08-04-02]` 참조.

## 확인문제
1. `using UnityEditor;`를 최상단에 쓰면 빌드에서 깨지는 이유는?
2. Scenes In Build 누락 시 실기에서 나는 대표 증상은?
3. Development Build를 켠 채 스토어에 내면 안 되는 이유는?
