using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    }

    // Update is called once per frame
    private void Update()
    {
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
        else
        {

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

        //player jump
        if (charController.isGrounded)
        {
            
            verticalSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpSpeed;
            }
            
        } else
        {
           
        }

        verticalSpeed += (gravity * Time.deltaTime);
        movement += (transform.up * verticalSpeed * Time.deltaTime);

        charController.Move(movement);
    }
    private void FixedUpdate()
    {
        lightTimer();
        if(isSeen)
        {
            seenTimer();
        }
        IsSeenCheck();
    }

    private bool GroundCheck()
    {
        //checks to see if player is grounded. Needed for jumping animation.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up * .5f, out hit))
        {
            return true;
        } else
        {
            return false;
        }




    }
    private void checkEnemies() {
        //find all enemies in scene
        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy");


        //foreach (GameObject g in EnemyList)
        //{
        //    //Debug.Log(Vector3.Distance(g.transform.position, gameObject.transform.position));
        //}

    }

    public void setInLight(Vector3 lightPos)
    {
        //set
        float dist = Vector3.Distance(transform.position, lightPos);
        //Debug.Log("position" + transform.position);
        if (dist < maxLightDetectionNumber)
        {
            inLight = true;
            inLightTimerCur = inLightTimerMax;
        }
    }

    public void setIsSeen()
    {
        if(inLight) {
            isSeen = true;
            seenTimerCur = seenTimerMax;
            Debug.Log("Player Seen 123");
        }
    }

    private void seenTimer()
    {
        seenTimerCur -= .1f;
        //Debug.Log(seenTimerCur);
        if(seenTimerCur <= 0)
        {
            isSeen = false;
        }
    }



    private void lightTimer()
    {
        if(inLight)
        {
            inLightTimerCur -= .1f;
            //Debug.Log(inLightTimerCur);
            if(inLightTimerCur <= 0)
            {
                inLight = false;
            }
        }
    }

    private void IsSeenCheck()
    {
        if (isSeen)
        {
            playerMaterial.color = Color.red;
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
            //Debug.Log("hit laser");
        }
    }
}
