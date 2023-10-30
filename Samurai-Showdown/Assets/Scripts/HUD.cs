using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI points;
    public GameManagerScript gameManager;
    public GameObject[] vidas;
    // Update is called once per frame
    void Update()
    {
        points.text = gameManager.PuntosTotales.ToString();
    }
    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }
    public void ActivarVida (int indice)
    {
        vidas[indice].SetActive(true);
    }
}
