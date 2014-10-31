using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour
{
    public AudioClip wallHitSound;
    public float stunTime = 3f;

    private bool isStunned = false;
    void Start()
    {
        audio.ignoreListenerVolume = true;
    }

    void OnCollisionEnter(Collision col)
    {
        var hit = col.gameObject.GetComponent<EnvironmentObject>();
        if (hit != null && isStunned == false)
        {
            AudioSource.PlayClipAtPoint(wallHitSound, col.contacts[0].point);
            StartCoroutine( PlayStunnedSound() );
        }
    }

    IEnumerator PlayStunnedSound()
    {
        isStunned = true;
        AudioListener.volume = 0f;
        yield return new WaitForSeconds(stunTime);
        AudioListener.volume = 1f;
        isStunned = false;
    }
}
