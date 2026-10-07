# S07_03_ex_Cinemachine

## 학습목표
Cinemachine 3.x 실전 샷 전환. 개념씬 S07_03(CameraDirector Lerp)과 비교.

## 조작법
1. Play (ShotA/ShotB 자동 생성, Brain 자동 부착)
2. C키 → 샷 전환 (Blend 1.5s로 섞임)
3. Hierarchy의 ShotA/ShotB에 Follow 타겟 지정 후 다시 전환

## 태그 찾아보기
### [S07-03-EX-01] Priority 전환
위치: `Scripts/CameraDirector_Real.cs:13`
> 카메라 on/off가 아니라 Priority 대소로 전환. Brain이 블렌딩 담당.

### [S07-03-EX-02] Brain 보장
위치: `Scripts/CameraDirector_Real.cs:29`
> Brain 없으면 전환 불가. 없으면 자동 추가 + Blend 시간 적용.

## 확인문제
1. 두 샷의 Priority가 같으면 어떤 샷이 보이는가?
2. Blend 시간을 0으로 하면 화면이 어떻게 바뀌는가?
3. Follow를 비우면 샷 위치는 어떻게 결정되는가?
