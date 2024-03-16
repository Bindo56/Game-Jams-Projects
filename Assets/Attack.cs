using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            Debug.Log("Destroyed");
            Destroy(collision.gameObject,0.5f); //damage player
        }

    }
}
