using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConsumiblesScript : MonoBehaviour
{

    public GameObject consumibleSeleccionado = null;
    public GameObject construir;
    public ConstruirScriptGeneral scrConstruir;
    public float precioSeleccionado;

    // Start is called before the first frame update
    void Start()
    {
        construir = GameObject.Find("Construir");
        scrConstruir = construir.GetComponent<ConstruirScriptGeneral>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (consumibleSeleccionado != null)
        {
            if ((scrConstruir.plataActual - precioSeleccionado) >= 0)
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0f;
                Instantiate(consumibleSeleccionado, mouseWorldPos, Quaternion.identity);
            }
        }
    }
}
