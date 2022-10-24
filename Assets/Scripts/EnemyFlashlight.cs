using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashlight : MonoBehaviour
{
    public Transform enemyTransform;
    public float maxRange;
    public bool isUp;
    public float moveDist;

    // Start is called before the first frame update
    void Start()
    {
        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        MoveLight();
    }

    private void MoveLight()
    {
        //move location of light up and down
        if (transform.localPosition.y < maxRange && isUp)
        {
            transform.position += new Vector3(0, moveDist, 0);
        }
        else
        {
            isUp = false;
            transform.position += new Vector3(0, -moveDist, 0);
            if(transform.localPosition.y < 0)
            {
                isUp = true;
            }
        }
           
    }

        
    
}
