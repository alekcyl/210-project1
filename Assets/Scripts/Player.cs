using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //references
    public GameObject parent;
    public Animator ani;
    public CharacterController charController;
    public Material playerMaterial;

    //movement
    public float gravity = -9.8f;
    public float jumpSpeed;
    public float verticalSpeed;
    public float moveSpeed;
    public float maxCoyote;
    public float curCoyote;
    public bool canJump;
    public bool isGrounded;
    public bool hasJumped;


    //lighting
    public bool inLight;
    public float inLightTimerCur;
    public float inLightTimerMax;
    public float maxLightDetectionNumber;

    //seen
    public bool isSeen;
    public float seenTimerCur;
    public float seenTimerMax;

    //life
    public bool isAlive;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        playerMaterial.color = Color.white;
        isAlive = true;
        canJump = true;

    }

    // Update is called once per frame
    private void Update()
    {
        //reload current scene if dead
        if(isAlive == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //is currently used, but gives player reference to enemies in map
        checkEnemies();

        //movement controls
        Vector3 movement = Vector3.zero;

        //get movement input & rotate player according to input
        float movementInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if(Input.GetAxis("Horizontal") < 0)
        {
            parent.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
            
        } else if(Input.GetAxis("Horizontal") > 0)
        {
            parent.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        }

        //set animations
        if (movementInput == 0)
        {
            ani.SetBool("isIdle", true);
        }
        else
        {
            ani.SetBool("isIdle", false);
        }
        if(GroundCheck())
        {
            ani.SetBool("isJumping", false);
        } else
        {
            ani.SetBool("isJumping", true);
        }

        //move players transform
        movement += (Vector3.right * movementInput);

        //player jump && grounding behavior

        if (charController.isGrounded) //if grounded reset jumps
        {
            hasJumped = false;
            isGrounded = true;
        } else
        {
            isGrounded = false; //if not grounded still allow player to jump
        }

        if(isGrounded) //if grounded dont let gravity do its thing
        {
            verticalSpeed = 0f;
        }
        if ((hasJumped == false)) //if player hasnt jumped, let them if they have coyote time
        {
            
            if (Input.GetKeyDown(KeyCode.Space) && curCoyote > 0)
            {
                verticalSpeed = jumpSpeed;
                hasJumped = true;

            }
        }
        
        //apply movement
        verticalSpeed += (gravity * Time.deltaTime);
        movement += (transform.up * verticalSpeed * Time.deltaTime);

        charController.Move(movement);
    }

    //timer for coyote time
    private void CoyoteTimer()
    {
        if(curCoyote > 0)
        {
            curCoyote -= .1f;
        } 
        
    }
    private void FixedUpdate() //plays all timers at a regular pace
    {
        lightTimer();
        if(isSeen)
        {
            seenTimer();
        }
        IsSeenCheck();
        if(charController.isGrounded == false)
        {
            CoyoteTimer();
        } else
        {
            curCoyote = maxCoyote;
        }
    }

    private bool GroundCheck()
    {
        //checks to see if player is grounded. Needed for jumping animation.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up * 2f, out hit))
        {
            return true;
        } else
        {
            return false;
        }




    }
    private void checkEnemies() {
        //not currently in use
        //find all enemies in scene
        //GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy");


        //foreach (GameObject g in EnemyList)
        //{
        //    //Debug.Log(Vector3.Distance(g.transform.position, gameObject.transform.position));
        //}

    }

    public void setInLight(Vector3 lightPos) //show if player is in the light
    {
        //calculates distance from the position the light hits the floor and player
        float dist = Vector3.Distance(transform.position, lightPos);
        if (dist < maxLightDetectionNumber)
        {
            Debug.Log(inLight);
            inLight = true;
            inLightTimerCur = inLightTimerMax;
        }
    }

    public void setIsSeen() //makes the player visable if they are in light
    {
        if(inLight) {
            isSeen = true;
            seenTimerCur = seenTimerMax;
        }
    }
    
    private void seenTimer() //timer so player can become unseen
    {
        seenTimerCur -= .1f;
        if(seenTimerCur <= 0)
        {
            isSeen = false;
        }
    }

    private void lightTimer() //timer so player can become unlit
    {
        if(inLight)
        {
            inLightTimerCur -= .1f;
            if(inLightTimerCur <= 0)
            {
                inLight = false;
            }
        }
    }

    private void IsSeenCheck() //checks to see if player is seen
    {
        if (isSeen)
        {
            playerMaterial.color = Color.red;
            isAlive = false;
            //Debug.Log("is seen");
        }
        else
        {
            playerMaterial.color = Color.white;
            //Debug.Log("is Hidden");
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Laser")
        {
            isAlive = false;
            playerMaterial.color = Color.red;
            Debug.Log("hit laser");
        } else if(col.tag == "WinItem")
        {
            col.gameObject.GetComponent<Intel>().SetNextLevel();
        } else if(col.tag == "Enemy")
        {
            isAlive = false;
        }
    }
}
