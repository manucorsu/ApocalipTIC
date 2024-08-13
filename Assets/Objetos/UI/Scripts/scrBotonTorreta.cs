using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class scrBotonTorreta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cuadroTorreta;
    public Text txtTítuloTorreta;
    public Text txtDescTorreta;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject == GameObject.Find("btnTiralápices"))
        {
            txtTítuloTorreta.text = "Tiralápices";
            txtDescTorreta.text = "Arma básica que dispara lápices.";
        }

        if (this.gameObject == GameObject.Find("btnNicho"))
        {
            txtTítuloTorreta.text = "Nicho";
            txtDescTorreta.text = "Lanza un chorro de agua con daño en área.";
        }

        if (this.gameObject == GameObject.Find("btnTacho"))
        {
            txtTítuloTorreta.text = "Tacho";
            txtDescTorreta.text = "Criatura que come un enemigo a la vez.";
        }

        if (this.gameObject == GameObject.Find("btnProyector"))
        {
            txtTítuloTorreta.text = "Proyector";
            txtDescTorreta.text = "Ciega a los enemigos frenándolos por un tiempo.";
        }

        cuadroTorreta.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cuadroTorreta.SetActive(false);
    }
}
