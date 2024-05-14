using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Player player;

    [SerializeField] protected Transform muzzle;

    [SerializeField] protected EnemySetting setting;

    private void Update()
    {
        if (player != null)
        {
            Aiming();

            Shooting();
        }
    }

    private void Aiming()
    {
        var direction = player.transform.position - transform.position;

        direction.Normalize();

        var newRotation = Vector3.RotateTowards(transform.forward, direction, setting.speedRotation * Time.deltaTime, 0f);

        transform.rotation = Quaternion.LookRotation(newRotation);
    }

    public virtual void Shooting()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            this.player = player;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            this.player = null;
        }
    }
}
