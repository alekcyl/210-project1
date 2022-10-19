using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer(); 
    }

    private void CheckPlayer()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.right * 10);
        if (Physics.Raycast(transform.position, transform.right, out hit, 10))
        {
            if(hit.collider.gameObject.GetComponent<Player>() != null)
            {
                //Debug.Log(hit.point);
                player.setIsSeen();
                Debug.Log("Player Seen");
            }
        }
    }
}
