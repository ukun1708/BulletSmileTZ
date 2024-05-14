using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurelFirst : Enemy
{
    private Tween muzzleTweem;

    private float timer;

    [Inject] private VfxManager vfxManager;
    [Inject] private DiContainer container;

    public override void Shooting()
    {
        timer += Time.deltaTime;

        if (timer >= setting.rateFire)
        {
            timer = 0;

            Shot();
        }        
    }

    private void Shot()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<Player>())
            {
                GameObject _bullet = container.InstantiatePrefab(setting.bullet, muzzle.position, transform.rotation, null);

                muzzleTweem.Kill();

                muzzleTweem = muzzle.DOLocalMoveZ(0f, .15f).OnComplete(() =>
                {
                    muzzleTweem = muzzle.DOLocalMoveZ(.5f, .25f).SetEase(Ease.OutBack);
                });

                vfxManager.PlayVFX(VfxManager.VfxType.muzzle, muzzle.position, Quaternion.Euler(-transform.eulerAngles));
            }
        }
    }
}
