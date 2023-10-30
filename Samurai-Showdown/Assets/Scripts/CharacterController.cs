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
        canMove = false;
        Vector2 direccionGolpe;

        if(rigidBody.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1,0);
        }else{
            direccionGolpe = new Vector2(1,0);
        }
        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);
        StartCoroutine(WaitAndMove());
        Debug.Log("Golpe");
    }
    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(0.1f);

        while(!EstaEnSuelo()){
            yield return null;
        }

        canMove = true;
    }
}
