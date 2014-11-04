using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour
{
    public GameObject quad;
    public Texture2D instructions;
    public Texture2D theEnd;
    public AnimationCurve inAlpha;
    public AnimationCurve outAlpha;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Fade(instructions, inAlpha));
    }

    public void DoEnding()
    {
        StartCoroutine(TheEnd());
    }

    IEnumerator TheEnd()
    {
        yield return StartCoroutine(Fade(instructions, outAlpha));
        yield return StartCoroutine(Fade(theEnd, inAlpha));
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
