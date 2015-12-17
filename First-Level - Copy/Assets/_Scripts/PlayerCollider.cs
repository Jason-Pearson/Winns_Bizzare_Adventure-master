/* Author: Arunan Shan */
/* File: PlayerCollider.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: Controls the score & collider with object*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class PlayerCollider : MonoBehaviour {

	//PUBLIC INSTANCE VARIABLES
	public Text scoreLabel;
	public Text livesLabel;
	public Text gameOverLabel;
    public Text winLabel;
	public Text finalScoreLabel;
	public Text instructLabel;
    public Text instructLabel2;
	public int  scoreValue;
	public int  livesValue;
    //private int buildScore;
    //private int buildLives;
    public int loadlevel = 2; // 2 = level 1, 3 = level 2, 4 = level 3, 5 = Win
    //private bool restart;
    public bool keepScore;
    public bool keepLives;
    public bool showIntructLabel2;
    public HighScoreController highscoreScript;
    //private Transform _transform;

    public Text timerLabel;
    public Text finalTimeLabel;
    public float timer;
    [HideInInspector]
    public float bestTime;

    //private AudioSource[] _audioSources;
   // private AudioSource _portalSound;

    //private Animator _animator;

    void Awake()
    {
        GameObject highscore = GameObject.FindWithTag("HighScoreController"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
        if (highscore != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            highscoreScript = highscore.GetComponent<HighScoreController>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        else if (highscore == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find ScoreController script for final score referencing to GameOver - finalAcquired Label");
        }

        if(this.instructLabel2 != null)
        {
            this.instructLabel2.enabled = false;
        }
    }
	// Use this for initialization
	void Start () {
        //restart = false;

        //if (keepScore == true)
       // {
        //    this.scoreValue = this.highscoreScript.keepScore;
        //    this._SetScoreLives();
       // }
       // else
       // {
            //this.loadlevel = this.highscoreScript.loadLevelIndex;
            this.scoreValue = this.highscoreScript.keepScore;
            this._SetScoreLives();
       // }

		this.gameOverLabel.enabled = false; // Hides end game text 
		this.finalScoreLabel.enabled = false;
		//this.restartLabel.enabled = false;
        this.winLabel.enabled = false;

        if (this.instructLabel != null && this.instructLabel2 != null)
        {
            this.instructLabel.enabled = true;
            this.instructLabel2.enabled = false;
        }
        
        

        this.finalTimeLabel.enabled = false;
        //this._transform = gameObject.GetComponent<Transform>();

        //this._animator = gameObject.GetComponent<Animator>();

        //this._audioSources = gameObject.GetComponents<AudioSource>();
        //this._portalSound = this._audioSources[6];
        
	}
    public void Timer() // method to update to the current score upon killing enemies or picking up Gold and Silver coins
    {
        this.timerLabel.text = String.Format("{0:0}", timer); // label equals this string statement - score is concatenated to string for display

        if (timer <= 5f)
        {
            this.timerLabel.color = Color.red;
        }
        if (timer <= 0)
        {
            bestTime = timer;
            this._EndGame();
        }
    }
	// Update is called once per frame
	void Update () {
	        //if(restart)
            //{
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //Application.LoadLevel(Application.loadedLevel);
            //}

		    //}


    //highscoreScript.keepScore = scoreValue;
    timer -= Time.deltaTime;

    if (timer <= 440f)
    {
        if (this.instructLabel != null && this.instructLabel2 != null)
        {
            this.instructLabel.enabled = false;
            //this.instructLabel2.enabled = true;
        }
    }

        if(showIntructLabel2)
        {
        if (this.instructLabel2 != null)
        {
            this.instructLabel2.enabled = false;
        }
        }
    
    this.Timer();

    this.highscoreScript.loadLevelIndex = this.loadlevel;
    this.highscoreScript.keepScore = this.scoreValue;

	}

	void OnTriggerEnter2D(Collider2D otherGameObject) {
		if (otherGameObject.tag == "GoldCoin") {
			this.scoreValue += 10; // add 10 points
		}


		if (otherGameObject.tag == "Heart") {
			this.livesValue += 1; 
		}


		if (otherGameObject.tag == "Death") {
			this._EndGame();
		}
        if(otherGameObject.tag == "Portal")
        {
            bestTime = timer;
            this._WinGame();
            otherGameObject.GetComponent<AudioSource>().Play();
           // this._portalSound.playOnAwake = true;
        }
		this._SetScoreLives ();

        if(otherGameObject.tag == "PowerUp")
        {
            bestTime = timer;
            this.instructLabel2.enabled = true;
            Invoke("_showInstructLabel2", 5f);
        }
	}

    private void _showInstructLabel2()
    {
        this.showIntructLabel2 = true;
    }

	void OnCollisionEnter2D(Collision2D otherGameObject)
	{

		if (otherGameObject.gameObject.CompareTag ("Snake")) {
			this.livesValue--; // remove one life
			if(this.livesValue <= 0) {
				this._EndGame();
			}
		}

        if (otherGameObject.gameObject.CompareTag("Wolf"))
        {
            this.livesValue--; // remove one life
            if (this.livesValue <= 0)
            {
                this._EndGame();
            }
        }

        if (otherGameObject.gameObject.CompareTag("Black Wolf"))
        {
            this.livesValue--; // remove one life
            if (this.livesValue <= 0)
            {
                this._EndGame();
            }
        }

        if (otherGameObject.gameObject.CompareTag("Turok"))
        {
            this.livesValue -= 2; // remove one life
            if (this.livesValue <= 0)
            {
                this._EndGame();
            }
        }

        if (otherGameObject.gameObject.CompareTag("Snow"))
        {
            this.livesValue -= 1; // remove one life
            if (this.livesValue <= 0)
            {
                this._EndGame();
            }
        }

        if (otherGameObject.gameObject.CompareTag("Harpy"))
        {
            this.livesValue -= 2; // remove one life
            if (this.livesValue <= 0)
            {
                this._EndGame();
            }
        }

        if (otherGameObject.gameObject.CompareTag("Taurus"))
        {
            this.livesValue -= 3; // remove one life
            if (this.livesValue <= 0)
            {
                this._EndGame();
            }
        }
		this._SetScoreLives ();
	}

	// PRIVATE METHODS
	private void _SetScoreLives() {
		this.scoreLabel.text = "Score: " + this.scoreValue;
		this.livesLabel.text = "Lives: " + this.livesValue;
	}
	//ends game displays game over text
	private void _EndGame() {
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.SetActive(false);
        //highscoreScript.keepScore = scoreValue;
        Application.LoadLevel("Game Over");
        /*
        this.scoreLabel.enabled = false;
		this.livesLabel.enabled = false;
		this.gameOverLabel.enabled = true; // Makes game over, final score, restart text appear when game ends 
		this.finalScoreLabel.enabled = true;
		//this.restartLabel.enabled = true;
		this.finalScoreLabel.text = "Final Score: " + this.scoreValue;

        //restart = true;
        /*this.finalTimeLabel.text = "Time Left: " + String.Format("{0:0.000}", bestTime);
        this.finalTimeLabel.enabled = true;*/

	}

    private void _WinGame()
    {
        gameObject.SetActive(false);

        this.scoreLabel.enabled = false;
        this.livesLabel.enabled = false;
        this.winLabel.enabled = true; // Makes game over, final score, restart text appear when game ends 
        this.finalScoreLabel.enabled = true;
        //this.restartLabel.enabled = true;
        this.finalScoreLabel.text = "Final Score: " + this.scoreValue;

        this.finalTimeLabel.text = "Best Time: " + String.Format("{0:0.000}", bestTime);
        this.finalTimeLabel.enabled = true;

        Invoke("_levelLoader",5);
    }

    private void _levelLoader(){
        keepLives = true;
        keepScore = true;
        loadlevel++;
        Application.LoadLevel(loadlevel);
    }

}
