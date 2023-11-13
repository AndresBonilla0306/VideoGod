using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public AudioClip sonidoAtaque;
     private void OnTriggerEnter2D(Collider2D coll)
     {
         if(coll.CompareTag("Player"))
         {

             GameManagerScript.Instance.PerderVida();
             AudioManager.Instance.PlaySound(sonidoAtaque);
             CharacterController characterController = coll.GetComponent<CharacterController>();

            if (characterController != null)
            {
                // Aplicar un golpe al personaje
                characterController.AplicarGolpe();

                // Obtener la dirección desde el enemigo al personaje
                Vector2 pushDirection = (coll.transform.position - transform.position).normalized;

                // Aplicar una fuerza al personaje para empujarlo
                characterController.AplicarEmpuje(pushDirection);
            }
         }
     }
}
