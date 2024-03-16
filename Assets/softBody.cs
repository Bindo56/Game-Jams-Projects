using UnityEngine;
using UnityEngine.U2D;

public class softBody : MonoBehaviour
{
    const float splineOffset = 0.2f;


    [SerializeField] public SpriteShapeController spriteShape;
    [SerializeField] Transform[] points;


    private void Awake()
    {
        UpdateVertices();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVertices();
    }


    private void UpdateVertices()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 _vertex = points[i].localPosition;

            Vector2 _towardCenter = (Vector2.zero - _vertex).normalized;

            float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                spriteShape.spline.SetPosition(i, (_vertex - _towardCenter * _colliderRadius));
            }
            catch
            {
                Debug.Log("spline points are close ");
                spriteShape.spline.SetPosition(i, (_vertex - _towardCenter * (_colliderRadius + splineOffset)));
            }

            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);

            Vector2 _newRt = Vector2.Perpendicular(_towardCenter) * _lt.magnitude;
            Vector2 _newLt = Vector2.zero - (_newRt);

            spriteShape.spline.SetRightTangent(i, _newRt);
            spriteShape.spline.SetLeftTangent(i, _newLt);

        }
    }

}
