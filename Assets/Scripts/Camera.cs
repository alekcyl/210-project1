using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Target;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(Target.position.x, Target.position.y, transform.position.z), Time.deltaTime * 10);
    }
}
