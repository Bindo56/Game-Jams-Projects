using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public float Intensity = 1f;
    public float Mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;
    Mesh ogMesh, cloneMesh;
    MeshRenderer renderer;

    JellyVertex[] jv;
    Vector3[]  vertexArray;

    // Start is called before the first frame update
    void Start()
    {
        ogMesh = GetComponent<MeshFilter>().sharedMesh;
        cloneMesh = Instantiate(ogMesh);
        GetComponent<MeshFilter>().sharedMesh = cloneMesh;
        renderer = GetComponent<MeshRenderer>();

        jv = new JellyVertex[cloneMesh.vertices.Length];
        for (int i = 0; i < cloneMesh.vertices.Length; i++)
            jv[i] = new JellyVertex(i, transform.TransformPoint(cloneMesh.vertices[i]));
        
        


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertexArray = ogMesh.vertices;
        for (int i = 0;i < jv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[jv[i].ID]);

            float intensity = (1 - (renderer.bounds.max.y - target.y) / renderer.bounds.size.y) * Intensity;
            jv[i].Shake(target, Mass, stiffness, damping);
            target = transform.InverseTransformPoint(jv[i].Position);
            vertexArray[jv[i].ID] = Vector3.Lerp(vertexArray[jv[i].ID], target, intensity);
            
        }

        cloneMesh.vertices = vertexArray;

    }

    public class JellyVertex
    {
        public int ID;
        public Vector3 Position;
        public Vector3 velocity, Force;


     public JellyVertex(int _id,Vector3 _pos)
        {
            ID = _id;
            Position = _pos;
        }
    public void Shake(Vector3 target , float _m , float _s , float _d)
    {
            Force = (target - Position) * _s;
            velocity = (velocity + Force / _m) * _d;
            Position += velocity;
            if((velocity +  Force + Force / _m).magnitude < 0.001f)
            {
                Position = target;
            }


    }

    }




}
