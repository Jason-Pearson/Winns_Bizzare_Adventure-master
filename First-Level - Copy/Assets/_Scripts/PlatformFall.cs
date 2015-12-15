/* Author: Arunan Shan */
/* File: PlatformFall.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: This script makes the platform fall once player steps on it*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;

public class PlatformFall : MonoBehaviour {
	
	public float fallDelay = 1f;
	
	
	private Rigidbody2D rb2d;
	
	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Invoke ("Fall", fallDelay);
		}
	}
	
	void Fall()
	{
		rb2d.isKinematic = false;
	}
	
	
	
}