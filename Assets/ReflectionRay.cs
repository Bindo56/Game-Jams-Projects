using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ReflectionRay : MonoBehaviour
{
    public LineRenderer Lr;

    public int score;
    [SerializeField] TextMeshProUGUI scoretext;

    List<Vector3> Points;
    Vector2 startPos, Dir;

    // Start is called before the first frame update
    void Start()
    {
        Lr = GetComponent<LineRenderer>();
        Points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.position;
        var mouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        var DDirection = mouse - this.transform.position;
       // var ray = new Ray2D(this.transform.position,  DDirection);
        var ray = new Ray2D(startPos,  DDirection);

        Points.Clear();
        Points.Add(startPos);

      

        Debug.DrawLine(ray.origin, mouse, Color.red, 0f);

        var hit = Physics2D.Raycast(ray.origin, ray.direction);

        if(hit)
        {

            // var relectRay = Quaternion.FromToRotation(-ray.direction, hit.normal);
            // var multiRelect = Quaternion.FromToRotation();

            //   var relectDir = relectRay * hit.normal;
            //  var multirelectDir = multiRelect * hit.normal;

            if (hit.collider.CompareTag("Player"))
            {
                Destroy(hit.transform.gameObject);
                Debug.Log(score++);
                Debug.Log("Player");
                scoretext.text = "Score" + score;
            }
            var Reflect = Vector2.Reflect(ray.direction, hit.normal);

            Points.Add(hit.point);
            Points.Add(hit.point + Reflect * 5);


            

            Debug.DrawRay(hit.point, Reflect, Color.red, 0f);
            //Debug.DrawRay(hit.point, multirelectDir, Color.red, 0f);

        }

       // else 
          //  Lr.positionCount = 0;



        Lr.positionCount = Points.Count;
        Lr.SetPositions(Points.ToArray());
    }
}
