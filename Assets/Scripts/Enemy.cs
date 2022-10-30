using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Transform flashlightTransform;
    public Material enemyMaterial;
    public bool isRed;
    public bool canSwapColor;
    public int xAxisDirection;
    public float facingDirTimerCur;
    public float facingDirTimerMax;


    // Start is called before the first frame update
    void Start()
    {
        xAxisDirection = 1;
        isRed = false;
        canSwapColor = true;
        enemyMaterial.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer(); 
    }
    private void FixedUpdate()
    {
        FacingDirTimer();
    }
    private void CheckPlayer()
    {
        //check to see if flashlight hits the player
        RaycastHit hit;
        Debug.DrawRay(flashlightTransform.position, transform.right * 3);
        if (Physics.Raycast(flashlightTransform.position, transform.right, out hit, 3.5f))
        {
            if(hit.collider.gameObject.GetComponent<Player>() != null)
            {
                //Debug.Log(hit.point);
                player.setIsSeen();
                //Debug.Log("Player Seen");
            }
        }
    }
    private void SwapMaterialColor()
    {
        if(isRed)
        {
            isRed = false ;
            enemyMaterial.color = Color.white;
            
        } else
        {
            isRed = true;
            enemyMaterial.color = Color.red;
        }
    }

    private void FacingDirTimer()
    {
        //timer to change enemy facing direction
        facingDirTimerCur -= .01f;
        //Debug.Log(facingDirTimerCur);
        if(facingDirTimerCur <= 1 && canSwapColor)
        {
            SwapMaterialColor();
            canSwapColor = false;
        }
        if (facingDirTimerCur <= 0)
        {
            SwapMaterialColor();
            canSwapColor = true;
            facingDirTimerCur = facingDirTimerMax;
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        //changes the facing direction of enemy
        xAxisDirection *= -1;
        if(xAxisDirection == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
