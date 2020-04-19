using UnityEngine;
using System.Collections.Generic;

// Starting in 2 seconds.
// a projectile will be launched every 0.3 seconds

public class ExampleClass : MonoBehaviour
{
    public Transform prefab;

    void Start()
    {
        InvokeRepeating("LaunchProjectile", 0.001f, 20.0f);
    }

    void LaunchProjectile()
    {
        //Debug.Log("happy");
        Instantiate(prefab, transform.position, transform.rotation);
    }
}