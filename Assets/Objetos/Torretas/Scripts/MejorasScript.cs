using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MejorasScript : MonoBehaviour
{

    //Objetos

    public GameObject sceneScripts;
    public GameObject cuadroMejora;
    private EnemySpawner enemySpawner;

    //Variables



    // Start is called before the first frame update
    void Start()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        cuadroMejora = GameObject.Find("cuadroMejora");
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
            Vector3 posiciónDestino = new Vector3(this.transform.position.x + 3, this.transform.position.y);

            if (this.gameObject.transform.position.y < -5)
            {
                posiciónDestino.y += 1.5f;
            }

            if (this.gameObject.transform.position.y > 4)
            {
                posiciónDestino.y -= 1.5f;
            }

            if (this.gameObject.transform.position.x > 7)
            {
                posiciónDestino.x -= 6;
            }

            cuadroMejora.transform.position = Vector3.MoveTowards(cuadroMejora.transform.position, posiciónDestino, 1000);
        }
    }
}
