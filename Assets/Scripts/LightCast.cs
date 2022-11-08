using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCast : MonoBehaviour
{
    public Transform lightPos;
    public Transform playerPos;
    public Player player;

    
    void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        //send hit info on light to player
        RaycastHit hit;
        Debug.DrawRay(lightPos.position, lightPos.forward * 100);
        if (Physics.Raycast(lightPos.position, lightPos.forward * 100, out hit))
        {
            player.setInLight(hit.point);

    
        }
    }
}
