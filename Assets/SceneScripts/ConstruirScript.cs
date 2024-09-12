using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            foreach(var go in raycastResults)
            {
                if (go.gameObject.name == "cuadroMejora")
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
