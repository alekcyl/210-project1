using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject parent;
    public Animator ani;
    public CharacterController charController;
    public float gravity = -9.8f;
    public float jumpSpeed;
    public float verticalSpeed;
    public float moveSpeed;

    public bool inLight;
    public float inLightTimerCur;
    public float inLightTimerMax;
    public float maxLightDetectionNumber;

    public bool isSeen;
    public float seenTimerCur;
    public float seenTimerMax;

    public Material playerMaterial;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        playerMaterial.color = Color.white;

    }

    // Update is called once per frame
    private void Update()
    {
        checkEnemies();
        //first person movement controls
        Vector3 movement = Vector3.zero;

        
        float movementInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if(Input.GetAxis("Horizontal") < 0)
        {
            //transform.Rotate(new Vector3(0f, 90f, 0f));
            parent.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
            
        } else if(Input.GetAxis("Horizontal") > 0)
        {
            //transform.Rotate(new Vector3(0f, -90f, 0f));
            parent.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        }
        else
        {

        }
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

        movement += (Vector3.right * movementInput);
        //movement += (transform.forward * movementInput);
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

       
        if (Input.GetMouseButtonDown(0))
        {
           
        }

    }
    private void FixedUpdate()
    {
        lightTimer();
        if(isSeen)
        {
            seenTimer();
        }
        //Debug.Log(inLight);
        IsSeenCheck();
    }

    private bool GroundCheck()
    {
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

        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy");


        //foreach (GameObject g in EnemyList)
        //{
        //    //Debug.Log(Vector3.Distance(g.transform.position, gameObject.transform.position));
        //}

    }

    public void setInLight(Vector3 lightPos)
    {
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
            Debug.Log("is Hidden");
        }
    }

    
}
