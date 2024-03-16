using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColliderTextPopup : MonoBehaviour
{
    public Collider2D cd;
    PlayerMove Player;
   [SerializeField] TextMeshProUGUI Text;

    // Start is called before the first frame update
    private void Awake()
    {
        Text.enabled = false;
    }
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
                if (Player.characterselect == 2)
                {
                    Debug.Log("Sqaure");
                    Text.enabled = true;
                }
                else if(Player.characterselect == 1)
                {
                    Text.enabled= false;
                }

            }

        }
    }
   
}
