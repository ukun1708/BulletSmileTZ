using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoolReactiveProperty startGame = new();
    public BoolReactiveProperty loseGame = new();
    public BoolReactiveProperty winGame = new();
    public IntReactiveProperty health = new();

    public CancellationTokenSource tokenSource = new();

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        health.Subscribe(value =>
        {
            if (value <= 0)
            {
                loseGame.Value = true;
            }
        });
    }

    public void RestartScene()
    {
        tokenSource.Cancel(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
