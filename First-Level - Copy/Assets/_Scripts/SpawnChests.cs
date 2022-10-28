using UnityEngine;
using System.Collections;

public class SpawnChests : MonoBehaviour {

    public Transform[] chestSpawns;
    public GameObject chest;

    // Use this for initialization
    void Start()
    {
        Spawn();
    }

    //spawns coin amount is random
    void Spawn()
    {
        for (int i = 0; i < chestSpawns.Length; i++)
        {
            Instantiate(chest, chestSpawns[i].position, Quaternion.identity);
        }
    }
}
