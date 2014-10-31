using UnityEngine;
using System.Collections;

public class EnvironmentObject : MonoBehaviour
{
    public AudioClip projectileHitClip;

    // Use this for initialization
    void Start()
    {

    }

    public void PlayProjectileHitSound(Vector3 hitPoint)
    {
        if(projectileHitClip != null)
            AudioSource.PlayClipAtPoint(projectileHitClip, hitPoint);
    }
}
