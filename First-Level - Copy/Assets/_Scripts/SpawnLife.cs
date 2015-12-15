/* Author: Arunan Shan */
/* File: SpawnLife.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: Script spawns the heart object on platforms*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;

public class SpawnLife : MonoBehaviour {
	
	public Transform[] lifeSpawns;
	public GameObject life;
	
	// Use this for initialization
	void Start () {
		
		Spawn();
	}

	//Spawns life
	void Spawn()
	{
		for (int i = 0; i < lifeSpawns.Length; i++)
		{
				Instantiate(life, lifeSpawns[i].position, Quaternion.identity);
		}
	}
	
}