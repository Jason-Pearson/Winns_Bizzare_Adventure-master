/* Author: Arunan Shan */
/* File: SpawnEnemy.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: This script spawns enemies on platforms*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
	
	public Transform[] snakeSpawns;
	public GameObject snake;

    public Transform[] wolfSpawns;
    public GameObject wolves;
	// Use this for initialization
	void Start () {
		
		Spawn();
	}
	
	void Spawn()
	{
		for (int i = 0; i < snakeSpawns.Length; i++)
		{
				Instantiate(snake, snakeSpawns[i].position, Quaternion.identity);
		}
        for (int i = 0; i < wolfSpawns.Length; i++)
        {
            Instantiate(wolves, wolfSpawns[i].position, Quaternion.identity);
        }
	}
	
}