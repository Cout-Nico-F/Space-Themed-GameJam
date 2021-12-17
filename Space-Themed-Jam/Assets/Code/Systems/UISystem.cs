using DG.Tweening;
using TMPro;
using UnityEngine;

public class UISystem : MonoBehaviour, IEventObserver
{
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private CanvasGroup countdownCanvasGroup;


    private void Start()
    {
        HideAllMenus();
        // nos suscribimos a los eventos Victory y Game Over
    }

    private void HideAllMenus()
    {
    }

    public void ShowCountdown()
    {
        levelText.text = "Level 1"; // aqui irá el current level del LevelSystem
        countdownCanvasGroup.alpha = 1;
    }


    public void SetCountdownText(string text)
    {
        if (text.Equals(countdownText.text)) return;

        countdownText.text = text;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1), .1f).SetEase(Ease.OutSine));
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1, 1, 1), .2f).SetEase(Ease.OutSine));
    }


    public void HideCountdownText()
    {
        countdownText.text = "GO!";
        Sequence sequence = DOTween.Sequence();
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1), .1f).SetEase(Ease.OutSine));
        sequence.Append(countdownText.rectTransform.DOScale(new Vector3(1, 1, 1), .3f).SetEase(Ease.OutSine));
        sequence.Append(countdownCanvasGroup.DOFade(0, .2f));
        sequence.OnComplete(() => countdownText.gameObject.SetActive(false));
    }


    public void ResetScore()
    {
        scoreView.ResetScore();
    }


    public void AddScore(int points)
    {
        scoreView.AddScore(points);
    }


    public void Process(EventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
