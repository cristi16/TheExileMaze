using UnityEngine;
using System.Collections;

public class EnvironmentObject : MonoBehaviour
{
    public AudioSource impactSoundSource;

    // Use this for initialization
    void Start()
    {
    }

    public void PlayProjectileHitSound(ContactPoint hitPoint)
    {
        impactSoundSource.transform.position = hitPoint.point;
        impactSoundSource.Play();
        impactSoundSource.GetComponent<LowPassFilter>().sourcePoint = hitPoint.point;
    }
}
