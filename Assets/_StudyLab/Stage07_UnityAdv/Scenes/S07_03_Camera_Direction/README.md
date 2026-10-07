# S07_03_Camera_Direction

## 학습목표
빌트인 API만으로 카메라 샷 블렌딩 연출 구현 + Cinemachine 개념 매핑

## 조작법
1. 씬에 빈 GameObject를 2~3개 배치해 샷 위치/각도 지정
2. `shots` 배열에 순서대로 등록 후 Play
3. Space: 다음 샷으로 블렌딩 전환 (ease 적용)
4. `blendTime` / `holdTime` 값을 바꿔 연출 차이 체감

## 태그 찾아보기
### [S07-03-01] 샷 블렌딩
위치: `Scripts/CameraDirector.cs:15`
> 즉시 점프가 아닌 Lerp/Slerp + ease로 컷의 어색함 제거.
> 상세: `Stage07 DeepDive [S07-03-01]` 참조.

### [S07-03-02] Cinemachine 매핑표
위치: `Scripts/CameraDirector.cs:70`
> shots -> Path/Dolly, blendTime -> Default Blend, PlayNext -> Priority 전환.
> 상세: `Stage07 DeepDive [S07-03-02]` 참조.

## 확인문제
1. Lerp에 ease를 걸지 않으면 카메라 움직임이 어색한 이유는?
2. 위치는 Lerp, 회전은 Slerp를 쓰는 이유는?
3. Cinemachine의 Default Blend가 이 스크립트의 무엇에 대응하는가?
