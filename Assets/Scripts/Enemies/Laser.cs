using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Laser : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            damagable.ApplyDamage(gameManager.health.Value);
        }
    }
}
