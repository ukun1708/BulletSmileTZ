using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button button;
    [SerializeField] private PopUpSetting setting;

    [Inject] private GameManager gameManager;

    private Image buttonImage;
    private TMP_Text buttonText;

    private void Awake()
    {
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<TMP_Text>();

        Color color = buttonImage.color;
        color.a = 0f;
        buttonImage.color = color;
        buttonText.color = color;
    }

    private void Start()
    {
        button.gameObject.SetActive(false);

        Subscribe();

        ButtonLogic();
    }

    private void ButtonLogic()
    {
        button.onClick.AddListener(async () =>
        {
            button.transform.DOScale(Vector3.zero, .25f).SetUpdate(true).SetEase(Ease.InBack);

            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, .15f);

            await UniTask.Delay(250, true);

            title.transform.DOScale(Vector3.zero, .25f).SetUpdate(true).SetEase(Ease.InBack).OnComplete(() =>
            {
                gameManager.RestartScene();
            });
        });
    }

    private void Subscribe()
    {
        gameManager.startGame.Subscribe(value =>
        {
            if (value == true)
            {
                title.DOFade(0f, .5f).SetUpdate(true).SetUpdate(true).OnComplete(() =>
                {
                    title.gameObject.SetActive(false);
                });
            }
            else
            {
                title.text = setting.startText;

                title.transform.localScale = Vector3.zero;

                title.transform.DOScale(Vector3.one, .25f).SetUpdate(true).SetEase(Ease.OutBack);
            }
        });
        gameManager.loseGame.Subscribe(value =>
        {
            if (value == true)
            {
                FadeAnimation(setting.loseText);
            }
        });
        gameManager.winGame.Subscribe(value =>
        {
            if (value == true)
            {
                FadeAnimation(setting.winText);
            }
        });
    }

    private async void FadeAnimation(string titleText)
    {
        title.text = titleText;

        title.gameObject.SetActive(true);

        title.DOFade(1f, .5f).SetUpdate(true);

        await UniTask.Delay(2000, true);

        button.gameObject.SetActive(true);

        buttonImage.DOFade(1f, .5f).SetUpdate(true);
        buttonText.DOFade(1f, .5f).SetUpdate(true);
    }
}
