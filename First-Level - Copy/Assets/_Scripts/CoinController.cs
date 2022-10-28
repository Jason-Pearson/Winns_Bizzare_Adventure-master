/* Author: Arunan Shan */
/* File: CoinController.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: Destroys the coin game object on collision*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinController : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//destroys coin on collision with player
	void OnTriggerEnter2D(Collider2D otherGameObject) 
	{
		if (otherGameObject.tag == "Player") {
		

			Destroy(gameObject);
		}
	

	}


}
