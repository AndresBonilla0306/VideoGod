using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float vida;
    public Animator anim;
    public int valor = 1;
    public GameManagerScript gameManager;
    public AudioClip sonidoDamage;

    public void TomarDaño(float daño)
    {
        vida -= daño;
        AudioManager.Instance.PlaySound(sonidoDamage);
        if (vida <= 0)
        {
            Muerte();
        }
    }
    public void Muerte()
    {
        anim.SetTrigger("Muelto");
        StartCoroutine (DelayAndDie());
        gameManager.SumarPoints(valor);
    }
    IEnumerator DelayAndDie() {
    yield return new WaitForSeconds(1);
    Destroy(this.gameObject);
    }
}
