using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resumenote : MonoBehaviour
{
  [SerializeField]  TextMeshProUGUI resume;
    PlayerMove Player;
  [SerializeField]  Collider2D cd;
    // Start is called before the first frame update
    void Start()
    {
        resume.enabled = false;
        cd = GetComponent<Collider2D>();
        Player = GetComponent<PlayerMove>();

        Player = FindObjectOfType<PlayerMove>();

       // cd = GetComponent<Collider2D>();

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
              //  if (Player.characterselect == 1)
                {
                    Debug.Log("error");
                    resume.enabled = true;
                    SceneManager.LoadScene(3);
                }
              //  else if (Player.characterselect == 2)
                {
              //      SceneManager.LoadScene(3);
                  ////  resume.enabled = true;
                   // Debug.Log("SwitchToBox");
                }

            }
        }
    }
}
