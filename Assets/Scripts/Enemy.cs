using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Transform flashlightTransform;
    public int xAxisDirection;
    public float facingDirTimerCur;
    public float facingDirTimerMax;

    // Start is called before the first frame update
    void Start()
    {
        xAxisDirection = 1;
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

    private void FacingDirTimer()
    {
        facingDirTimerCur -= .01f;
        //Debug.Log(facingDirTimerCur);
        if (facingDirTimerCur <= 0)
        {
            facingDirTimerCur = facingDirTimerMax;
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
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
