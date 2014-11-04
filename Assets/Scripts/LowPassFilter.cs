using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class LowPassFilter : MonoBehaviour
{   
    public bool useTransformAsSource = false;
    public float minFrequency = 500f;
    public float regularFrequency = 5000f;

    [HideInInspector]
    public Transform sourcePoint;
    private GameObject player;
    private AudioLowPassFilter filter;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        filter = GetComponent<AudioLowPassFilter>();
        if (useTransformAsSource)
            sourcePoint = this.transform;
    }

    void Update()
    {
        if(audio.isPlaying)
        {
            Debug.DrawLine(player.transform.position, sourcePoint.position, Color.red);
            float dot = Vector3.Dot(player.transform.forward, (sourcePoint.position - player.transform.position).normalized);
            if (dot > 0)
            {
                filter.cutoffFrequency = regularFrequency;
            }
            else
            {
                filter.cutoffFrequency = Mathf.Lerp(minFrequency, regularFrequency, 1f - Mathf.Abs(dot));
            }
        }
    }
}
