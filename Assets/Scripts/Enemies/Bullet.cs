using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [Inject] private VfxManager vfxManager;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            damagable.ApplyDamage(damage);

            vfxManager.PlayVFX(VfxManager.VfxType.hit, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
