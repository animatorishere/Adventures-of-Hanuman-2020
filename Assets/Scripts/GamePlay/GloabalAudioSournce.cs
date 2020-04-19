using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloabalAudioSournce : MonoBehaviour
{
    public static GloabalAudioSournce instance;

    public AudioSource sm;

    public AudioClip run, attack, ComboAttack, jumpAttack,jump,hanumanDamage, mashroomDamage, zombieDamage, spiderDamage;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

  
}
