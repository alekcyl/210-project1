using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Target;
    public float maxLightRange;
    public GameObject[] lightList;

    private void Start()
    {
        lightList = GameObject.FindGameObjectsWithTag("Light");
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(Target.position.x, Target.position.y, transform.position.z), Time.deltaTime * 10);
    }

    private void Update()
    {
        LightRenderer();
    }
    public void LightRenderer()
    {
        
        foreach(GameObject g in lightList)
        {
            //Debug.Log(g);
            float dist = Vector3.Distance(Target.transform.position, g.transform.position);
            if(dist > maxLightRange)
            {
                g.SetActive(false);
            } else if(g.activeSelf == false)
            {
                g.SetActive(true);
            } else
            {

            }
            

        }
    }
}
