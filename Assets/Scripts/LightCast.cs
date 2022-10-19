using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCast : MonoBehaviour
{
    public Transform lightPos;
    public Transform playerPos;
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
        Debug.DrawRay(lightPos.position, lightPos.forward * 100);
        if (Physics.Raycast(lightPos.position, lightPos.forward * 100, out hit))
        {
            //Debug.Log(hit.point);
            player.setInLight(hit.point);

    
        }
    }
}
