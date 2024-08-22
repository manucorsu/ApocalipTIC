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
    public TMP_Text textoMejora1;
    public TMP_Text textoMejora2;
    public TMP_Text textoMejora3;

    public GameObject tiralápices;
    public GameObject nicho;
    public GameObject tacho;
    public GameObject proyector;

    private Button btnMejora1;
    private Button btnMejora2;
    private Button btnMejora3;

    public scrBotones scrbotones;

    public Camera mainCamera;


    //Variables

    private int nivel1;
    private bool nivel2;
    private bool nivel3;


    // Start is called before the first frame update
    void Start()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        cuadroMejora = GameObject.Find("cuadroMejora");

        textoTorreta = GameObject.Find("txtMejoraTorreta").GetComponent<TMP_Text>();
        textoMejora1 = GameObject.Find("txtMejoraSpeed").GetComponent<TMP_Text>();
        textoMejora2 = GameObject.Find("txtMejoraRange").GetComponent<TMP_Text>();
        textoMejora3 = GameObject.Find("txtMejoraDamage").GetComponent<TMP_Text>();

        btnMejora1 = GameObject.Find("btnMejora1").GetComponent<Button>();
        btnMejora2 = GameObject.Find("btnMejora2").GetComponent<Button>();
        btnMejora3 = GameObject.Find("btnMejora3").GetComponent<Button>();

        scrbotones = sceneScripts.GetComponent<scrBotones>();

        cuadroMejoraTransform = cuadroMejora.GetComponent<RectTransform>();
        enemySpawner = sceneScripts.GetComponent<EnemySpawner>();

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnMouseDown()
    {
        if (enemySpawner.spawnear == false)
        {
            Vector2 posiciónDestino = new Vector2(Input.mousePosition.x + 100, Input.mousePosition.y);
            cuadroMejora.transform.position = Vector3.MoveTowards(cuadroMejora.transform.position, posiciónDestino, 10000);
        }

        Mejorar();
    }

    public void Mejorar()
    {
        

        //TIRALÁPICES

        if (this.gameObject == tiralápices)
        {
            TorretaScript torretaScr = this.GetComponent<TorretaScript>();
            textoTorreta.text = "Tiralápices";
            textoMejora1.text = "Spd:" + torretaScr.nivel1; //torretaScr.bps.ToString();
            textoMejora2.text = "Ran:" + torretaScr.nivel2; //torretaScr.rango.ToString();
            textoMejora3.text = "Dmg:" + torretaScr.nivel3; //torretaScr.dmg.ToString();

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = true;
        }

        //NICHO

        if (this.gameObject == nicho)
        {
            TorretaScript2 torretaScr2 = this.GetComponent<TorretaScript2>();
            textoTorreta.text = "Nicho";
            textoMejora1.text = "Ran:" + torretaScr2.nivel1; //torretaScr2.chorroScale.ToString();
            textoMejora2.text = "Dmg:" + torretaScr2.nivel2; //torretaScr2.dps.ToString();
            textoMejora3.text = "";

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
        }

        //TACHO

        if (this.gameObject == tacho)
        {
            TorretaScript3 torretaScr3 = this.GetComponent<TorretaScript3>();
            textoTorreta.text = "Tacho";
            textoMejora1.text = "Spd:" + torretaScr3.nivel1; //torretaScr3.cooldown.ToString();
            textoMejora2.text = "Ran:" + torretaScr3.nivel2; //torretaScr3.rango.ToString();
            textoMejora3.text = "";

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
        }

        //PROYECTOR

        if (this.gameObject == proyector)
        {
            TorretaScript4 torretaScr4 = this.GetComponent<TorretaScript4>();
            textoTorreta.text = "Proyector";
            textoMejora1.text = "Spd:" + torretaScr4.nivel1; //torretaScr4.rayoScale.ToString();
            textoMejora2.text = "Ran:" + torretaScr4.nivel2; //torretaScr4.dps.ToString();
            textoMejora3.text = "";

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
        }

        scrbotones.torretaParaMejorar = this.gameObject;

    }
}
