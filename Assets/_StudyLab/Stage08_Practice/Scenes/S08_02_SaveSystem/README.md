# S08_02_SaveSystem

## 학습목표
JsonUtility + PlayerPrefs + 파일 2단 저장/불러오기/삭제 흐름 이해

## 조작법
1. Inspector에서 gold/playerName/stage 변경 후 Play, S: 저장
2. 값 변경 후 L: 불러오기 (원복 확인)
3. D: 슬롯 + 파일 삭제 후 L (경고 로그 확인)
4. PlayerPrefs 삭제 후 L: 파일 폴백 복구 확인

## 태그 찾아보기
### [S08-02-01] JsonUtility 규칙
위치: `Scripts/SaveSystem_JSON.cs:15`
> public/SerializeField만 저장. 프로퍼티·딕셔너리 미지원.
> 상세: `Stage08 DeepDive [S08-02-01]` 참조.

### [S08-02-02] 2단 저장
위치: `Scripts/SaveSystem_JSON.cs:40`
> 빠른 슬롯(PlayerPrefs) + 영속 백업(파일). 폴백 복구 포함.
> 상세: `Stage08 DeepDive [S08-02-02]` 참조.

## 확인문제
1. 프로퍼티로 선언한 필드가 저장 안 되는 이유는?
2. 딕셔너리를 저장하려면 어떤 우회책이 있는가? (리스트 변환)
3. PlayerPrefs만으로 실서비스 저장소를 쓰면 안 되는 이유는?
