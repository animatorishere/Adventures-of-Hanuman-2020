using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRunAudioSource : MonoBehaviour
{

    public static HRunAudioSource instance;

    public AudioSource sm;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }


}
