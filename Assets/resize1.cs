using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resize1 : MonoBehaviour
{
    public Collider2D cd;
    PlayerMove Player;
    [SerializeField] TextMeshProUGUI resizeText;
    // Start is called before the first frame update
    void Start()
    {
        resizeText.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
          //  if (Player.GetComponent<PlayerMove>() != null)
            {
               // if (Player.characterselect == 1)
                {
                    resizeText.enabled = true;

              //      Invoke("Off", 5f);

                }


            }
        }
    }


    private void Off()
    {
        resizeText.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        resizeText.enabled=false;
    }
}
