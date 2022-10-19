using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{ 
    public CharacterController charController;
    public float gravity = -9.8f;
    public float jumpSpeed;
    public float verticalSpeed;
    public float moveSpeed;



    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
       
    }

    // Update is called once per frame
    private void Update()
    {


        //first person movement controls
        Vector3 movement = Vector3.zero;

        
        float movementInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        movement += (transform.right * movementInput);

        if (charController.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpSpeed;
            }
        }

        verticalSpeed += (gravity * Time.deltaTime);
        movement += (transform.up * verticalSpeed * Time.deltaTime);

        charController.Move(movement);

       
        if (Input.GetMouseButtonDown(0))
        {
           
        }

    }

}
