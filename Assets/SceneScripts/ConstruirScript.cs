using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstruirScript : MonoBehaviour
{

    //Objetos

    public GameObject torretaSeleccionada;
    private SpriteRenderer sr;
    public GameObject construir;
    private ConstruirScriptGeneral scrConstruir;

    //Variables

    public float precioSeleccionado;



    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        construir = GameObject.Find("Construir");
        scrConstruir = construir.GetComponent<ConstruirScriptGeneral>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseDown()
    {
        if ((scrConstruir.plataActual - precioSeleccionado) >= 0)
        {
            Instantiate(torretaSeleccionada, transform.position, Quaternion.identity);
            scrConstruir.plataActual -= precioSeleccionado;
            Destroy(this.gameObject);
        }
    }

    private void OnMouseEnter()
    {
        sr.color = Color.green;
    }

    private void OnMouseExit()
    {
        sr.color = Color.white;
    }
}
