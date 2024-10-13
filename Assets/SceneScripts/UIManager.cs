using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public Image cuadroMejora;
    public bool cuadroMejoraAbierto = false;
    public int i; //¿¿¿???

    // Start is called before the first frame update
    void Start()
    {
        cuadroMejora = GameObject.Find("cuadroMejora").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
