using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvaScript : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene ("SampleScene");
    }

    public void Options()
    {
        SceneManager.LoadScene ("Opciones");
    }


    public void Back()
    {
        SceneManager.LoadScene ("Menu");
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

}
