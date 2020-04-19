using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHomeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickHome()
    {
        SceneManager.LoadScene("Home");

    }

}
