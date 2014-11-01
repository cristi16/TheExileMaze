using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public AudioClip introClip;

	// Use this for initialization
	IEnumerator Start () {
        audio.clip = introClip;
        audio.Play();

        yield return new WaitForSeconds(introClip.length);

        Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
