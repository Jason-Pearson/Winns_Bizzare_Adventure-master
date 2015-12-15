using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    HighScoreController highscoreScript;
    public Text finalScoreLabel;
    private int loadSameLevel;
    public GameObject highscore;
    public GameObject background;
    public GameObject platform;
    public Material newMatLevel2;
    public Sprite level2Platform;
    public EdgeCollider2D lvl2PlatformCollider;

    public Material newMatLevel3;
    public Sprite level3Platform;
    void Awake()
    {
        this.lvl2PlatformCollider.isTrigger = true;
        highscore = GameObject.FindWithTag("HighScoreController"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
        if (highscore != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            highscoreScript = highscore.GetComponent<HighScoreController>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (highscore == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find ScoreController script for final score referencing to GameOver - finalAcquired Label");
        }

        if(highscoreScript.loadLevelIndex == 3)
        {
            this.lvl2PlatformCollider.isTrigger = false;
            background.GetComponent<Renderer>().material = newMatLevel2;
            platform.GetComponent<SpriteRenderer>().sprite = level2Platform;
            platform.GetComponent<Transform>().transform.localScale = new Vector3(1.5f, 1.5f,1);
            platform.GetComponent<Transform>().transform.position = new Vector3(-399, -251, 0);
        }
        else if (highscoreScript.loadLevelIndex == 4)
        {
            //this.lvl2PlatformCollider.isTrigger = false;
            background.GetComponent<Renderer>().material = newMatLevel3;
            platform.GetComponent<SpriteRenderer>().sprite = level3Platform;
            //platform.GetComponent<Transform>().transform.localScale = new Vector3(1.5f, 1.5f, 1);
            //platform.GetComponent<Transform>().transform.position = new Vector3(-399, -251, 0);
        }
    }

	// Use this for initialization
	void Start () {
        this.finalScoreLabel.text = "Final Score: " + highscoreScript.keepScore;
        this.loadSameLevel = highscoreScript.loadLevelIndex;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnRestartButtonClick()
    {
        if(loadSameLevel == 2)
        {
            Destroy(highscore);
        }
        else
        {
            highscoreScript.keepScore = 0;
        }
        Application.LoadLevel(this.loadSameLevel);
    }

    public void OnMenuButtonClick()
    {
        Destroy(highscore);
        Destroy(highscoreScript);
        Application.LoadLevel("Menu");
    }
}
