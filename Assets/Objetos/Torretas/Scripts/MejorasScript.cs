using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MejorasScript : MonoBehaviour
{

    //Objetos

    private GameObject sceneScripts;
    private GameObject cuadroMejora;
    private RectTransform cuadroMejoraTransform;
    private EnemySpawner enemySpawner;

    public TMP_Text textoTorreta;
    public TMP_Text textoSpeed;
    public TMP_Text textoRange;
    public TMP_Text textoDamage;

    public GameObject tiralápices;
    public GameObject nicho;
    public GameObject tacho;
    public GameObject proyector;

    private Button btnMejora1;
    private Button btnMejora2;
    private Button btnMejora3;


    //Variables



    // Start is called before the first frame update
    void Start()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        cuadroMejora = GameObject.Find("cuadroMejora");

        textoTorreta = GameObject.Find("txtMejoraTorreta").GetComponent<TMP_Text>();
        textoSpeed = GameObject.Find("txtMejoraSpeed").GetComponent<TMP_Text>();
        textoRange = GameObject.Find("txtMejoraRange").GetComponent<TMP_Text>();
        textoDamage = GameObject.Find("txtMejoraDamage").GetComponent<TMP_Text>();

        btnMejora1 = GameObject.Find("btnMejora1").GetComponent<Button>();
        btnMejora2 = GameObject.Find("btnMejora2").GetComponent<Button>();
        btnMejora3 = GameObject.Find("btnMejora3").GetComponent<Button>();

        cuadroMejoraTransform = cuadroMejora.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (EnemySpawner.spawnear == false)
        {
            Vector2 posiciónDestino = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            cuadroMejora.transform.position = Vector3.MoveTowards(cuadroMejora.transform.position, posiciónDestino, 1000);
        }

        //TIRALÁPICES

        if (this.gameObject == tiralápices)
        {
            TorretaScript torretaScr = this.GetComponent<TorretaScript>();
            textoTorreta.text = "Tiralápices";
            textoSpeed.text = "Spd:" + torretaScr.bps.ToString();
            textoRange.text = "Ran:" + torretaScr.rango.ToString();
            textoDamage.text = "Dmg:" + torretaScr.dmg.ToString();

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = true;
        }

        //NICHO

        if (this.gameObject == nicho)
        {
            TorretaScript2 torretaScr2 = this.GetComponent<TorretaScript2>();
            textoTorreta.text = "Nicho";
            textoSpeed.text = "Ran:" + torretaScr2.chorroScale.ToString();
            textoRange.text = "Dmg:" + torretaScr2.dps.ToString();
            textoDamage.text = "";

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
        }

        //TACHO

        if (this.gameObject == tacho)
        {
            TorretaScript3 torretaScr3 = this.GetComponent<TorretaScript3>();
            textoTorreta.text = "Tacho";
            textoSpeed.text = "Spd:" + torretaScr3.cooldown.ToString();
            textoRange.text = "Ran:" + torretaScr3.rango.ToString();
            textoDamage.text = "";

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
        }

        //PROYECTOR

        if (this.gameObject == proyector)
        {
            TorretaScript4 torretaScr4 = this.GetComponent<TorretaScript4>();
            textoTorreta.text = "Proyector";
            textoSpeed.text = "Ran:" + torretaScr4.rayoScale.ToString();
            textoRange.text = "Dmg:" + torretaScr4.dps.ToString();
            textoDamage.text = "";

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
        }


    }
}
