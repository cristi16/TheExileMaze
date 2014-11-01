using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour
{
    public AudioClip wallHitSound;
    public AudioSource headAcheSource;

    private float masterVolumeFadeTime = 1f;
    private bool isStunned = false;
    void Start()
    {
        audio.ignoreListenerVolume = true;
        headAcheSource.ignoreListenerVolume = true;
    }

    void OnCollisionEnter(Collision col)
    {
        var hit = col.gameObject.GetComponent<EnvironmentObject>();
        if (hit != null && isStunned == false)
        {

            StartCoroutine(PlayStunnedSound(col.contacts[0].point));
        }
    }

    IEnumerator PlayStunnedSound(Vector3 point)
    {
       
        AudioSource.PlayClipAtPoint(wallHitSound, point);

        isStunned = true;
        StartCoroutine(FadeMaster());
        headAcheSource.Play();

        yield return new WaitForSeconds(headAcheSource.clip.length);

        float timer = 0;
        while (AudioListener.volume < 1f)
        {
            AudioListener.volume = Mathf.Lerp(0f, 1f, timer / masterVolumeFadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        isStunned = false;
    }

    IEnumerator FadeMaster()
    {
        float timer = 0f;
        while (AudioListener.volume > 0f)
        {
            AudioListener.volume = Mathf.Lerp(1f, 0f, timer / masterVolumeFadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
