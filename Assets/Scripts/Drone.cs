using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    //references
    public Player player;
    public Material enemyMaterial;

    //behavior
    public bool isRed;
    public bool canSwapColor;
    public int xAxisDirection;
    public float facingDirTimerCur;
    public float facingDirTimerMax;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
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
        MoveSelf();
    }
    private void CheckPlayer()
    {
        //check to see if flashlight hits the player
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up * 5);
        if (Physics.Raycast(transform.position, -transform.up, out hit, 5f))
        {
            if (hit.collider.gameObject.GetComponent<Player>() != null)
            {
                //make player seen if hit by drone ray
                player.setIsSeen();
            }
        }
    }
    private void SwapMaterialColor()
    {
        //turn drone red for player feedback
        if (isRed)
        {
            isRed = false;
            enemyMaterial.color = Color.white;

        }
        else
        {
            isRed = true;
            enemyMaterial.color = Color.red;
        }
    }

    private void FacingDirTimer()
    {
        //timer to change enemy facing direction
        facingDirTimerCur -= .01f;
    
        if (facingDirTimerCur <= 1 && canSwapColor)
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
        if (xAxisDirection == -1)
        {
            transform.rotation = Quaternion.Euler(90, 180, -90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(90, 0, -90);
        }
        
    }
    private void MoveSelf()
    {
        transform.position = startPosition + new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f);
    }
}
