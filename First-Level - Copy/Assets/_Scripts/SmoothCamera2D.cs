/* Author: Arunan Shan */
/* File: SmoothCamera2D.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: Makes the main camera follow the player*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
	// Update is called once per frame
	void Update () 
	{
	// Follows transform of player
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}
}