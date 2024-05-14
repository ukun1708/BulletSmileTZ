using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public Transform arrow;
    private void Update()
    {
        LookAtPoint();
    }
    private void LookAtPoint()
    {
        var direction = transform.position - arrow.transform.position;

        direction.Normalize();

        arrow.transform.rotation = Quaternion.LookRotation(arrow.transform.forward, -direction);
    }
}
