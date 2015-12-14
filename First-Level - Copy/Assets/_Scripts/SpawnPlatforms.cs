/* Author: Arunan Shan */
/* File: SpawnPlatforms.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: Spawns a platform with coins and enemy*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {

	//PUBLIC INSTANCE VARIABLES
	public int maxPlatforms = 50;
	public GameObject platform;
	public float horizontalMin = 50f;
	public float horizontalMax = 500f;
	public float verticalMin = -50f;
	public float verticalMax = 500f;
	
	
	private Vector2 originPosition;
	
	
	void Start () {
		
		originPosition = transform.position;
		Spawn ();
		
	}
	//Instantiates coin and enemy platform
	void Spawn()
	{
		for (int i = 0; i < maxPlatforms; i++)
		{
			Vector2 randomPosition = originPosition + new Vector2 (Random.Range(horizontalMin, horizontalMax), Random.Range (verticalMin, verticalMax));
			Instantiate(platform, randomPosition, Quaternion.identity);
			originPosition = randomPosition;
		}
	}
	
}