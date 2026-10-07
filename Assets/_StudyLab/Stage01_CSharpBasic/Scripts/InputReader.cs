using UnityEngine;
using UnityEngine.InputSystem;

// S01_04 씬 전용: 새 Input System(Keyboard 직접 폴링)으로 WASD 이동
// Input Actions 에셋 없이 학습용, 패키지 com.unity.inputsystem 필요
public class InputReader : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    // [S01-04-01] WHY: 기존 Input.GetKey 대신 InputSystem Keyboard 사용
    // 장치별 입력(키보드/게임패드) 분리와 리바인딩이 쉬움
    // README [S01-04-01], DeepDive [S01-04-01] 참조
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        Vector2 input = Vector2.zero;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            input.y += 1f;
        }
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            input.y -= 1f;
        }
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            input.x -= 1f;
        }
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            input.x += 1f;
        }

        // [S01-04-02] WHY: 정규화로 대각선 속도 보정, 없으면 루트2배 빨라짐
        // magnitude가 1 초과일 때만 정규화해도 동일 결과
        Vector3 dir = new Vector3(input.x, 0f, input.y);
        if (dir.sqrMagnitude > 1f)
        {
            dir.Normalize();
        }
        transform.position += dir * speed * Time.deltaTime;

        // [S01-04-03] WHY: 단발 입력(점프 등)은 폴링 대신 edge 감지 권장
        // wasPressedThisFrame 예시, 스페이스바 로그로 확인
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[S01-04-03] Space pressed (단발 입력 감지)");
        }
    }
}
