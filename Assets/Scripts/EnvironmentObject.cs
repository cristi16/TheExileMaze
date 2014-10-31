using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LowPassFilter))]
public class EnvironmentObject : MonoBehaviour
{
    public AudioClip projectileHitClip;

    // Use this for initialization
    void Start()
    {
        audio.clip = projectileHitClip;
    }

    public void PlayProjectileHitSound(ContactPoint hitPoint)
    {
        if(projectileHitClip != null)
        {
            audio.Play();
            GetComponent<LowPassFilter>().sourcePoint = hitPoint.point;
        }
    }
}
