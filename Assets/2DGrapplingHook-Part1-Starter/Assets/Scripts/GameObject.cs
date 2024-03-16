
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float jumpSpeed = 3f;
    public bool groundCheck;
    public bool isSwinging;
    private SpriteRenderer playerSprite;
  [SerializeField]  private Rigidbody2D rBody;
    private bool isJumping;
  //  private Animator animator;
    private float jumpInput;
    private float horizontalInput;

    public Vector2 ropeHook;
    public float swingForce = 4f;

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
       // animator = GetComponent<Animator>();
    }
     
    void Update() //movement
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 5f);
       // groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 5f);
    }

    void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f) //for climbling teticles
        {
           // animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput < 0f;

            if(isSwinging)
            {
                var playerToHookDir = (ropeHook - (Vector2)transform.position).normalized;

                Vector2 ppdDir; //perpendicular direction
                if(horizontalInput < 0)
                {
                    ppdDir = new Vector2(playerToHookDir.y, playerToHookDir.x);
                    var leftperppos = (Vector2)transform.position - ppdDir * -2f;
                    Debug.DrawLine(transform.position, leftperppos,Color.green,0f);

                }
                else
                {
                    ppdDir = new Vector2(playerToHookDir.y, -playerToHookDir.x);
                    var rightperpPos = (Vector2)transform.position + ppdDir * 2f;
                    Debug.DrawLine(transform.position,rightperpPos,Color.green,0f);
                }


                var force = ppdDir * swingForce;
                rBody.AddForce(force, ForceMode2D.Force);

            }
            if(!isSwinging)
            {

                if(!groundCheck)
                {
                    return;
                }

                isJumping = jumpInput > 0f;
                if(isJumping)
                {
                    rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
                }

            }




            if (groundCheck)
            {
                var groundForce = speed * 2f;
                rBody.AddForce(new Vector2((horizontalInput * groundForce - rBody.velocity.x) * groundForce, 0));
                rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
            }
        }
        else
        {
           // animator.SetFloat("Speed", 0f);
        }

        if (!groundCheck) return;

        isJumping = jumpInput > 0f;
        if (isJumping)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
        }
    }
}
