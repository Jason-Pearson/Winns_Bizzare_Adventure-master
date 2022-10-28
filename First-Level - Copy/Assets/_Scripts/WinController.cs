using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinController : MonoBehaviour {

    HighScoreController highscoreScript;
    public Text finalScoreLabel;
    public GameObject highscore;

	// Use this for initialization
	void Awake () {
        highscore = GameObject.FindWithTag("HighScoreController"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
        if (highscore != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            highscoreScript = highscore.GetComponent<HighScoreController>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (highscore == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find ScoreController script for final score referencing to GameOver - finalAcquired Label");
        }
	}

    void Start()
    {
        this.finalScoreLabel.text = "Final Score: " + highscoreScript.keepScore;
    }

	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnRestartButtonClick()
    {
        Destroy(highscore);
        Destroy(highscoreScript);
        Application.LoadLevel("Menu");
    }
}
