using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
   public static GameManagerScript Instance {get; private set; }
   public HUD hud;
   private int vidas = 3;
   public int PuntosTotales {get {return puntosTotales;}}
   private int puntosTotales;
   void Awake()
   {
    if(Instance == null)
    {
        Instance = this;
    }else 
    {
        Debug.Log("Más de un GameManger en la escena");
    }
   }
   public void PerderVida()
   {
    vidas -= 1;

    if(vidas == 0)
    {
        SceneManager.LoadScene(0);
    }
    hud.DesactivarVida(vidas);
   }
   public bool GanarVida()
   {

    if(vidas == 3)
    {
        return false;
    }
    hud.ActivarVida(vidas);
    vidas += 1;
    return true;
   }
   public void SumarPoints(int puntosSumar)
   {
        puntosTotales += puntosSumar;
   }
}
