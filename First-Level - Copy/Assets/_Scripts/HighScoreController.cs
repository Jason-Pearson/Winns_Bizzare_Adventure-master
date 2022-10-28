using UnityEngine;
using System.Collections;

public class HighScoreController : MonoBehaviour {

    GameObject player;
    PlayerCollider playerScript;
    public int keepScore;
    public int lastScore;
    public int loadLevelIndex;
    void Awake ()
{
    DontDestroyOnLoad(this);
    player = GameObject.FindWithTag("Player"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
    if (player != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
    {
        playerScript = player.GetComponent<PlayerCollider>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
    }
    if (player == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
    {
        Debug.Log("Cannot find ScoreController script for final score referencing to GameOver - finalAcquired Label");
    }

}
	// Use this for initialization
	void Start () {
        //if(playerScript != null)
        //{
            //this.keepScore = playerScript.scoreValue;
        //}

        player = GameObject.FindWithTag("Player"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
        if (player != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            playerScript = player.GetComponent<PlayerCollider>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (player == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find ScoreController script for final score referencing to GameOver - finalAcquired Label");
        }
        this.loadLevelIndex = playerScript.loadlevel;
	}
	
	// Update is called once per frame
	void Update () {
        //if(Application.loadedLevelName == "Game Over")
        //{
            //this.keepScore = 0;
        //}
        //else // if playerscript.level2 = true?
        //{
        //this.keepScore = playerScript.scoreValue;
        //}
        //this.lastScore = this.keepScore;
        //this.loadLevelIndex = playerScript.loadlevel;
	}
}
