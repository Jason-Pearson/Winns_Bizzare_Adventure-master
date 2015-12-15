/* Author: Jason Pearson
 * File: ShotMover.cs
 * Creation Date: Sept 29, 2015
 * Description: This script controls the shot movement and control the gameobject's destruction for player Blast and enemy Laser
 * Last Modified Date: Oct 5, 2015
 */
using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour {

    //PUBLIC INSTANCE VARIABLES
    public float speed; // set the speed per unit to move the gameobject

    public GameObject arrow; //reference for the 2d spriterenderer of the sprite image in the 2D sprite object you are using to represent a shot
    //public GameObject blast, laser; //(1) gameobject reference for the player blast and enemy laser - in order to -
	// Use this for initialization
    public bool greenArrow;
    public bool Snow;
	
    void Start()
    {
        if(greenArrow)
        {
            Destroy(gameObject, 0.6f);

        }
        else if(Snow)
        {
            Destroy(gameObject, 0.65f);

        }
        else
        {
            Destroy(gameObject, 1f);

        }
    }
    void Update () {
        if(Snow)
        {
            Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
            currentPosition.y -= this.speed; // increment the new position by the speed
            gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform
        }
        else if (greenArrow)
        {
            Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
            currentPosition.y += this.speed; // increment the new position by the speed
            gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform
        }
        else
        {
            Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
            currentPosition.x += this.speed; // increment the new position by the speed
            gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform
        }

        /*SpriteRenderer arrowSprite = gameObject.GetComponent<SpriteRenderer>();
        //destroy game object (player blast or enemy laser) upon leaving the camera's view - checks every frame via Update
        if(!arrowSprite.isVisible)// mind you that isVisible is checking the view of an object from (any) camera - but since the main, top down camera is the only camera, it is okay that this code can stick to it
        {
            Destroy(gameObject);
        }*/
	}
    /*void OnTriggerEnter2D(Collider2D otherGameObject) //(2) - control destroying the shot gameobjects according to -
    {
        if (otherGameObject.tag == "Player")//(3) - destroying the (laser) 2D sprite gameobject when it hits the player -
        {
            Debug.Log("Enemy Laser hit Player"); //for debugging
            Destroy(laser);
        }
        if (otherGameObject.tag == "Enemy")//(4) - or destroying the (blast) 2D sprite gameobject when it hits the enemy 
        {
            Debug.Log("Player Blast hit Enemy"); //for debugging
            Destroy(blast);
        }
    }*/

    void OnCollisionEnter2D(Collision2D other)
    {
        if(!greenArrow && !Snow)
        {
            if (other.gameObject.tag == "Chest")
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

        }
        
        if(greenArrow || Snow)
        {
            if (other.gameObject.tag == "Platform")
            {
                Destroy(gameObject);
            }
        }
        
    }
}
