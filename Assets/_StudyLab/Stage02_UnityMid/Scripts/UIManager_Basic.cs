using System;
using UnityEngine;
using UnityEngine.UI;

// S02_03 씬 전용: Button onClick + Action 이벤트 예제
public class UIManager_Basic : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Text scoreText;

    private int score;

    // [S02-03-01] WHY: 외부 알림은 Action 이벤트로 분리, UI와 게임 로직 결합도 감소
    // 구독자가 없어도 null 조건 연산자로 안전 호출
    // README [S02-03-01], DeepDive [S02-03-01] 참조
    public event Action<int> OnScoreChanged;

    void Awake()
    {
        OnScoreChanged += RefreshScoreText;
    }

    void Start()
    {
        // [S02-03-02] WHY: 인스펙터 연결 대신 코드 AddListener, 누락을 로그로 즉시 발견
        // 람다 남발 대신 명명 메서드로 중복 등록 방지
        // README [S02-03-02], DeepDive [S02-03-02] 참조
        if (startButton == null || scoreText == null)
        {
            Debug.LogWarning("[S02-03-02] Button/Text 미연결. 인스펙터에 연결하세요.");
            return;
        }
        startButton.onClick.AddListener(OnStartClicked);
        RefreshScoreText(score);
    }

    void OnDestroy()
    {
        // [S02-03-03] WHY: 구독 해제로 메모리 누수/중복 호출 방지
        // 씬 전환 후 파괴된 객체가 이벤트에 남으면 예외 발생
        OnScoreChanged -= RefreshScoreText;
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(OnStartClicked);
        }
    }

    private void OnStartClicked()
    {
        score += 10;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"[S02-03-02] Button 클릭, score={score}");
    }

    private void RefreshScoreText(int value)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {value}";
        }
    }
}
