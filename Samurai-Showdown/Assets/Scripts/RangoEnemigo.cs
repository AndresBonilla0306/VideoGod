using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{
    public Animator anim;
    public EnemyMovement enemy;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            anim.SetBool("Walk", false);
            anim.SetBool("attack", true);
            enemy.attacking = true;
            GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Toque");
        }
    }
}
