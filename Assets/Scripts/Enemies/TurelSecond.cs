using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurelSecond : Enemy
{
    private float timer;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform laserHit;

    [Inject] private GameManager gameManager;

    private void Start()
    {
        lineRenderer.enabled = false;
        laserHit.gameObject.SetActive(false);
    }

    public override void Shooting()
    {
        timer += Time.deltaTime;

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (lineRenderer.enabled == true)
            {
                lineRenderer.SetPosition(0, muzzle.position);
                lineRenderer.SetPosition(1, hit.point);

                laserHit.position = hit.point;

                if (hit.collider.TryGetComponent(out IDamagable damagable))
                {
                    damagable.ApplyDamage(gameManager.health.Value);
                }
            }
        }

        if (timer >= setting.rateFire)
        {
            timer = 0;

            LaserLaunch();
        }
    }

    private async void LaserLaunch()
    {
        lineRenderer.enabled = true;
        laserHit.gameObject.SetActive(true);

        try
        {
            await UniTask.Delay(1000, cancellationToken: gameManager.tokenSource.Token);

            lineRenderer.enabled = false;
            laserHit.gameObject.SetActive(false);
        }
        catch (OperationCanceledException)
        {

        }
    }
}
