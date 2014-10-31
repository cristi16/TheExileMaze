using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Pitch : MonoBehaviour
{
    [HideInInspector]
    public Vector3 sourcePoint;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(audio.isPlaying)
        {
            float dot = Vector3.Dot(player.transform.forward, (sourcePoint - player.transform.position).normalized);
            if (dot > 0)
            {
                audio.pitch = 1f;
            }
            else
            {
                audio.pitch = Mathf.Lerp(0.7f, 1f, 1f - Mathf.Abs(dot));
            }
        }
    }
}
