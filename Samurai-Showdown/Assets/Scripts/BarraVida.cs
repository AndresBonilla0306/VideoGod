using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        //slider = GetComponent<Slider>();
    }
    public void CambiarVidaMax(float vidaMaxima)
    {
        slider = GetComponent<Slider>();
        slider.maxValue = vidaMaxima;
    }
    public void CambiarVidaActual(float cantidadVida)
    {
        slider = GetComponent<Slider>();
        slider.value = cantidadVida;
    }
    public void InicializarVida (float cantidadVida)
    {
        slider = GetComponent<Slider>();
        CambiarVidaMax(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }
}
