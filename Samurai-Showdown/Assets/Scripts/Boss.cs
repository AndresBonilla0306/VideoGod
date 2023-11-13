using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform Player;
    private bool LookRight = true;
    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] private BarraVida barraVida;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioataque;
    public AudioClip BossDamage;
    public AudioClip BossDie;
    public AudioClip SoundAtaque;
    //[SerializeField] private float dañoAtaque;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        barraVida.InicializarVida(vida);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        float distanciaPlayer = Vector2.Distance(transform.position, Player.position);
        animator.SetFloat("distanciaPlayer", distanciaPlayer);
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        AudioManager.Instance.PlaySound(BossDamage);
        barraVida.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            Muerte();
        }
    }
    public void Muerte()
    {
        animator.SetTrigger("Muelto");
        AudioManager.Instance.DetenerAudio();
        AudioManager.Instance.PlaySound(BossDie);
        StartCoroutine (DelayAndDie());
        //gameManager.SumarPoints(valor);
    }
    IEnumerator DelayAndDie() {
    yield return new WaitForSeconds(1);
    Destroy(this.gameObject);
    Menu.Instance.Win();
    }
    public void MirarJugador()
    {
        if((Player.position.x > transform.position.x && !LookRight) || (Player.position.x < transform.position.x && LookRight))
        {
            LookRight = !LookRight;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180,0);
        }
    }

    // Update is called once per frame
    public void Ataque1()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioataque);
        AudioManager.Instance.PlaySound(SoundAtaque);

        foreach (Collider2D collision in objetos)
        {
            if(collision.CompareTag("Player"))
            {
                GameManagerScript.Instance.PerderVida();
                CharacterController characterController = collision.GetComponent<CharacterController>();

            if (characterController != null)
            {
                // Aplicar un golpe al personaje
                characterController.AplicarGolpe();

                // Obtener la dirección desde el enemigo al personaje
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;

                // Aplicar una fuerza al personaje para empujarlo
                characterController.AplicarEmpuje(pushDirection);
            }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioataque);
    }
}
