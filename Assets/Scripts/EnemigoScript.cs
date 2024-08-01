using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    private Vector3 initialPos;
    public GameObject[] spawners;
    private string spName;
    private bool puedeMoverse = true;

    private void Awake()
    {
        //initialPos = this.transform.position;
        //foreach(GameObject spawner in spawners)
        //{
        //    if(initialPos == spawner.transform.position)
        //    {
        //        spName = spawner.name;
        //        break;
        //    }
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"spName = {spName}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
