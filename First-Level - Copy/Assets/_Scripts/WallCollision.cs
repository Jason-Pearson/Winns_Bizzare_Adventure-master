using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionStay2D(Collision2D otherGameObject)
    {

        if (otherGameObject.gameObject.CompareTag("Arrow"))
        {
            Destroy(otherGameObject.gameObject);
        }
    }
}
