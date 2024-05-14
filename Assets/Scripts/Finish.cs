using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Finish : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            gameManager.winGame.Value = true;

            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, .5f);
        }
    }
}
