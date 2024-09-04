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

    //Variables

    public float precioSeleccionado;
    private bool isPaused;
    void Awake()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        sr = gameObject.GetComponent<SpriteRenderer>();
        scrConstruir = sceneScripts.GetComponent<ConstruirScriptGeneral>();
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
                Instantiate(torretaSeleccionada, transform.position, Quaternion.identity);
                scrConstruir.plataActual -= precioSeleccionado;
                Destroy(this.gameObject);
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
