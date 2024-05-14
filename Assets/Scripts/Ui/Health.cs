using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using DG.Tweening;

public class Health : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Transform icon;

    [Inject] private GameManager gameManager;

    private Tween shakeTween;

    private void Start()
    {
        Subscribe();        
    }

    private void Subscribe()
    {
        gameManager.health.Subscribe(value =>
        {
            healthText.text = value.ToString();

            if (shakeTween == null)
            {
                shakeTween = icon.transform.DOShakeScale(.1f, .25f, 1).SetUpdate(true).OnComplete(() =>
                {
                    shakeTween = null;
                });
            }
        });
    }
}
