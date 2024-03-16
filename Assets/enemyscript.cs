using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    [SerializeField] BoxCollider2D bx;
    [SerializeField] GameObject slime;
    [SerializeField] GameObject enemy;
    [SerializeField] PlayerMovement SlimeMove;
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] Movement EnemyMove;
    [SerializeField] Attack Attack;
    [SerializeField] CircleCollider2D cc;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float timer;
    /* [SerializeField] GameObject Player;
     [SerializeField] GameObject Target;*/

    private void Start()
    {
       EnemyMove.enabled = false;
    }
    private void Update()
    {
       BackToNormal();
            
        //    timer -= Time.deltaTime;
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("PlayerDetected");

            playerswitch();

           
        }

    }

    private void playerswitch() //switcihing the player to enemy
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            timer -= Time.deltaTime;
            Debug.Log("Switch");
            SlimeMove.enabled = false;
            EnemyMove.enabled = true;
            cc.enabled = false;
            Attack.enabled = false;
            sr.color = Color.clear;
            

            StartCoroutine(switchback());

        }
       
    }

    private void BackToNormal()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            SlimeMove.enabled = true;
            EnemyMove.enabled = false;
            
            sr.color = Color.white;
            slime.transform.position = enemy.transform.position;


            Destroy(enemy, 1F);

        }
        
    }

    private void switchbackNor()
    {
        SlimeMove.enabled = true;
        EnemyMove.enabled = false;

        sr.color = Color.white;
        slime.transform.position = enemy.transform.position;


        Destroy(enemy, 1F);
    }

    IEnumerator switchback()
    {
        yield return new WaitForSeconds(10);
        switchbackNor();

    }

  /*  private void PlayerPrefab()
    {
       Vector3 spawn = Target.transform.position;

       GameObject slime = Instantiate(Player,spawn,Quaternion.identity);

        slime.GetComponent<Movement>();

       

    }*/
}
