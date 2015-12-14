using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Animator _animator;
    private BoxCollider2D _chestCollider;

    PlayerController playerScript;

    private AudioSource[] _audioSources;
    private AudioSource _coins;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
        if (player != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            playerScript = player.GetComponent<PlayerController>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (player == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find ScoreController script for final score referencing to GameOver - finalAcquired Label");
        }
    }
	// Use this for initialization
	void Start () {
        this._rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._chestCollider = gameObject.GetComponent<BoxCollider2D>();

        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._coins = this._audioSources[0];
        this._animator.SetInteger("AnimState", 0); // play idle animation
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
            if (otherCollider.gameObject.CompareTag("Player"))
            {
                //Destroy(otherCollider.gameObject);
                //enemy hit sound
                    this._chestCollider.enabled = false; //so it doesn't check a collision with collision2d - colliding with the player after being hit, let the death animatoin play-out before destroying enemy object
                    this._animator.SetInteger("AnimState", 1); // play death animation
                    this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    for (int i = 0; i < 5; i++ )
                    {
                        this._coins.Play();
                        playerScript.coinCount += 1;
                        playerScript.setCoinCount();
                    }
                Destroy(gameObject, 1.4f);
             }
     }
    void OnTriggerStay2D (Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Arrow"))
        {
            Destroy(otherCollider.gameObject);
            //enemy hit sound
            this._chestCollider.enabled = false; //so it doesn't check a collision with collision2d - colliding with the player after being hit, let the death animatoin play-out before destroying enemy object
            this._animator.SetInteger("AnimState", 1); // play death animation
            this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            for (int i = 0; i < 5; i++)
            {
                this._coins.Play();
                playerScript.coinCount += 1;
                playerScript.setCoinCount();
            }
            Destroy(gameObject, 1.4f);
        }
    }
    /*void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Arrow"))
        {
            Destroy(otherCollider.gameObject);
            //enemy hit sound
            this._chestCollider.enabled = false; //so it doesn't check a collision with collision2d - colliding with the player after being hit, let the death animatoin play-out before destroying enemy object
            this._animator.SetInteger("AnimState", 1); // play death animation
            this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            for (int i = 0; i < 5; i++)
            {
                this._coins.Play();
                playerScript.coinCount += 1;
                playerScript.setCoinCount();
            }
            Destroy(gameObject, 1.4f);
        }
    }*/
}
    

