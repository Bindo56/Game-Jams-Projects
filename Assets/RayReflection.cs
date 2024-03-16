using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RayReflection : MonoBehaviour
{
    //  int Infty = 999;
    const int Infty = 999;

    public int score;
    [SerializeField] TextMeshProUGUI scoretext;

    int maxReflect = 100;
    int crtReflect = 0;

    [SerializeField]
   // GameObject player;
    Vector2 startPoint, Dir;
    Vector2 OgRay;
    // Vector2  Dir;
    
    List<Vector3> Points;
    int RayDistance = 100;
   [SerializeField] LineRenderer lr;
   // Transform startPoint;


    // Use this for initialization
    void Start()
    {
        Points = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
       // startPoint = transform.position + Vector3.right * 2;
            

    }

    private void Update()
    {
        OgRay = transform.position;
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var DDirection = mouse - this.transform.position;
        var ray = new Ray2D(OgRay, DDirection);

      //  Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // var DDirection = mouse - transform.position;

       // startPoint = this.transform.position;
        //Dir = transform.position + transform.right * defaultRayDistance; 
        startPoint = ray.origin;
        Dir = DDirection;

        var hit = Physics2D.Raycast(startPoint, (Dir - startPoint).normalized, RayDistance);

        crtReflect = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hit)
        {
            Reflection(startPoint, hit);
        }
        else
        {
            Points.Add(startPoint + (Dir - startPoint).normalized * Infty);
        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void Reflection(Vector2 origin, RaycastHit2D hitData)
    {
        if (crtReflect > maxReflect)
        {

            return;
        }
        Points.Add(hitData.point);

        crtReflect++;

        Vector2 Direction = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(Direction, hitData.normal);

        var newHit = Physics2D.Raycast(hitData.point + (newDirection), newDirection * 100, RayDistance);


        if (newHit)
        {
            if (newHit.collider.CompareTag("Player"))
            {
                Destroy(newHit.transform.gameObject);
                Debug.Log(score++);
                Debug.Log("Player");
                scoretext.text = "Score" + score;
            }
            Reflection(hitData.point, newHit);
        }
        else
        {
            Points.Add(hitData.point + newDirection * RayDistance);
        }
    }
}
