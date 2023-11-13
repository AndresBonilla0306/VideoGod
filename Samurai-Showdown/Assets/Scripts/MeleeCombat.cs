using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    [SerializeField] private ParticleSystem particulas;
    public AudioClip SonidoEspada;
    public AudioClip Cortado;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update(){

        if(tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && tiempoSiguienteAtaque <= 0)
        {
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
            AudioManager.Instance.PlaySound(SonidoEspada);
        }
    }
    private void Golpe(){

        animator.SetTrigger("Golpe");

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioGolpe);

        foreach (Collider2D collisionador in objetos)
        {
            if(collisionador.CompareTag("Enemy")){
                collisionador.transform.GetComponent<Enemy>().TomarDaño(dañoGolpe);
                particulas.Play();
            }
            if(collisionador.CompareTag("Boss"))
            {
                collisionador.transform.GetComponent<Boss>().TomarDaño(dañoGolpe);
                particulas.Play();
            }
            if(collisionador.CompareTag("Tentacle"))
            {
                AudioManager.Instance.PlaySound(Cortado);
                Destroy(collisionador.gameObject);
                particulas.Play();
                GameManagerScript.Instance.SumarPoints(5);
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioGolpe);
    }
}
