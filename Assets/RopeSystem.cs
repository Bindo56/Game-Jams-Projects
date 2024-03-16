using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class RopeSystem : MonoBehaviour
{
    public GameObject RopeHinge;
    public DistanceJoint2D DJ;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public PlayerMovement playerMovement;
    bool ropeAttached;
    bool distanceSet;
    [SerializeField] Vector2 playerPosition;
  //  [SerializeField] Transform hook;
  [SerializeField]  Rigidbody2D ropeHingeRB;
    SpriteRenderer ropeHingeSR;

    [Header("RopeClimb")]
    public float climbSpeed;
    bool isColliding;

    [Header("Rope")]
    public LineRenderer RopeRnder;
    public LayerMask ropeLayer;
    [SerializeField] float ropeDistance = 20f;
    List<Vector2> ropePosVector = new List<Vector2>(); 

    Dictionary<Vector2 , int > wrapPoints = new Dictionary<Vector2 , int>();





    private void Awake()
    {
        DJ.enabled = false;
        playerPosition = transform.position; //
        
        ropeHingeRB = RopeHinge.GetComponent<Rigidbody2D>();
        ropeHingeSR = RopeHinge.GetComponent<SpriteRenderer>();
        crosshairSprite.enabled = true;


    }

    private void Update()
    {
        

        var worldMousePos =  Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x , Input.mousePosition.y , 0)); 

        var facingDir = worldMousePos - transform.position;
        var aimAngle = Mathf.Atan2(facingDir.y, facingDir.x);

        if(aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;

        }

        var aimDir = Quaternion.Euler(0,0,aimAngle * Mathf.Rad2Deg) * Vector2.right;

        playerPosition = transform.position; //

        if (!ropeAttached)
        {
            SetAimPos(aimAngle);
            playerMovement.isSwinging = false;
            
        }
        else
        {
            crosshairSprite.enabled = false;

            playerMovement.isSwinging = true;
            playerMovement.ropeHook = ropePosVector.Last();
            /* if(!crosshairSprite.enabled)
             {
                 return;
             }*/

            if (ropePosVector.Count > 0)
            {
                var lastRopePoint = ropePosVector.Last();
                var PlayerToNxtHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, ropeLayer);

                if (PlayerToNxtHit)
                {
                    var colliderwithVertices = PlayerToNxtHit.collider as PolygonCollider2D;

                    if(colliderwithVertices !=null)
                    {
                        var closestPointToHit = RopeWarpToCollider(PlayerToNxtHit, colliderwithVertices);

                        if (wrapPoints.ContainsKey(closestPointToHit))
                        {
                            ResetRope();
                            return;
                        }

                        ropePosVector.Add(closestPointToHit);
                        wrapPoints.Add(closestPointToHit, 0);
                        distanceSet = false;
                    }

                }
            }

        }

        HandleInput(aimDir);
        UpdateRopePos();
        HandleRopeLenght();



    }

    private void SetAimPos(float aimAngle)  // For Aiming the cursor to where the tenticles should lunch
    {
        if (!crosshairSprite.enabled)
        {
           crosshairSprite.enabled = true;
           // return;
        }

        var x = transform.position.x + 2f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 2f * Mathf.Sin(aimAngle);


        var CrossHairPos = new Vector3(x,y,0);

        crosshair.transform.position = CrossHairPos;

    }

    private void HandleInput(Vector2 aimDir) //detecting the aim and lunching at selected layer where to aatach
    {
       /* if(Input.GetKeyDown(KeyCode.Alpha1))
        {

            crosshairSprite.enabled = true;

        }*/
       // if(Input.GetKey(KeyCode.Alpha1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ropeAttached) //remove crossaim
                    return;

                RopeRnder.enabled = true;

                var hit = Physics2D.Raycast(playerPosition, aimDir, ropeDistance, ropeLayer);

                if (hit.collider != null)
                {
                    ropeAttached = true;

                    if (!ropePosVector.Contains(hit.point)) //custom hit target
                    {
                        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0), ForceMode2D.Impulse);

                        ropePosVector.Add(hit.point);
                        DJ.distance = Vector2.Distance(playerPosition, hit.point);

                        DJ.enabled = true;
                        ropeHingeSR.enabled = true;

                    }
                }
                else
                {
                    RopeRnder.enabled = false;
                    ropeAttached = false;
                    DJ.enabled = false;
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                ResetRope();
            }

        }
       



    }

    private void ResetRope() //reset tenticles
    {

        DJ.enabled = false;
        ropeAttached = false;
        playerMovement.isSwinging = false;
        RopeRnder.positionCount = 2;
        RopeRnder.SetPosition(0,transform.position);
        RopeRnder.SetPosition(1,transform.position);
        ropePosVector.Clear();
        ropeHingeSR.enabled=false;
        wrapPoints.Clear();

    }

    private void UpdateRopePos() //if tenticles collide 
    {
        if(!ropeAttached) { return; }

        RopeRnder.positionCount = ropePosVector.Count + 1;

        for (var i = RopeRnder.positionCount - 1; i >= 0; i--)
        {
            if(i != RopeRnder.positionCount - 1)
            {
                RopeRnder.SetPosition(i, ropePosVector[i]);

                if(i == ropePosVector.Count - 1 || ropePosVector.Count == 1)
                {
                    var ropepos = ropePosVector[ropePosVector.Count - 1];

                    if(ropePosVector.Count == 1)
                    {
                        ropeHingeRB.transform.position = ropepos;

                        if (!distanceSet)
                        {
                            DJ.distance = Vector2.Distance(transform.position, ropepos); //chick ropepos for error
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeRB.transform.position = ropepos;

                        if (!distanceSet)
                        {
                            DJ.distance = Vector2.Distance(transform.position, ropepos);
                            distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == ropePosVector.IndexOf(ropePosVector.Last()))
                {
                    var ropePos = ropePosVector.Last();
                    ropeHingeRB.transform.position = ropePos;

                    if (!distanceSet)
                    {
                        DJ.distance = Vector2.Distance(transform.position, ropePos); distanceSet = true;
                    }

                }
            }
            else
            {
                RopeRnder.SetPosition(i, transform.position);
            }



        }
       
    }

    private Vector2 RopeWarpToCollider(RaycastHit2D hit , PolygonCollider2D polygonCollider)
    {

        var distanceDictionary =  polygonCollider.points.ToDictionary<Vector2,float,Vector2>(Position => Vector2.Distance(hit.point,polygonCollider.transform.TransformPoint(Position)),Position => polygonCollider.transform.TransformPoint(Position));  


        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);

        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;    

    }

    private void HandleRopeLenght()
    {
        if(Input.GetAxis("Vertical") >= 1f && ropeAttached && !isColliding)
        {
            DJ.distance -= Time.deltaTime * climbSpeed;

        }
        else if (Input.GetAxis("Vertical") < 0f && ropeAttached)
        {
            DJ.distance += Time.deltaTime * climbSpeed;
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }






}
