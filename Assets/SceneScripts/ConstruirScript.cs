using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        isPaused = sceneScripts.GetComponent<PauseScript>().isPaused;
    }

    private void OnMouseDown()
    {
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
