﻿using UnityEngine;
using System.Collections;

public class Salvation : MonoBehaviour {

    PlayerController player;
    public float winDist = 10f;

	IEnumerator Start () {
        player = FindObjectOfType<PlayerController>();
        bool playing = true;
        while(playing)
        {
            yield return new WaitForSeconds(1f);
            if(Vector3.Distance(player.transform.position, transform.position) < winDist)
            {
                playing = false;
                StartCoroutine(PlayEnd());
            }
        }
	}

	IEnumerator PlayEnd()
    {
        audio.ignoreListenerVolume = true;
        foreach (AudioSource source in GameObject.FindGameObjectWithTag("Salvation").GetComponentsInChildren<AudioSource>())
            source.ignoreListenerVolume = true;
        AudioListener.volume = 0;
        player.FadeOutFootSteps(1f);
        audio.Play();
        player.ignoreInput = true;

        yield return new WaitForSeconds(audio.clip.length);

        yield return new WaitForSeconds(5f);

        StartCoroutine( FadeOut(audio, 5f) );
        foreach (AudioSource source in GameObject.FindGameObjectWithTag("Salvation").GetComponentsInChildren<AudioSource>())
            StartCoroutine(FadeOut(source, 5f));
        yield return new WaitForSeconds(4f);
        Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator FadeOut(AudioSource audio, float time)
    {
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            audio.volume = Mathf.Lerp(audio.volume, 0f, timer / time);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, winDist);
    }
}
