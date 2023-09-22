using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI points;
    public GameObject[] vidas;
    // Update is called once per frame
    void Update()
    {
        
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
