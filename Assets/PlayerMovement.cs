/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float inputx;
    [SerializeField] float moveSpeed;
    float inputy;

  [SerializeField]  PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputx = Input.GetAxisRaw("Horizontal");
        inputy = Input.GetAxisRaw("Vertical");
        wallmovement();
        movement();

       
    }
    private void wallmovement()
    {
        if (player.isWallDetected())
        {
            Debug.Log("wall");
            rb.velocity = new Vector2(rb.velocity.x, inputy * moveSpeed);
        }

        *//* else
             rb.velocity = new Vector2(0, 0);*//*
        if (player.isleftWallDetected())
        {
            Debug.Log("leftwall");
            rb.velocity = new Vector2(rb.velocity.x, inputy * moveSpeed);

        }
    }

    private void movement()
    {
        if (player.isGroundDetected())
        {
            Debug.Log("Grounded");
            rb.velocity = new Vector2(moveSpeed * inputx, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);

            }
        }





        if (player.iscellingDetected())
        {
            rb.velocity = new Vector2(rb.velocity.x, inputy * moveSpeed);
            Debug.Log("celing");
            rb.velocity = new Vector2(moveSpeed * inputx, rb.velocity.y);

        }
        *//* else
             rb.velocity = new Vector2 (0, 0);*//*

    }
}
*/