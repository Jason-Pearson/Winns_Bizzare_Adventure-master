/* Author: Arunan Shan */
/* File: PlayerController.cs */
/* Creation Date: Oct 19, 2015 */
/* Description: Controls player movement, audio and animation*/
/* Last Modified by: Monday October 25, 2015 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//VELOCITY RANGE
[System.Serializable]
public class VelocityRange {
	public float vMin, vMax;

	public VelocityRange(float vMin, float vMax)
	{
		this.vMin = vMin;
		this.vMax = vMax;
	}
	}

public class PlayerController : MonoBehaviour {
	//PUBLIC INSTANCE VARIABLES
	public float speed = 50f;
	public float jump = 500f;


	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);

    public GameObject arrow; // reference for shot gameobject to hold the 2D Blast sprite object
    public Transform arrowSpawn; // holds transform position of the empty gameobject ShotSpawn - which is a child of the shot gameobject to instantiate in front of the player
    public float fireRate; // = 0.25 --> shoots 4 times a second --> 1/0.25
    private float nextFire; // = 0 

    public GameObject arrow2;
    public Transform arrow2Spawn;
    public bool greenArrow;

    public GameObject portal;
    public Transform portalSpawn;

    public Text coinCountLabel;
    public int coinCount = 0;

	//PRIVATE INSTANCE VARIABLES
	public Rigidbody2D _rigidBody2D;
	private Transform _transform;
	private Animator _animator;
	private AudioSource[] _audioSources;
	public AudioSource _goldCoinSound;
	private AudioSource _jumpSound;
	private AudioSource _hitSound;
	private AudioSource _lifeSound;
	private AudioSource _gameOverSound;
    private AudioSource _shoot;
    private AudioSource _greenArrow;

    public ShotMover Arrow;

	public float _movingValue = 0;
	private bool _isFacingRight = true;
	private bool _isGrounded =true;

    public bool level2;
    public bool level3;
    private bool dimLights = false;
    public Light light;

    public bool start;
    public bool end;
    public bool reset;
    /*private SpriteRenderer _portalSprite;
    private BoxCollider2D _portalCollider;*/

    void Awake()
    {
        /*Instantiate(portal, portalSpawn.position, portalSpawn.rotation);//instantiate the game object shot per frame at a held key press, set at a vector3 position, at a set quaternion euler (rotation)
        _portalSprite = portal.GetComponent<SpriteRenderer>();
        _portalCollider = portal.GetComponent<BoxCollider2D>();
        _portalSprite.enabled = false;
        _portalCollider.enabled = false;*/
        this.portal.SetActive(false);
    }
	// Use this for initialization
	void Start () {
		this._rigidBody2D = gameObject.GetComponent<Rigidbody2D> ();
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();

		//Audio Array
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._goldCoinSound = this._audioSources [0];
		this._jumpSound = this._audioSources [1];
		this._hitSound = this._audioSources [2];
		this._lifeSound = this._audioSources [3];
		this._gameOverSound = this._audioSources [4];
        this._shoot = this._audioSources[5];
        this._greenArrow = this._audioSources[7];

        if(level2)
        {
            this.coinCountLabel.text = "Coins for Portal: " + this.coinCount + "/75";

        }
        else if(level3)
        {
            this.coinCountLabel.text = "Coins for Portal: " + this.coinCount + "/100";
        }
        else
        {
            this.coinCountLabel.text = "Coins for Portal: " + this.coinCount + "/45";
        }
        
	}
    void Update()
    {
        if (level2)
        {
            if (this.coinCount >= 75)
            {
                this.coinCountLabel.color = Color.blue;
                /*this._portalSprite.enabled = true;
                this._portalCollider.enabled = true;*/
                this.portal.SetActive(true);
                this.portal.GetComponent<AudioSource>().Play();
                Debug.Log(this.portal.activeInHierarchy);
            }
        }
        else if (level3)
        {
            if (this.coinCount >= 100)
            {
                this.coinCountLabel.color = Color.blue;
                /*this._portalSprite.enabled = true;
                this._portalCollider.enabled = true;*/
                this.portal.SetActive(true);
                this.portal.GetComponent<AudioSource>().Play();
                Debug.Log(this.portal.activeInHierarchy);
            }
        }
        else
        {
            if (this.coinCount >= 45)
            {
                this.coinCountLabel.color = Color.blue;
                /*this._portalSprite.enabled = true;
                this._portalCollider.enabled = true;*/
                this.portal.SetActive(true);
                this.portal.GetComponent<AudioSource>().Play();
                Debug.Log(this.portal.activeInHierarchy);
            }
        }

        if(dimLights)
        {
            if (this.light != null)
            {
                if(level3)
                {
                    this.light.intensity -= 0.04f;
                }
                else
                {
                    this.light.intensity -= 0.025f;
                }
            }
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;

		float absVelX = Mathf.Abs (this._rigidBody2D.velocity.x);
		float absVelY = Mathf.Abs (this._rigidBody2D.velocity.y);

		this._movingValue = Input.GetAxis ("Horizontal"); //value is between -1 and 1

		//check is player is moving

		if (this._movingValue != 0) {
			//we're moving
			this._animator.SetInteger("AnimState", 1); //play walk clip
			if(this._movingValue > 0)
			{
				//moving right
				if (absVelX < this.velocityRange.vMax){
					 forceX = this.speed;
					this._isFacingRight = true;
					this._flip();
				}
			}
			else if (this._movingValue < 0)
			{
				//moving left 
				if (absVelX < this.velocityRange.vMax){
					forceX = -this.speed;
					this._isFacingRight = false;
					this._flip();
				}
			}
		} else if (this._movingValue == 0) {
			//we're idle
			this._animator.SetInteger("AnimState", 0); 
		}

		//Check if player is jumping

		if (Input.GetKey ("up") || Input.GetKey (KeyCode.W)) {
			//check if player is grounded
			if(this._isGrounded){
			//player is jumping
				this._animator.SetInteger("AnimState", 3);
			if (absVelY < this.velocityRange.vMax) {
				forceY = this.jump;
					this._jumpSound.Play ();
					this._isGrounded = false;
			}
			}
		}
		
		this._rigidBody2D.AddForce (new Vector2 (forceX, forceY));

        if (arrow != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            Arrow = arrow.GetComponent<ShotMover>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (arrow == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find SHOTMOVER");
        }

        if (greenArrow)
        {
            if (Input.GetButton("Fire2") && Time.time > nextFire) //Hold key and shoot every 0.25 sec by - game time > nextFire = 0
            {
                this._animator.SetInteger("AnimState", 4);
                nextFire = Time.time + fireRate; // then update nextFire = gametime (now 0.2) + fireRate (0.25) --> then when game time is 0.27 > nextFire (0.26) and fire button is held = shoot Bolt prefab via the reference shot gameObject 
                Instantiate(arrow2, arrow2Spawn.position, arrow2.transform.rotation);//instantiate the game object shot per frame at a held key press, set at a vector3 position, at a set quaternion euler (rotation)
                //arrow.GetComponent<AudioSource>().playOnAwake = true;//upon instantiating the (shot), if the audio isn't playing on awake (on the very first frame), play this audio clip
                this._shoot.Play();
            }
        }
        
        if (Input.GetButton("Fire1") && Time.time > nextFire) //Hold key and shoot every 0.25 sec by - game time > nextFire = 0
        {
            this._animator.SetInteger("AnimState", 4);
            nextFire = Time.time + fireRate; // then update nextFire = gametime (now 0.2) + fireRate (0.25) --> then when game time is 0.27 > nextFire (0.26) and fire button is held = shoot Bolt prefab via the reference shot gameObject 
            if(_isFacingRight)
            {
            Arrow.speed = 10;
            arrowSpawn.rotation = Quaternion.Euler(0, 180, 0);
            Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);//instantiate the game object shot per frame at a held key press, set at a vector3 position, at a set quaternion euler (rotation)
            }
            else
            {
                Arrow.speed = -10;
                arrowSpawn.rotation = Quaternion.Euler(0, 0, 0);
                Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);//instantiate the game object shot per frame at a held key press, set at a vector3 position, at a set quaternion euler (rotation)
            }
            //arrow.GetComponent<AudioSource>().playOnAwake = true;//upon instantiating the (shot), if the audio isn't playing on awake (on the very first frame), play this audio clip
            this._shoot.Play();
        }

	}
	void OnTriggerEnter2D(Collider2D otherGameObject) 
	{
		if (otherGameObject.tag == "GoldCoin") {
			this._goldCoinSound.Play (); //play coin sound
            coinCount++;
            setCoinCount();
		}
////		if (otherGameObject.tag == "Snake") {
////			this._animator.SetInteger("AnimState", 3);
////			this._hitSound.Play (); //play hit sound
////
////			
//		}
		if (otherGameObject.tag == "Heart") {
			this._lifeSound.Play (); //play life sound
			
		}
		if (otherGameObject.tag == "Death") {
			this._gameOverSound.Play ();
		}

        if(otherGameObject.tag == "Dim")
        {
            this.dimLights = true;
        }

        if(otherGameObject.tag == "PowerUp")
        {
            this._greenArrow.Play();
            this.greenArrow = true;
            Destroy(otherGameObject.gameObject);
        }

        if (otherGameObject.tag == "Start")
        {
            this.start = true;
            this.end = false;
        }

        if (otherGameObject.tag == "End")
        {
            this.start = false;
            this.end = true;
        }

        if (otherGameObject.tag == "Stop")
        {
            this.start = false;
            this.end = false;
        }

        /*if (otherGameObject.tag == "Reset")
        {
            this.reset = true;
        }*/
    
	}

    void OnTriggerStay2D(Collider2D otherGameObject)
    {
        if (otherGameObject.tag == "Reset")
        {
            this.reset = true;
        }
    }

    void OnTriggerExit2D(Collider2D otherGameObject)
    {
        if (otherGameObject.tag == "Reset")
        {
            this.reset = false;
        }
    }
	void OnCollisionEnter2D(Collision2D otherGameObject)
	{
		if (otherGameObject.gameObject.CompareTag ("Snake")) {
			this._animator.SetInteger("AnimState", 2);
			this._hitSound.Play (); //play hit sound
		}
        if (otherGameObject.gameObject.CompareTag("Wolf"))
        {
            this._animator.SetInteger("AnimState", 2);
            this._hitSound.Play(); //play hit sound
        }
        if (otherGameObject.gameObject.CompareTag("Black Wolf"))
        {
            this._animator.SetInteger("AnimState", 2);
            this._hitSound.Play(); //play hit sound
        }
        if (otherGameObject.gameObject.CompareTag("Turok"))
        {
            this._animator.SetInteger("AnimState", 2);
            this._hitSound.Play(); //play hit sound
        }
        if (otherGameObject.gameObject.CompareTag("Snow"))
        {
            Destroy(otherGameObject.gameObject);
            this._animator.SetInteger("AnimState", 2);
            this._hitSound.Play(); //play hit sound
        }
        if (otherGameObject.gameObject.CompareTag("Harpy"))
        {
            this._animator.SetInteger("AnimState", 2);
            this._hitSound.Play(); //play hit sound
        }
        if (otherGameObject.gameObject.CompareTag("Taurus"))
        {
            this._animator.SetInteger("AnimState", 2);
            this._hitSound.Play(); //play hit sound
        }
	}


	void OnCollisionStay2D(Collision2D otherCollider)
	{
		if (otherCollider.gameObject.CompareTag ("Platform")) {
			this._isGrounded = true;
		}
	}

    public void setCoinCount()
    {
        if (level2)
        {
            this.coinCountLabel.text = "Coins for Portal: " + this.coinCount + "/75";

        }
        else if (level3)
        {
            this.coinCountLabel.text = "Coins for Portal: " + this.coinCount + "/100";
        }
        else
        {
            this.coinCountLabel.text = "Coins for Portal: " + this.coinCount + "/45";
        }
    }
	//flips player sprite
	private void _flip(){
		if(this._isFacingRight){
			this._transform.localScale = new Vector3(1f, 1f, 1f);// flip back to normal
		}
		else{
			this.transform.localScale = new Vector3(-1f, 1f, 1f);//flip other way
			}
	}


}
