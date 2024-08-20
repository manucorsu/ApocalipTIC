using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MejorasScript : MonoBehaviour
{

    //Objetos

    public GameObject sceneScripts;
    public GameObject cuadroMejora;
    public RectTransform cuadroMejoraTransform;
    private EnemySpawner enemySpawner;

    public TMP_Text textoTorreta;
    public TMP_Text textoSpeed;
    public TMP_Text textoRange;
    public TMP_Text textoDamage;


    //Variables



    // Start is called before the first frame update
    void Start()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        cuadroMejora = GameObject.Find("cuadroMejora");
        cuadroMejoraTransform = cuadroMejora.GetComponent<RectTransform>();
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
            Vector2 posiciónDestino = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            cuadroMejora.transform.position = Vector3.MoveTowards(cuadroMejora.transform.position, posiciónDestino, 1000);
        }
    }
}
