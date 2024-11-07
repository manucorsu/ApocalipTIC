using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstruirScript : MonoBehaviour
{
    [SerializeField] private AudioClip buySfx;
    //Objetos
    [HideInInspector] public GameObject sceneScripts;
    public GameObject torretaSeleccionada = null;
    private SpriteRenderer sr;
    private ConstruirScriptGeneral scrConstruir;
    private scrBotones botonesScript;

    //Variables

    public float precioSeleccionado;
    private bool isPaused;
    private bool isBig = true;
    private float timer = 0;

    void Awake()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        sr = gameObject.GetComponent<SpriteRenderer>();
        scrConstruir = sceneScripts.GetComponent<ConstruirScriptGeneral>();
        botonesScript = sceneScripts.GetComponent<scrBotones>();
    }

    void Update()
    {
        isPaused = PauseScript.Instance.IsPaused;

        if (isBig)
        {
            if (timer < 1)
            {
                timer += Time.deltaTime;
            } else
            {
                timer = 0;
                transform.localScale = new Vector2(0.9f, 0.9f);
                isBig = false;
            }
        } else
        {
            if (timer < 1)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                transform.localScale = new Vector2(1, 1);
                isBig = true;
            }
        }
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
                if (go.gameObject.name == "cuadroMejora" || go.gameObject.name == "btnVender" || go.gameObject.name == "Bloqueo (3)" || go.gameObject.name == "Bloqueo (1)")
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
                try { SoundManager.Instance.PlayUISound(buySfx, 0.4f); }
                catch (System.NullReferenceException)
                {
                    Debug.LogError("NullReferenceExeception: El singleton de SoundManager fue null.\n" +
                        "NUNCA inicies Game directamente, siempre pasá por Inicio primero.");
                }
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

        foreach (GameObject boton in GameObject.Find("SCENESCRIPTS").GetComponent<scrBotones>().botones)
        {
            if (GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>().plataActual - boton.GetComponent<scrBotonTorreta>().precio < 0)
            {
                Image imagen = boton.GetComponent<Image>();
                imagen.sprite = GameObject.Find("SCENESCRIPTS").GetComponent<scrBotones>().btTorretaSprite1;
            }
        }


        if (sceneScripts.GetComponent<scrBotones>().pasoTutorial == 5)
        {

            foreach (GameObject boton in sceneScripts.GetComponent<scrBotones>().botonesTorretas)
            {
                Image imagen = boton.GetComponent<Image>();
                imagen.sprite = sceneScripts.GetComponent<scrBotones>().btTorretaSprite1;
            }

            foreach (GameObject tile in sceneScripts.GetComponent<scrBotones>().tiles)
            {
                if (tile != null)
                {
                    tile.GetComponent<ConstruirScript>().torretaSeleccionada = null;
                }
            }
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
