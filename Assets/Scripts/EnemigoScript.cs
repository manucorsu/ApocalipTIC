using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public float spd;
    public string spName;
    public bool puedeMoverse = true;
    [SerializeField] GameObject waypoints;

    // Start is called before the first frame update
    void Start()
    {
        BuscarPath();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BuscarPath()
    {
        /*jajaja no puedo usar switch porque apareció en 2019 y unity 2018 no lo acepta 
         jajajaja*/

        if(spName == "A1" || spName == "A4" || spName == "A7")
        {
            A147();
        }
        else if(spName == "A2" || spName == "A5" || spName == "A8")
        {
            A258();
        }
        else if(spName == "A3" || spName == "A6" || spName == "A9")
        {
            A369();
        }
        else if(spName == "B1" || spName == "B3" || spName == "B5")
        {
            B135();
        }
        else if(spName == "B2" || spName == "B4" || spName == "B6")
        {
            B246();
        }
        else if (spName == "C1" || spName == "C2" || spName == "C3")
        {
            C123();
        }
        else if(spName == "C4" || spName == "C5" || spName == "C6")
        {
            C456();
        }
        else if(spName == "D1" || spName == "D3" || spName == "D5")
        {
            D135();
        }
        else if(spName == "D2" || spName == "D4" || spName == "D6")
        {
            D246();
        }
    }

    #region caminos, ver SPAWNERSGUIDE
    //Los "A" tienen la suerte de que no deben girar en ningún momento para llegar al objetivo
    private void A147()
    {
        
    }

    private void A258()
    {

    }

    private void A369()
    {

    }

    private void B135()
    {

    }

    private void B246()
    {

    }

    private void C123()
    {

    }

    private void C456()
    {

    }

    private void D135()
    {
        
    }

    private void D246()
    {

    }
    #endregion
}