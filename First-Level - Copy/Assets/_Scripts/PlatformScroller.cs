using UnityEngine;
using System.Collections;

public class PlatformScroller : MonoBehaviour {

    private Transform _transform;
    private EdgeCollider2D _platCollider;

    PlayerController playerScript;

    public bool plat1;
    public bool plat2;
    /*public bool plat3;
    public bool plat4;
    public bool plat5;*/

    /*public bool start;
    public bool end;*/

    public float speed; // set the speed per unit to move the gameobject
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
        this._transform = gameObject.GetComponent<Transform>();
        this._platCollider = gameObject.GetComponent<EdgeCollider2D>();

	}
	
	// Update is called once per frame
	void Update () {
        if(playerScript.plat1 && this.plat1)
        {
            if (playerScript.start)
            {
                //playerScript.speed = this.speed;
                Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
                currentPosition.x += this.speed; // increment the new position by the speed
                //this._transform.position = currentPosition; // then assign the new position to the gameobject's transform
                gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform    
                //playerScript.gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform    
                if (playerScript._movingValue == 0)
                {
                    //playerScript.gameObject.GetComponent<Transform>().position = new Vector2(currentPosition.x, playerScript.gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform    
                    playerScript.gameObject.GetComponent<Transform>().position = new Vector2((playerScript.gameObject.GetComponent<Transform>().position.x + this.speed), playerScript.gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform    

                }
                else if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
                {

                }
                /*else
                {
                    playerScript.gameObject.GetComponent<Transform>().position = new Vector2(currentPosition.x, playerScript.gameObject.GetComponent<Transform>().position.y);
                }*/

            }
            else if (playerScript.end)
            {
                //playerScript.speed = this.speed;
                Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
                currentPosition.x -= this.speed; // increment the new position by the speed
                //this._transform.position = currentPosition; // then assign the new position to the gameobject's transform
                gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform            
                if (playerScript._movingValue == 0)
                {
                    playerScript.gameObject.GetComponent<Transform>().position = new Vector2((playerScript.gameObject.GetComponent<Transform>().position.x - this.speed), playerScript.gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform    
                }
                else if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
                {

                }
                /*else
                {
                    playerScript.start = false;
                    playerScript.end = false;
                }*/
            }
            else if ((!playerScript.start && !playerScript.end) || playerScript.reset)
            {
                gameObject.GetComponent<Transform>().position = new Vector2(8412f, 1679.5f);  // then assign the new position to the gameobject's transform            
            }
        }

        if (playerScript.plat2 && this.plat2)
        {
            if (playerScript.start)
            {
                //playerScript.speed = this.speed;
                Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
                currentPosition.y += this.speed; // increment the new position by the speed
                //this._transform.position = currentPosition; // then assign the new position to the gameobject's transform
                gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform    
                //playerScript.gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform    
                if (playerScript._movingValue == 0)
                {
                    this.speed = 4f;
                    //playerScript.gameObject.GetComponent<Transform>().position = new Vector2(currentPosition.x, playerScript.gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform    
                    playerScript._rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll; //RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    playerScript._rigidBody2D.gravityScale = 0;
                    playerScript.gameObject.GetComponent<Transform>().position = new Vector2(7057f, playerScript.gameObject.GetComponent<Transform>().position.y + this.speed); // then assign the new position to the gameobject's transform    

                }
                else if(playerScript._movingValue != 0)
                {
                    this.speed = 0f;
                    //gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform            
                    playerScript._rigidBody2D.gravityScale = 170;
                    playerScript._rigidBody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

                }
                else if ((Input.GetKey("up") || Input.GetKey(KeyCode.W)) && (playerScript._movingValue != 0))
                {
                    playerScript._rigidBody2D.gravityScale = 170;
                    playerScript._rigidBody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform            

                }
                /*else
                {
                    playerScript.gameObject.GetComponent<Transform>().position = new Vector2(currentPosition.x, playerScript.gameObject.GetComponent<Transform>().position.y);
                }*/

            }
            else if (playerScript.end)
            {
                //playerScript.speed = this.speed;
                Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
                currentPosition.y -= this.speed; // increment the new position by the speed
                //this._transform.position = currentPosition; // then assign the new position to the gameobject's transform
                gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform            
                if (playerScript._movingValue == 0)
                {
                    this.speed = 4f;
                    //playerScript.gameObject.GetComponent<Transform>().position = new Vector2(currentPosition.x, playerScript.gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform    
                    playerScript._rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll; //RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    playerScript._rigidBody2D.gravityScale = 0;
                    playerScript.gameObject.GetComponent<Transform>().position = new Vector2(7057f, playerScript.gameObject.GetComponent<Transform>().position.y - this.speed); // then assign the new position to the gameobject's transform    

                }
                else if (playerScript._movingValue != 0)
                {
                    this.speed = 0f;
                    //gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y); // then assign the new position to the gameobject's transform            
                    playerScript._rigidBody2D.gravityScale = 170;
                    playerScript._rigidBody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

                }
                else if ((Input.GetKey("up") || Input.GetKey(KeyCode.W)) && (playerScript._movingValue != 0))
                {
                    playerScript._rigidBody2D.gravityScale = 170;
                    playerScript._rigidBody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y);  // then assign the new position to the gameobject's transform            
                    
                }
                /*else
                {
                    playerScript.start = false;
                    playerScript.end = false;
                }*/
            }
            else if (!playerScript.start && !playerScript.end && !playerScript.reset)
            {
                playerScript._rigidBody2D.gravityScale = 170;
                playerScript._rigidBody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y);  // then assign the new position to the gameobject's transform            
            }
            else if(playerScript.reset)
            {
                gameObject.GetComponent<Transform>().position = new Vector2(7057f, 2454f); // then assign the new position to the gameobject's transform            
            }
        }
           
	}

    /*void OnTriggerExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Start")
        {
            this.start = true;
            this.end = false;
        }

        if (other.gameObject.tag == "End")
        {
            this.start = false;
            this.end = true;
        }
    }
    void OnTriggerStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Start")
        {
            this.start = true;
            this.end = false;
        }

        if (other.gameObject.tag == "End")
        {
            this.start = false;
            this.end = true;
        }
    }
    void OnTriggerEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Start")
        {
            this.start = true;
            this.end = false;
        }

        if (other.gameObject.tag == "End")
        {
            this.start = false;
            this.end = true;
        }
    }*/
     
}
