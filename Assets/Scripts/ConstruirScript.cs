using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstruirScript : MonoBehaviour
{

    public GameObject torretaSeleccionada;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Instantiate(torretaSeleccionada, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
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
