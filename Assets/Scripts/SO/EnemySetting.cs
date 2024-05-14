using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/enemy setting")]
public class EnemySetting : ScriptableObject
{
    public float speedRotation;
    public float rateFire;
    public Bullet bullet;
}
