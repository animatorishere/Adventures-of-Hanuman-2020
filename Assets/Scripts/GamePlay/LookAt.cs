using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour   
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//transform.LookAt(target.transform.position,Vector3.up);
        if (transform.position.x < target.transform.position.x)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        else
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
    }
}
