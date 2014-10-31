using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Pitch))]
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
            GetComponent<Pitch>().sourcePoint = hitPoint.point;
        }
    }
}
