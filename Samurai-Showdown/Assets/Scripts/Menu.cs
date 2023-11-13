using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject PantallaMuerte;
    [SerializeField] private GameObject PantallaWin;
    public AudioClip Muelto;
    public AudioClip Siu;
    public static Menu Instance { get; private set; }
    private void Awake()
    {
        if( Instance == null)
        {
            Instance = this;
        }else
        {
            Debug.Log("Shit");
        }
    }
    public void Muerte()
    {
        Time.timeScale = 0f;
        PantallaMuerte.SetActive(true);
        Debug.Log("Morido");
        AudioManager.Instance.DetenerAudio();
        AudioManager.Instance.PlaySound(Muelto);
    }
    public void Win()
    {
        Time.timeScale = 0f;
        PantallaWin.SetActive(true);
        AudioManager.Instance.PlaySound(Siu);
    }
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void volverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene ("Menu");
    }
}
