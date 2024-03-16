using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float inputX;
    float inputY;
   [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(moveSpeed * inputX, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed * inputY);
    }
}
