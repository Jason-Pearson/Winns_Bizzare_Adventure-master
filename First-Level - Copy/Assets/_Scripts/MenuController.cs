using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    // Start Button Event Handler
    public void OnPlayButtonClick()
    {
        Application.LoadLevel("Main");
    }

    // Start Button Event Handler
    public void OnInstructButtonClick()
    {
        Application.LoadLevel("Controls");
    }

}
