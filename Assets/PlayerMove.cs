using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float inputx;
    [SerializeField] float moveSpeed;
    float inputy;

    float resize = 1f;
    // float maxSize = 3f;
    bool isResizing;

    public GameObject Player1, Player2;
    public Collider2D Player1Col, Player2Col;
    public int characterselect;

    [SerializeField] int gravityScale;

    [Header("PrefabIns")]
    [SerializeField] GameObject sqprefab;
    GameObject squareprefab;
    [SerializeField] Transform spawnPoint;

    float cRotation;
    float Drotation = 90f;

    //  public Transform attackChecks;
    // public float attackCheckRadius;
    [SerializeField] protected Transform cellingCheck;
    [SerializeField] protected float cellingCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform leftwallCheck;
    [SerializeField] protected float leftwallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterselect = 2;
        Player2.SetActive(false);
        rb.gravityScale = gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        inputx = Input.GetAxisRaw("Horizontal");
        inputy = Input.GetAxisRaw("Vertical");
        movement();
        wallmovement();
        rotate();
        prefab();
        ChangePlayer();
        isWallDetected();
        isleftWallDetected();
        iscellingDetected();
        isGroundDetected();
        Playerresize();

    }

    private void Playerresize()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            isResizing = true;
            //ebug.Log("resizing");
            if (isResizing)
            {
                float maxSize = 3f;
                rb.gravityScale = 10f;

                transform.localScale += new Vector3(resize, resize, 0f) * Time.deltaTime;
                transform.localScale = Vector3.Min(transform.localScale, new Vector3(maxSize, maxSize, 0f));


            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            isResizing = false;
            transform.localScale -= new Vector3(resize, resize, 0f) * Time.deltaTime;
            rb.gravityScale = 1f;
            transform.localScale = Vector3.Max(transform.localScale, new Vector3(0.5f, 0.5f, 0f));

        }





    }



    private void wallmovement()
    {
        if (isWallDetected())
        {
            Debug.Log("wall");
            rb.velocity = new Vector2(rb.velocity.x, inputy * moveSpeed);
        }


        if (isleftWallDetected())
        {
            Debug.Log("leftwall");
            rb.velocity = new Vector2(rb.velocity.x, inputy * moveSpeed);

        }
    }

    public void movement()
    {
        if (isGroundDetected())
        {
            Debug.Log("Grounded");
            rb.velocity = new Vector2(moveSpeed * inputx, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);

            }
        }





        if (iscellingDetected())
        {
            rb.velocity = new Vector2(rb.velocity.x, inputy * moveSpeed);
            Debug.Log("celing");
            rb.velocity = new Vector2(moveSpeed * inputx, rb.velocity.y);

        }
        /**//* else
             rb.velocity = new Vector2 (0, 0);*//*
 */
    }
    private void rotate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Rotate");
            cRotation += Drotation;
            rb.rotation = cRotation;
        }



    }

    private void prefab()
    {
        if (Input.GetKeyDown(KeyCode.F) && characterselect == 2)
        {
            Debug.Log("square");
            squareprefab = Instantiate(sqprefab, spawnPoint);

            Invoke("DesroyPrefav", 50);
        }
    }

    private void DesroyPrefav()
    {
        Destroy(squareprefab);
    }

    public virtual bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool iscellingDetected() => Physics2D.Raycast(cellingCheck.position, Vector2.up, cellingCheckDistance, whatIsGround);

    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
    public virtual bool isleftWallDetected() => Physics2D.Raycast(leftwallCheck.position, -Vector2.left, leftwallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(cellingCheck.position, new Vector3(cellingCheck.position.x, cellingCheck.position.y + cellingCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(leftwallCheck.position, new Vector3(leftwallCheck.position.x + leftwallCheckDistance, leftwallCheck.position.y));
    }


    public void ChangePlayer()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (characterselect == 1)
            {

                characterselect = 2;
            }
            else if (characterselect == 2)
            {
                characterselect = 1;
            }


            if (characterselect == 1)
            {


                Player1.SetActive(true);
                Player2.SetActive(false);
                Player1Col.enabled = true;
                Player2Col.enabled = false;


            }
            else if (characterselect == 2)
            {
                Player1.SetActive(false);
                Player2.SetActive(true);
                Player1Col.enabled = false;
                Player2Col.enabled = true;

            }


        }
    }


}



