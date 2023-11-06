using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
   public float velocidad;
   public float fuerzaSalto;
   public LayerMask capaSuelo;
   private Rigidbody2D rigidBody;
   private BoxCollider2D boxCollider;
   private bool lookright = true;
   private Animator animator;
   public float fuerzaGolpe;
   private bool canMove = true;
   private Vector2 lastMovementDirection;
   public float fuerzaEmpuje;
   public GameObject toActivate;
   public GameObject ToActivate2;
   public GameObject Cartel;
   public AudioClip SonidoDamage;
   public AudioClip BossFight;
   public AudioClip BossSpawn;
   public AudioClip SonidoAmbiente;

    private void Start() 
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        ProcesarMovimiento();
        saltito(); 
        //Ataque();
    }
    void ProcesarMovimiento()
    {
        if(!canMove) return;
        //logica mamalona de movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");

        if(inputMovimiento != 0f)
        {
            animator.SetBool("isRunning", true);
            lastMovementDirection = new Vector2(inputMovimiento, 0f);
        }
        else{
            animator.SetBool("isRunning", false);
        }
        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);

        voletado(inputMovimiento);
    }
    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null; 
    }
    void saltito()
    {
        if(Input.GetKeyDown(KeyCode.Space) && EstaEnSuelo())
        {
            rigidBody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            animator.SetBool("IsJump", true);
        }
        else
        {
            animator.SetBool("IsJump", false);
        }
    }
    void voletado(float inputMovimiento)
    {
        //Va a voltear el sprite del jugador 
        if((lookright == true && inputMovimiento < 0) || (lookright == false && inputMovimiento > 0 ))
        {
            lookright = !lookright;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void AplicarGolpe()
    {
        AudioManager.Instance.PlaySound(SonidoDamage);
        canMove = false;
        Vector2 direccionGolpe = lastMovementDirection.normalized;

        if (direccionGolpe == Vector2.zero)
        {
            // Si no hay una dirección registrada, asumimos que el personaje estaba mirando hacia la derecha.
            direccionGolpe = new Vector2(1, 1);
        }
        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);
        StartCoroutine(WaitAndMove());
        Debug.Log("Golpe");
    }
    public void AplicarEmpuje(Vector2 direccion)
    {
        Vector2 fuerza = direccion.normalized * fuerzaEmpuje;

        rigidBody.AddForce(fuerza, ForceMode2D.Impulse);
    }
    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(0.1f);

        while(!EstaEnSuelo()){
            yield return null;
        }

        canMove = true;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Spawn"))
        {
            AudioManager.Instance.DetenerAudio();
            AudioManager.Instance.PlayInLoop(BossFight);
            AudioManager.Instance.PlaySound(BossSpawn);
            toActivate.SetActive(true);
            ToActivate2.SetActive(true);
            Cartel.SetActive(false);
            Debug.Log("Spawn");
        }
        if(coll.CompareTag("Sign"))
        {
            Cartel.SetActive(true);
        }
        if(coll.CompareTag("Start"))
        {
            AudioManager.Instance.PlayInLoop(SonidoAmbiente);
            Destroy(coll.gameObject);
        }
    }
    
}
