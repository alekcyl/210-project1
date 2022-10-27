using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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
