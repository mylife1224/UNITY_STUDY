# HANDOFF — Unity C# StudyLab (2026-10-08 기준)

## 프로젝트
- 경로: `C:\Dev\UNITY_STUDY` / Unity `6000.3.13f1` / URP 아님(빌트인)
- 학습 코드: `Assets/_StudyLab/Stage00_Setup` ~ `Stage08_Practice`, 씬 42개
- 에디터 도구: 상단 메뉴 `StudyLab` (Build All Scenes, Diagnose *, 씬 일괄 생성기 `Assets/_StudyLab/Editor/StudyLabBuilder.cs`)

## 문서 규칙
- 태그 `[SXX-YY-ZZ]` / 실습형 `[SXX-YY-EX-ZZ]` 로 코드→씬README→Stage DeepDive 연결
- 코드 주석 WHY 3줄 이내, 상세는 README. 위치 표기 `Scripts/파일.cs:줄번호`
- 씬 README는 각 `Scenes/<씬명>/README.md`, 심화는 각 Stage `DeepDive.md`

## 설치 패키지
Input System(Active Input Handling=Both 유지, 구 Input API 사용 씬 다수),
Addressables, Cinemachine 3.x, Burst, Collections, Test Framework

## 결정 사항 (재논의 불필요)
- `PriorityQueue<T>` 미지원(.NET Standard 2.1) → `MinHeap` 자작(S04_06), `OrderBy` 대체(S06_01)
- S08 테스트: asmdef 이름참조 미해결로 Tests 어셈블리 폐기 → `InventoryDemo` ContextMenu 자가테스트 방식
- Stage07 개념씬 유지 + `_ex` 실습씬 3종 병존 (비교 학습용)
- 파일/클래스명 불일치 2건 허용: `Patterns_All.cs`→`PatternsDemo`, `InventoryDemo.cs`(구 `InventoryLogic.cs`, 임포트 꼬임으로 재생성)

## 알려진 주의점
- 외부 파일 대량 생성/이동 후 AssetDB stale 발생 가능 → 증상 시 에디터 재시작(Safe Mode 진입 금지)
- 씬 YAML 내장 스텁(`!u!115 MonoScript`) = 빌드 시점 스크립트 미임포트 잔재 → 해당 스크립트 Reimport 후 씬 재빌드
- 빌더 탐색: TypeCache 우선, 파일명≠클래스명 시 FindAssets 폴백 불가 → 클래스명 기준

## 현재 상태
- 컴파일 에러 0, 씬 42개 Lab 부착 완료, S08_03 자가테스트 PASS 확인
- 다음: S00_01부터 순차 학습 진행

## Git
- 원격: `https://github.com/mylife1224/UNITY_STUDY` (main)
- 제외: Library/, Temp/, Logs/, UserSettings/ 등 (.gitignore)
- 포함: Assets/, ProjectSettings/, Packages/, *.md
- 새 세션 작업 후 커밋 pushed 여부 확인: `git status -sb`
