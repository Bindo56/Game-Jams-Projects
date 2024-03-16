using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour 
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public Transform aim;
    public float shootForce; 


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Change "Fire1" to your input axis/button
        {
            Fire();
        }
    }

   private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = projectile.GetComponentInChildren<Rigidbody2D>();

        Vector2 shootDir = (aim.position - shootPoint.position).normalized;
        if (rb != null)
        {
          //  rb.AddForce(shootPoint.right * shootForce, ForceMode2D.Impulse);
           rb.velocity = shootDir * shootForce;
        }
        else
        {
            Debug.LogError("prefabnothere");
        }
    }
}
