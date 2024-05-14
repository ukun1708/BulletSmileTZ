using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using DG.Tweening;

public class Player : MonoBehaviour, IMovable, IDamagable
{
    private Rigidbody rb;

    [Inject] private GameManager gameManager;
    [Inject] private VfxManager vfxManager;

    [SerializeField] private PlayerSetting setting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Subscribe();        
    }

    private void Subscribe()
    {
        gameManager.startGame.Subscribe(value =>
        {
            if (value == true)
            {
                rb.isKinematic = false;
            }
            else
            {
                rb.isKinematic = true;
            }
        });
        gameManager.loseGame.Subscribe(value =>
        {
            if (value == true)
            {
                vfxManager.PlayVFX(VfxManager.VfxType.death, transform.position, Quaternion.identity);               
                Destroy(gameObject);
            }
        });
    }

    public void Move(Vector2 dir)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(dir * setting.movementForce, ForceMode.Impulse);
    }

    public void ApplyDamage(int damage)
    {
        gameManager.health.Value -= damage;
    }
}
