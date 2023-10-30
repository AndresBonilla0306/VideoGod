using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D coll)
     {
         if(coll.CompareTag("Player"))
         {
             GameManagerScript.Instance.PerderVida();

             coll.GetComponent<CharacterController>().AplicarGolpe();
             Debug.Log("ataque");
         }
     }
}
