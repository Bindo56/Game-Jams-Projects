using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] RaycastHit2D Ground;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] bool isWallDetected;
    [SerializeField] bool isleftwalldetected;
    [SerializeField] float raylenght;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool isPlayerDetected;
    bool playerdestroy;


    [Header("Flip")]
    public int facingDir = 1;
    public bool facingRight = true;


    public System.Action OnFlipped;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
       // attack();
        
    }

    private void Movement() //enemy movement
    {
        Collision();

        if (isGrounded)
        {
            rb.velocity = new Vector2(7 * facingDir, rb.velocity.y);

        }

        if (isWallDetected || !isGrounded || isleftwalldetected)
        {
            rb.velocity = Vector3.zero;
            //rb.velocity = new Vector2(-7 * facingDir, rb.velocity.y);
            flip();

        }


    }

    private void Collision()
    {
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right, 2, whatIsGround);
        isleftwalldetected = Physics2D.Raycast(transform.position, Vector2.left, 2, whatIsGround);
        isGrounded = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, raylenght, whatIsGround);
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, 5, PlayerLayer);
      

    }

    public virtual void flip() //flip if wall detected
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        rb.velocity = new Vector2(7 * facingDir, rb.velocity.y);
        if (OnFlipped != null)
            OnFlipped();
    }
    public virtual void flipController(float _x)
    {
        if (_x > 0 && !facingRight)
            flip();

        else if (_x < 0 && facingRight)
            flip();

    }

   

}
   



