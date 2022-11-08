using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Transform Target;



    void LateUpdate()
    {
        //follow player
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }

   
 
}
