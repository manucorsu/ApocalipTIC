using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConsumiblesScript : MonoBehaviour
{

    //Objetos

    public GameObject consumibleSeleccionado = null;
    public GameObject sceneScripts;
    public ConstruirScriptGeneral scrConstruir;
    public LayerMask zonasConsumibles;
    public GameObject zonaConsumible;
    public ZonaConsumiblesScript scrZona;

    //Variables 

    public float precioSeleccionado;

    // Start is called before the first frame update
    void Awake()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        zonaConsumible = GameObject.Find("ConsumiblesZona1");
        scrConstruir = sceneScripts.GetComponent<ConstruirScriptGeneral>();
        scrZona = zonaConsumible.GetComponent<ZonaConsumiblesScript>();
    }

    // Update is called once per frame
    void Update()
    {
        consumibleSeleccionado = scrZona.consumibleSeleccionado;
    }

    public void OnMouseDown()
    {
        if (consumibleSeleccionado != null)
        {
            if ((scrConstruir.plataActual - precioSeleccionado) >= 0)
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0f;
                Instantiate(consumibleSeleccionado, mouseWorldPos, Quaternion.identity);
                scrConstruir.plataActual -= precioSeleccionado;
            }
        }
    }
}
