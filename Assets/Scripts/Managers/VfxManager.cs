using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public GameObject[] particles;

    public void PlayVFX(VfxType vfxType, Vector3 createPosition, Quaternion rotation)
    {
        GameObject particle = Instantiate(particles[(int)vfxType], createPosition, rotation);

        particle.GetComponent<ParticleSystem>().Play();

        Destroy(particle, 1f);
    }
    public enum VfxType
    {
        muzzle,
        death,
        hit
    }

}
