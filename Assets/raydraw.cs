using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class raydraw : MonoBehaviour
{
    public int score;
    //public raycastHit2
    [SerializeField] TextMeshProUGUI scoretext;
    public LineRenderer LineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer.SetPosition(0, transform.position);

        RaycastHit2D raycastHit2 = Physics2D.Raycast(transform.position, transform.up, 100);
        Debug.DrawRay(transform.position, transform.up * 10, Color.red);
        if (raycastHit2.collider != null)
        {
            LineRenderer.SetPosition(1, raycastHit2.point);

            if (raycastHit2.collider.CompareTag("Player"))
            {
                Destroy(raycastHit2.transform.gameObject);
                Debug.Log(score++);
                Debug.Log("Player");
                SceneManager.LoadScene(2);

               // scoretext.text = "Score" + score;
            }
            else
            {


            }



        }
       

       


    }
}
