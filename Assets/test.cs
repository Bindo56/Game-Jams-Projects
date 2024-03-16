using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // transform.Translate(new Vector3(0, 5, 0));

        // Quaternion q = new Quaternion(0, 30, 0, 1); //* Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = new quaternion(0, 1, 0, 0);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);

       
        
    }
}
