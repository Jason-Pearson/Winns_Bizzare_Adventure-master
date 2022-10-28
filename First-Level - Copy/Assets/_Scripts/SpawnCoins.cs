/* Author: Arunan Shan */
/* File: SpawnCoin.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: Spawns the coin object*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;

public class SpawnCoins : MonoBehaviour {
	
	public Transform[] coinSpawns;
	public GameObject coin;
	
	// Use this for initialization
	void Start () {
		
		Spawn();
	}

	//spawns coin amount is random
	void Spawn()
	{
		for (int i = 0; i < coinSpawns.Length; i++)
		{
				Instantiate(coin, coinSpawns[i].position, Quaternion.identity);
		}
	}
	
}