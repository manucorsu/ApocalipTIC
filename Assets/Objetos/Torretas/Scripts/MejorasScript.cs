using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MejorasScript : MonoBehaviour
{

    //Objetos

    public GameObject sceneScripts;
    public Image cuadroMejora;
    private EnemySpawner enemySpawner;

    //Variables



    // Start is called before the first frame update
    void Start()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        enemySpawner = sceneScripts.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (enemySpawner.spawnear == false)
        {
            
        }
    }
}
