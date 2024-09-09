using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class scrBotonTorreta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cuadroTorreta;
    [SerializeField] private TMP_Text txtTítuloTorreta;
    [SerializeField] private TMP_Text txtDescTorreta;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject == GameObject.Find("btnTiralápices"))
        {
            txtTítuloTorreta.text = "Tiralápices";
            txtDescTorreta.text = "Arma básica que dispara lápices.";
            cuadroTorreta.SetActive(true);
        }

        if (this.gameObject == GameObject.Find("btnNicho"))
        {
            txtTítuloTorreta.text = "Nicho";
            txtDescTorreta.text = "Lanza un chorro de agua con daño en área.";
            cuadroTorreta.SetActive(true);
        }

        if (this.gameObject == GameObject.Find("btnTacho"))
        {
            txtTítuloTorreta.text = "Tacho";
            txtDescTorreta.text = "Criatura que come un enemigo a la vez.";
            cuadroTorreta.SetActive(true);
        }

        if (this.gameObject == GameObject.Find("btnProyector"))
        {
            txtTítuloTorreta.text = "Proyector";
            txtDescTorreta.text = "Ciega a los enemigos frenándolos por un tiempo.";
            cuadroTorreta.SetActive(true);
        }

        if (this.gameObject == GameObject.Find("btnTiralapiceras"))
        {
            txtTítuloTorreta.text = "Tiralapiceras";
            txtDescTorreta.text = "Dispara lapiceras a largas distancias.";
            cuadroTorreta.SetActive(true);
        }

        if (this.gameObject == GameObject.Find("btnBidón"))
        {
            txtTítuloTorreta.text = "Bidón";
            txtDescTorreta.text = "Provoca daño en área en una zona determinada.";
            cuadroTorreta.SetActive(true);
        }

        if (this.gameObject == GameObject.Find("btnPegamento"))
        {
            txtTítuloTorreta.text = "Pegamento";
            txtDescTorreta.text = "Deja un charco que realentiza a los enemigos.";
            cuadroTorreta.SetActive(true);
        }

        if (this.gameObject == GameObject.Find("btnPalomas"))
        {
            txtTítuloTorreta.text = "Palomas";
            txtDescTorreta.text = "Estampida que daña a todos los enemigos.";
            cuadroTorreta.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cuadroTorreta.SetActive(false);
    }
}
