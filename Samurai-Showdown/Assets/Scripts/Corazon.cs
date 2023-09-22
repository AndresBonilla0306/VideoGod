using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
    if(other.gameObject.CompareTag("Player"))
    {
        bool VidaGanada = GameManagerScript.Instance.GanarVida();
        if (VidaGanada)
        {
            Destroy(this.gameObject);
        }
    }
   }
}
