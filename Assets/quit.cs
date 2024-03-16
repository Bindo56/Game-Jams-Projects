using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    public void exit()
    {
        if (Application.isPlaying) 
        Application.Quit();
    }
}
