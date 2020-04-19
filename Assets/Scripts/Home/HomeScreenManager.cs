using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnClickPlay() {
        Invoke("Play", 0f);       
    }
    public void Play()
    {
        SceneManager.LoadScene("GamePlay_Final");
    }
     void OnClickQuit()
    {
        Invoke("Quit", 0.5f);
    }
     void Quit()
    {
        Application.Quit();

    }



}
