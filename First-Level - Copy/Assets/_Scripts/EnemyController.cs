using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // PUBLIC INSTANCE VARIABLES
    public float speed = 100f;
    public int hit;

    //public Transform sightStart;
    public Transform sightEnd;
    //public GameObject shot;
    //public Transform shotSpawn; //this variable is a refernece of the game object Shot Spawn but the variable type only references its transform component

    // PRIVATE INSTANCE VARIABLES
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Animator _animator;
    private PolygonCollider2D _enemyCollider;
    private bool _isGrounded = false;
    private bool _isGroundAhead = true;

    private AudioSource[] _audioSources;
    private AudioSource _hit;
    private AudioSource _death;

    public bool blackWolf;
    public bool Turok;
    public bool Harpy;
    public bool Taurus;

    public float fireRate; // = 0.25 --> shoots 4 times a second --> 1/0.25
    private float nextFire; // = 0 
    public Transform shotSpawn;
    public GameObject Snow;

    public Transform target;
    public PlayerController playerScript;

    private float _distanceFromTarget;
    void Awake()
    {
        if(blackWolf)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        if(Harpy)
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
        
    }
    // Use this for initialization
    void Start()
    {
        this._rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._enemyCollider = gameObject.GetComponent<PolygonCollider2D>();

        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._hit = this._audioSources[0];
        this._death = this._audioSources[1];
    }

    void Update()
    {
        if(Harpy)
        {
            
                    if (Time.time > nextFire) //shoot every 0.5 sec by - game time > nextFire = 0
                    {
                        nextFire = Time.time + fireRate; // then update nextFire = gametime (now 0.2) + fireRate (0.25) --> then when game time is 0.27 > nextFire (0.26) and fire button is held = shoot Bolt prefab via the reference shot gameObject 
                        Instantiate(Snow, shotSpawn.position, Snow.transform.rotation);//instantiate the game object shot per frame at a held key press, set at a vector3 position, at a set quaternion euler (rotation)
                        Snow.GetComponent<AudioSource>().playOnAwake = true;//upon instantiating the (shot), if the audio isn't playing on awake (on the very first frame), play this audio clip
                    }
        }
        
        if(blackWolf)
        {
            this._distanceFromTarget = Vector3.Distance(this._transform.position, this.target.position);
            //Debug.Log(this._distanceFromTarget);
            if (this._distanceFromTarget < 275)
            {
                if(hit > 0) //in order to stop Black Wolf from speeding off during Death animation
                {
                    this.speed = 400f;
                }
            /*// move towards the target
            this._transform.position = Vector3.MoveTowards(this._transform.position, target.position, this.speed);

            // look at the target
            Vector3 targetDir = this.target.position - this._transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, this.speed, 0.0F);
            this._transform.rotation = Quaternion.LookRotation(newDir);*/
            }
            else 
            {
                this.speed = 200f;
            }
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // check if enemy is grounded
        if (this._isGrounded)
        {
            this._animator.SetInteger("AnimState", 0); // play walking animation -
            this._rigidbody2D.velocity = new Vector2(this._transform.localScale.x, 0) * -this.speed; // and have enemy's velocity go forward by speed

            this._isGroundAhead = Physics2D.Linecast(this._transform.position, this.sightEnd.position, 1 << LayerMask.NameToLayer("Ground")); // linecast between enemy's transform and the ground's - returns a boolean value
            Debug.DrawLine(this._transform.position, this.sightEnd.position);

            if (this._isGroundAhead == false) // when the line cast is past the end position of the ground layer == false
            {
                /*if(blackWolf)
                {
                    if (this._transform.localScale.x == 1)
                    {
                        this.gameObject.transform.position = new Vector2((_transform.position.x + 100), _transform.position.y);
                    }
                    else
                    {
                        this.gameObject.transform.position = new Vector2((_transform.position.x - 100), _transform.position.y);
                    }
                }
                if (Turok)
                {
                    if (this._transform.localScale.x == 1)
                    {
                        this.gameObject.transform.position = new Vector2((_transform.position.x + 150), _transform.position.y);
                    }
                    else
                    {
                        this.gameObject.transform.position = new Vector2((_transform.position.x - 150), _transform.position.y);
                    }
                }*/
                this._flip(); // flip enemy local scale (invert sprite and gameobject's direction)
            }

        }
        else //if enemy is not grounded -
        {
            //this._animator.SetInteger("AnimState", 1); // play idle animation
        }
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Snake"))
        {
            this._flip();
        }

        if (otherCollider.gameObject.CompareTag("Wolf"))
        {
            this._flip();
        }

        if (otherCollider.gameObject.CompareTag("Harpy"))
        {
            this._flip();
        }

        if (otherCollider.gameObject.CompareTag("Taurus"))
        {
            this._flip();
        }

        if (otherCollider.gameObject.CompareTag("Black Wolf"))
        {
            /*if(Turok)
            {
                if (this.gameObject.transform.localScale == new Vector3 (1,1,1))
                {
                    this._flip();
                    this.gameObject.transform.position = new Vector2((_transform.position.x + 100),_transform.position.y);
                }
                else if (this.gameObject.transform.localScale == new Vector3(-1, 1, 1))
                {
                    this._flip();
                    this.gameObject.transform.position = new Vector2((_transform.position.x - 100), _transform.position.y);
                }
                
            }
            else
            {
                if (this.gameObject.transform.localScale == new Vector3(1, 1, 1))
                {
                    this._flip();
                    this.gameObject.transform.position = new Vector2((_transform.position.x + 50), _transform.position.y);
                }
                else if (this.gameObject.transform.localScale == new Vector3(-1, 1, 1))
                {
                    this._flip();
                    this.gameObject.transform.position = new Vector2((_transform.position.x - 50), _transform.position.y);
                }
            }*/
            this._flip();
        }

        if (otherCollider.gameObject.CompareTag("Turok"))
        {
            /*if (blackWolf)
            {
                if (this.gameObject.transform.localScale == new Vector3(1, 1, 1))
                {
                    this._flip();
                    this.gameObject.transform.position = new Vector2((_transform.position.x + 100), _transform.position.y);
                }
                else if (this.gameObject.transform.localScale == new Vector3(-1, 1, 1))
                {
                    this._flip();
                    this.gameObject.transform.position = new Vector2((_transform.position.x - 100), _transform.position.y);
                }
            }
            else
            {
                if (this.gameObject.transform.localScale.x == 1) //CHANGE IT TO THIS AND NOW IT WORKS...OKAY...
                {
                    this._flip(); //TUROK WILL NOT FLIP WITH ANOTHER TUROK IF LOCAL SCALE EQUAL 1(RIGHT) - THIS WHOLE CODE DOESN'T EXECUTE AT ALL
                    this.gameObject.transform.position = new Vector2((_transform.position.x + 100), _transform.position.y);
                }
                else if (this.gameObject.transform.localScale == new Vector3(-1, 1, 1))
                {
                    this._flip();
                    this.gameObject.transform.position = new Vector2((_transform.position.x - 100), _transform.position.y);
                }
            }*/
            this._flip();
        }

        if (otherCollider.gameObject.CompareTag("Arrow"))
        {
            Destroy(otherCollider.gameObject);
            hit--;
            //enemy hit sound
            if (hit <= 0)
            {
                this.fireRate = 100f;
                this._death.Play();
                this._enemyCollider.isTrigger = true; //so it doesn't check a collision with collision2d - colliding with the player after being hit, let the death animatoin play-out before destroying enemy object
                this.speed = 0f;
                this._animator.SetInteger("AnimState", 1); // play death animation
                this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                if(blackWolf)
                {
                    Invoke("_DeathAnimationTransition", 0.185f);
                }

                if(Harpy)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        playerScript._goldCoinSound.Play();
                        playerScript.coinCount += 1;
                        playerScript.setCoinCount();
                    }
                    Destroy(gameObject, 0.75f);
                }
                else
                {
                    Destroy(gameObject, 0.85f);
                }
            }
            else // for hit animation of all enemies - with special consideration for blackwolfs and turoks
            {
                if (blackWolf)
                {
                    Invoke("_HitAnimationTransition", 0.06f);
                    Invoke("_HitAnimationReset", 0.23f);
                }
                else if (Turok)
                {
                    Invoke("_HitAnimationTransition", 0.0205f);
                    Invoke("_HitAnimationReset", 0.199f);
                }
                this._hit.Play();
                this._animator.SetInteger("AnimState", 2); // play hit animation
            }
        }

        if(Harpy)
        {
            if (otherCollider.gameObject.CompareTag("Arrow2"))
            {
                Destroy(otherCollider.gameObject);
                hit -= 3;
                //enemy hit sound
                if (hit <= 0)
                {
                    this._death.Play();
                    this._enemyCollider.isTrigger = true; //so it doesn't check a collision with collision2d - colliding with the player after being hit, let the death animatoin play-out before destroying enemy object
                    this.speed = 0f;
                    this._animator.SetInteger("AnimState", 1); // play death animation
                    this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                    for (int i = 0; i < 10; i++)
                    {
                        playerScript._goldCoinSound.Play();
                        playerScript.coinCount += 1;
                        playerScript.setCoinCount();
                    }

                    Destroy(gameObject, 0.75f);
                }
            }
        }
        
    }

    private void _DeathAnimationTransition()
    {
        if (this.gameObject.CompareTag("Black Wolf"))
        {
            this.gameObject.transform.position = new Vector2(_transform.position.x, (_transform.position.y - 30));
        }
    }

    private void _HitAnimationTransition()
    {
        if (this.gameObject.CompareTag("Black Wolf"))
        {
            this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            this.gameObject.transform.position = new Vector2(_transform.position.x, (_transform.position.y - 20));
        }

        if (this.gameObject.CompareTag("Turok"))
        {
            this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            this.gameObject.transform.position = new Vector2(_transform.position.x, (_transform.position.y - 55));
        }
    }
    private void _HitAnimationReset()
    {
        if (this.gameObject.CompareTag("Black Wolf"))
        {
            this._rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            this.gameObject.transform.position = new Vector2(_transform.position.x, (_transform.position.y + 20));
        }

        if (this.gameObject.CompareTag("Turok"))
        {
            this._rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            this.gameObject.transform.position = new Vector2(_transform.position.x, (_transform.position.y + 55));
        }
    }

    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }

        if(blackWolf || Turok || Harpy || Taurus)
        {
            if (otherCollider.gameObject.CompareTag("Player"))
            {
                //this.gameObject.transform.position.y = new Vector2(stopX, stopY);
                this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                this._enemyCollider.isTrigger = true;
            }
        }
        /*if (Turok)
        {
            if (otherCollider.gameObject.CompareTag("Player"))
            {
                //this.gameObject.transform.position.y = new Vector2(stopX, stopY);
                this._rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                this._enemyCollider.isTrigger = true;
            }
        }*/
        
    }

    void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = false;
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }

        if (blackWolf || Turok || Harpy || Taurus)
        {
            if (otherCollider.gameObject.CompareTag("Player"))
            {
                //this.gameObject.transform.position.y = new Vector2(stopX, stopY);
                if(hit > 0)
                {
                    this._enemyCollider.isTrigger = false;
                }


                if(Harpy)
                {
                    this._rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                }
                else
                {
                    this._rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                }

            }
        }

        /*if (Turok)
        {
            if (otherCollider.gameObject.CompareTag("Player"))
            {
                //this.gameObject.transform.position.y = new Vector2(stopX, stopY);
                if (hit > 0)
                {
                    this._enemyCollider.isTrigger = false;
                } 
                this._rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }
        }*/
    }
    // PRIVATE METHODS
    private void _flip()
    {
        if (this._transform.localScale.x == 1)
        {
            if(blackWolf)
            {
                this.gameObject.transform.position = new Vector2((_transform.position.x + 100), _transform.position.y);
            }
            if (Turok)
            {
                this.gameObject.transform.position = new Vector2((_transform.position.x + 150), _transform.position.y);
            }
            this._transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            if (blackWolf)
            {
                this.gameObject.transform.position = new Vector2((_transform.position.x - 100), _transform.position.y);
            }
            if (Turok)
            {
                this.gameObject.transform.position = new Vector2((_transform.position.x - 150), _transform.position.y);
            }
            this._transform.localScale = new Vector3(1f, 1f, 1f); // reset to normal scale
        }
    }
}
