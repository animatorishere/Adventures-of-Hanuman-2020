using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitCounter : MonoBehaviour
{
    public static EnemyHitCounter instance;
    public int hitCounter, destroyCounter;
    public GameObject hitParticle, destroyParticle;
    public float destroyTime;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

}
