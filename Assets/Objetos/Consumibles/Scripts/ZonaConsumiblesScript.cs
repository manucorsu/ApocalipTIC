using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConsumiblesScript : MonoBehaviour
{

    public GameObject consumibleSeleccionado;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Instantiate(consumibleSeleccionado, mouseWorldPos, Quaternion.identity);
    }
}
