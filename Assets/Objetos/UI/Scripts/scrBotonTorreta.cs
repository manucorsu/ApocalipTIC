using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class scrBotonTorreta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cuadroTorreta;
    public float precio;
    [SerializeField] private TMP_Text txtTítuloTorreta;
    [SerializeField] private TMP_Text txtDescTorreta;

    private void Update()
    {
        if (GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>().plataActual - precio >= 0)
        {
            GetComponent<Button>().enabled = true;
            transform.Find("pricetag").GetComponent<TMP_Text>().color = Color.black;
            transform.Find("BloqueoBotón").GetComponent<Image>().enabled = false;
        } else
        {
            GetComponent<Button>().enabled = false;
            transform.Find("pricetag").GetComponent<TMP_Text>().color = Color.white;
            transform.Find("BloqueoBotón").GetComponent<Image>().enabled = true;
        }
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject == GameObject.Find("btnTiralápices"))
        {
            txtTítuloTorreta.text = "Tiralápices";
            txtDescTorreta.text = "Arma básica que dispara lápices.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(-322, -31);
        }

        if (this.gameObject == GameObject.Find("btnNicho"))
        {
            txtTítuloTorreta.text = "Nicho";
            txtDescTorreta.text = "Lanza un chorro de agua con daño en área.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(-290, -31);
        }

        if (this.gameObject == GameObject.Find("btnTacho"))
        {
            txtTítuloTorreta.text = "Tacho";
            txtDescTorreta.text = "Criatura que come un enemigo a la vez.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210, -31);
        }

        if (this.gameObject == GameObject.Find("btnProyector"))
        {
            txtTítuloTorreta.text = "Proyector";
            txtDescTorreta.text = "Lanza un flash que frena enemigos y revela bots invisibles.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, -31);
        }

        if (this.gameObject == GameObject.Find("btnTiralapiceras"))
        {
            txtTítuloTorreta.text = "Tiralapiceras";
            txtDescTorreta.text = "Dispara lapiceras a largas distancias.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, -31);
        }

        if (this.gameObject == GameObject.Find("btnLanzabombuchas"))
        {
            txtTítuloTorreta.text = "Lanzabombuchas";
            txtDescTorreta.text = "Lanza bombuchas que explotan en área.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -31);
        }

        if (this.gameObject == GameObject.Find("btnImán"))
        {
            txtTítuloTorreta.text = "Imán";
            txtDescTorreta.text = "Absorbe piezas de robots ganando plata.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, -31);
        }

        if (this.gameObject == GameObject.Find("btnParlante"))
        {
            txtTítuloTorreta.text = "Parlante";
            txtDescTorreta.text = "Daña en área y dispara ocho proyectiles.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -31);
        }

        if (this.gameObject == GameObject.Find("btnBidón"))
        {
            txtTítuloTorreta.text = "Bidón";
            txtDescTorreta.text = "Provoca daño en área en una zona determinada.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(190, 130);
        }

        if (this.gameObject == GameObject.Find("btnPegamento"))
        {
            txtTítuloTorreta.text = "Pegamento";
            txtDescTorreta.text = "Deja un charco que realentiza a los enemigos.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(190, 90);
        }

        if (this.gameObject == GameObject.Find("btnPalomas"))
        {
            txtTítuloTorreta.text = "Palomas";
            txtDescTorreta.text = "Estampida que daña a todos los enemigos.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(190, 20);
        }


        if (this.gameObject == GameObject.Find("btnNetbook"))
        {
            txtTítuloTorreta.text = "Netbook";
            txtDescTorreta.text = "Potencia a las torretas cercanas por unos segundos.";
            cuadroTorreta.SetActive(true);
            cuadroTorreta.GetComponent<RectTransform>().anchoredPosition = new Vector2(190, -31);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cuadroTorreta.SetActive(false);
    }
}
