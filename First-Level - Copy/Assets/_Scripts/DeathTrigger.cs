/* Author: Arunan Shan */
/* File: DeathTrigger.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: This script resets game when player falls from platform*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	private AudioSource[] _audioSources;
	private AudioSource _deathAudioSource;

	// Use this for initialization
	void Start () {
		this._audioSources = this.GetComponents<AudioSource> ();
		this._deathAudioSource = this._audioSources [0];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		    {
			this._deathAudioSource.Play ();
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}

