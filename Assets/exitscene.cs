using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitscene : MonoBehaviour
{
    public Collider2D cd;
    PlayerMove Player;
 
    // Start is called before the first frame update
    void Start()
    {
       
        Player = FindObjectOfType<PlayerMove>();

        cd = GetComponent<Collider2D>();

        if (Player != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.GetComponent<PlayerMove>() != null)
            {
                if (Player.characterselect == 1)
                {
                    Application.Quit();
                }

            }
        }
    }
}
