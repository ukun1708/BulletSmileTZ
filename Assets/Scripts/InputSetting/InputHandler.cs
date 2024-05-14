using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour
{
    private InputAsset inputActions;

    [SerializeField] private DirectionIndicator directionIndicator;

    [SerializeField] private DynamicJoystick joystick;

    private LineRenderer lineRenderer;

    private Tween timeScaleTween;

    private IMovable movable;

    [Inject] private GameManager gameManager;

    private void Awake()
    {
        inputActions = new InputAsset();
        directionIndicator.gameObject.SetActive(false);
        movable = GetComponent<IMovable>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        inputActions.Player.Click.started += ClickDown;
        inputActions.Player.Click.canceled += ClickUp;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Click.started += ClickDown;
        inputActions.Player.Click.canceled -= ClickUp;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (gameManager.winGame.Value == false)
        {
            if (directionIndicator.gameObject.activeSelf == true)
            {
                var input = new Vector2(joystick.Horizontal, joystick.Vertical);

                directionIndicator.transform.position = transform.position;
                directionIndicator.arrow.localPosition = input * 2f;

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, directionIndicator.arrow.position);
            }
        }            
    }

    private void ClickDown(InputAction.CallbackContext context)
    {
        if (gameManager.winGame.Value == false)
        {
            SlowDownTimeLogic(.01f);
            
            directionIndicator.gameObject.SetActive(true);
            lineRenderer.enabled = true;

            if (gameManager.startGame.Value == false)
            {
                gameManager.startGame.Value = true;
            }
        }        
    }

    private void ClickUp(InputAction.CallbackContext context)
    {
        if (gameManager.winGame.Value == false)
        {
            SlowDownTimeLogic(1f);
            directionIndicator.gameObject.SetActive(false);

            var direction = new Vector2(joystick.Horizontal, joystick.Vertical);

            movable.Move(direction);

            lineRenderer.enabled = false;
        }  
    }

    private void SlowDownTimeLogic(float timeScale)
    {
        timeScaleTween.Kill();
        timeScaleTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, timeScale, .15f);
    }
}
