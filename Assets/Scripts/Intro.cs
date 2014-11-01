using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public GameObject quad;
    public AudioClip introClip;
    public Texture2D splash;
    public Texture2D title;
    public AnimationCurve splashAlpha;
    public AnimationCurve titleAlpha;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return StartCoroutine(Fade(splash, splashAlpha));

        audio.clip = introClip;
        audio.Play();
        StartCoroutine(Fade(title, titleAlpha));
        yield return new WaitForSeconds(introClip.length);

        Application.LoadLevel(Application.loadedLevel + 1);
    }


    IEnumerator Fade(Texture2D image, AnimationCurve curve)
    {
        quad.renderer.material.mainTexture = image;
        for (float t = 0; t < curve[curve.length - 1].time; t += Time.deltaTime)
        {
            quad.renderer.material.color = new Color(1, 1, 1, curve.Evaluate(t));
            yield return false;
        }
    }
}
