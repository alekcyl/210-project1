using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 startPosition;

    //simple script to make platform rotate with a sine wave
    void Start()
    {
        startPosition = transform.position;
    }
    private void FixedUpdate()
    {
        MoveSelf();
    }

    private void MoveSelf()
    {
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time), 0.0f, 0.0f);
    }
}
