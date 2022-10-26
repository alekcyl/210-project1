using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Transform Target;



    void LateUpdate()
    {
        //follow player
        //transform.position = Vector3.Lerp(transform.position,
            //new Vector3(Target.position.x, Target.position.y, transform.position.z), Time.deltaTime * 10);
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }

   
 
}
