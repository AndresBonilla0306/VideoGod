using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator anim;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool attacking;
    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject Hit;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        Comportamientos();
    }
    public void Comportamientos()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !attacking)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0,2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    anim.SetBool("Walk", false);
                    break;
                case 1:
                    direccion = Random.Range(0,2);
                    rutina++;
                    break;
                case 2:
                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(0,180,0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                    }
                    anim.SetBool("Walk", true);
                    break;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !attacking)
            {
                if(transform.position.x < target.transform.position.x)
                {
                    anim.SetBool("Walk", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    anim.SetBool("attack", false);
                }
                else
                {
                    anim.SetBool("Walk", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    anim.SetBool("attack", false);
                }
            }
            else
            {
                if(!attacking)
                {
                    if(transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0,0,0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0,180,0);
                    }
                    anim.SetBool("Walk", false);
                }
            }
        }
    }
    public void Final_anim()
    {
        anim.SetBool("attack", false);
        attacking = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }
}