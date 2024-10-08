using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstruirScript : MonoBehaviour
{

    //Objetos
    [HideInInspector] public GameObject sceneScripts;
    public GameObject torretaSeleccionada = null;
    private SpriteRenderer sr;
    private ConstruirScriptGeneral scrConstruir;
    private scrBotones botonesScript;

    //Variables

    public float precioSeleccionado;
    private bool isPaused;
    void Awake()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        sr = gameObject.GetComponent<SpriteRenderer>();
        scrConstruir = sceneScripts.GetComponent<ConstruirScriptGeneral>();
        botonesScript = sceneScripts.GetComponent<scrBotones>();
    }

    void Update()
    {
        isPaused = PauseScript.isPaused;


    }

    private void OnMouseDown()
    {
        //EVITAR QUE EL CLICK NO ATRAVIESE LA UI

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                if (go.gameObject.name == "cuadroMejora" || go.gameObject.name == "btnVender")
                {
                    return;
                }
            }
        }


        if ((scrConstruir.plataActual - precioSeleccionado) >= 0 && !isPaused)
        {
            if (torretaSeleccionada != null)
            {
                GameObject torreta = Instantiate(torretaSeleccionada, transform.position, Quaternion.identity);
                MejorasScript torretaScr = torreta.GetComponent<MejorasScript>();
                torretaScr.tileParaRenovar = this.gameObject;
                scrConstruir.plataActual -= precioSeleccionado;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (GameObject.Find("SCENESCRIPTS").GetComponent<scrBotones>().pasoTutorial == 4)
        {
            GameObject.Find("SCENESCRIPTS").GetComponent<scrBotones>().tutoBotón();
            foreach (GameObject tile in GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>().tiles)
            {
                if (tile != null)
                {
                    tile.GetComponent<BoxCollider2D>().enabled = false;
                    tile.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            Image botonSí = GameObject.Find("btnSí").GetComponent<Image>();
            botonSí.enabled = true;
            botonSí.rectTransform.anchoredPosition = new Vector2(botonSí.rectTransform.anchoredPosition.x - 1000, botonSí.rectTransform.anchoredPosition.y);
        }

    }

    private void OnMouseEnter()
    {
        if (!isPaused) sr.color = Color.green;
    }

    private void OnMouseExit()
    {
        sr.color = Color.white;
    }
}
