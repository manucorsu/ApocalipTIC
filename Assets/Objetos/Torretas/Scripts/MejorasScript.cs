using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MejorasScript : MonoBehaviour
{

    //Objetos

    private GameObject sceneScripts;
    private GameObject cuadroMejora;
    private RectTransform cuadroMejoraTransform;

    public TMP_Text textoTorreta;
    public TMP_Text textoMejora1;
    public TMP_Text textoMejora2;
    public TMP_Text textoMejora3;

    public TMP_Text textoPrecioMejora;

    public GameObject tiralápices;
    public GameObject nicho;
    public GameObject tacho;
    public GameObject proyector;
    public GameObject tiralapiceras;
    public GameObject lanzabombuchas;
    public GameObject imán;
    public GameObject parlante;

    private Button btnMejora1;
    private Button btnMejora2;
    private Button btnMejora3;

    public scrBotones scrbotones;

    public Camera mainCamera;
    public RectTransform canvasTransform;

    public GameObject tileParaRenovar;

    //Variables

    public float precioExtraParaVender;
    public bool isPotenciado = false;
    private float precioParaMejorar;
    

    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        sceneScripts = GameObject.Find("SCENESCRIPTS");
        cuadroMejora = GameObject.Find("cuadroMejora");

        textoTorreta = GameObject.Find("txtMejoraTorreta").GetComponent<TMP_Text>();
        textoMejora1 = GameObject.Find("txtMejoraSpeed").GetComponent<TMP_Text>();
        textoMejora2 = GameObject.Find("txtMejoraRange").GetComponent<TMP_Text>();
        textoMejora3 = GameObject.Find("txtMejoraDamage").GetComponent<TMP_Text>();

        textoPrecioMejora = GameObject.Find("txtPrecioMejora").GetComponent<TMP_Text>();

        btnMejora1 = GameObject.Find("btnMejora1").GetComponent<Button>();
        btnMejora2 = GameObject.Find("btnMejora2").GetComponent<Button>();
        btnMejora3 = GameObject.Find("btnMejora3").GetComponent<Button>();

        scrbotones = sceneScripts.GetComponent<scrBotones>();

        cuadroMejoraTransform = cuadroMejora.GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseDown()
    {
        if (sceneScripts.GetComponent<scrBotones>().pasoTutorial == 5)
        {
            return;
        }

        //EVITAR QUE EL CLICK NO ATRAVIESE LA UI
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                if (go.gameObject.name == "cuadroMejora")
                {
                    return;
                }
            }
        }


        if (EnemySpawner.spawnear == false)
        {
            cuadroMejora.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.TransformPoint(Vector3.zero));
           
            float thisPosX = this.transform.position.x + 2.5f;
            float thisPosY = this.transform.position.y;

            if (this.transform.position.x > 8) {
                thisPosX -= 5.5f;
            }
            if (this.transform.position.y > 3)
            {
                thisPosY -= 1;
            }
            if (this.transform.position.y < -4)
            {
                thisPosY += 2;
            }

            Vector2 ViewportPosition = mainCamera.WorldToViewportPoint(new Vector2(thisPosX, thisPosY));
            Vector2 WorldObject_ScreenPosition = new Vector2(((ViewportPosition.x * canvasTransform.sizeDelta.x) - (canvasTransform.sizeDelta.x * 0.5f)), ((ViewportPosition.y * canvasTransform.sizeDelta.y) - (canvasTransform.sizeDelta.y * 0.5f)));
            cuadroMejoraTransform.anchoredPosition = WorldObject_ScreenPosition;
        }

        Mejorar();

        if (sceneScripts.GetComponent<scrBotones>().pasoTutorial == 6)
        {
            sceneScripts.GetComponent<scrBotones>().tutoBotón();
            Image botonSí = GameObject.Find("btnSí").GetComponent<Image>();
            botonSí.rectTransform.anchoredPosition = new Vector2(botonSí.rectTransform.anchoredPosition.x - 1000, botonSí.rectTransform.anchoredPosition.y);
        }
    }

    public void Mejorar()
    {
        //TIRALÁPICES

        if (this.gameObject == tiralápices)
        {
            TorretaScript torretaScr = this.GetComponent<TorretaScript>();
            textoTorreta.text = "Tiralápices";
            textoMejora1.text = "Velocidad:" + torretaScr.nivel1; //torretaScr.bps.ToString();
            textoMejora2.text = "Rango:" + torretaScr.nivel2; //torretaScr.rango.ToString();
            textoMejora3.text = "Daño:" + torretaScr.nivel3; //torretaScr.dmg.ToString();
            textoPrecioMejora.text = "$" + torretaScr.precioMejora;
            precioParaMejorar = torretaScr.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = true;
            btnMejora3.GetComponent<Image>().enabled = true;

            if (torretaScr.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel3 == 3) { btnMejora3.GetComponent<Button>().interactable = false; } else {btnMejora3.GetComponent<Button>().interactable = true; }
        }

        //NICHO

        if (this.gameObject == nicho)
        {
            TorretaScript2 torretaScr2 = this.GetComponent<TorretaScript2>();
            textoTorreta.text = "Nicho";
            textoMejora1.text = "Velocidad:" + torretaScr2.nivel1; //torretaScr2.chorroScale.ToString();
            textoMejora2.text = "Daño:" + torretaScr2.nivel2; //torretaScr2.dps.ToString();
            textoMejora3.text = "";
            textoPrecioMejora.text = "$" + torretaScr2.precioMejora;
            precioParaMejorar = torretaScr2.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr2.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
            btnMejora3.GetComponent<Image>().enabled = false;

            if (torretaScr2.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr2.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }
        }

        //TACHO

        if (this.gameObject == tacho)
        {
            TorretaScript3 torretaScr3 = this.GetComponent<TorretaScript3>();
            textoTorreta.text = "Tacho";
            textoMejora1.text = "Velocidad:" + torretaScr3.nivel1; //torretaScr3.cooldown.ToString();
            textoMejora2.text = "Rango:" + torretaScr3.nivel2; //torretaScr3.rango.ToString();
            textoMejora3.text = "";
            textoPrecioMejora.text = "$" + torretaScr3.precioMejora;
            precioParaMejorar = torretaScr3.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr3.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
            btnMejora3.GetComponent<Image>().enabled = false;

            if (torretaScr3.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr3.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }

        }

        //PROYECTOR

        if (this.gameObject == proyector)
        {
            TorretaScript4 torretaScr4 = this.GetComponent<TorretaScript4>();
            textoTorreta.text = "Proyector";
            textoMejora1.text = "Velocidad:" + torretaScr4.nivel1; //torretaScr4.rayoScale.ToString();
            textoMejora2.text = "Rango:" + torretaScr4.nivel2; //torretaScr4.dps.ToString();
            textoMejora3.text = "";
            textoPrecioMejora.text = "$" + torretaScr4.precioMejora;
            precioParaMejorar = torretaScr4.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr4.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.enabled = false;
            btnMejora3.GetComponent<Image>().enabled = false;

            if (torretaScr4.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr4.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }
        }

        //TIRALAPICERAS

        if (this.gameObject == tiralapiceras)
        {
            TorretaScript torretaScr = this.GetComponent<TorretaScript>();
            textoTorreta.text = "Tiralapiceras";
            textoMejora1.text = "Velocidad:" + torretaScr.nivel1; //torretaScr.bps.ToString();
            textoMejora2.text = "Rango:" + torretaScr.nivel2; //torretaScr.rango.ToString();
            textoMejora3.text = "Daño:" + torretaScr.nivel3; //torretaScr.dmg.ToString();
            textoPrecioMejora.text = "$" + torretaScr.precioMejora;
            precioParaMejorar = torretaScr.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.GetComponent<Image>().enabled = true;

            if (torretaScr.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel3 == 3) { btnMejora3.GetComponent<Button>().interactable = false; } else {btnMejora3.GetComponent<Button>().interactable = true; }
        }

        //LANZABOMBUCHAS

        if (this.gameObject == lanzabombuchas)
        {
            TorretaScript torretaScr = this.GetComponent<TorretaScript>();
            textoTorreta.text = "Lanzabombuchas";
            textoMejora1.text = "Velocidad:" + torretaScr.nivel1; //torretaScr.bps.ToString();
            textoMejora2.text = "Rango:" + torretaScr.nivel2; //torretaScr.rango.ToString();
            textoMejora3.text = "Daño:" + torretaScr.nivel3; //torretaScr.dmg.ToString();
            textoPrecioMejora.text = "$" + torretaScr.precioMejora;
            precioParaMejorar = torretaScr.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.GetComponent<Image>().enabled = true;

            if (torretaScr.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel3 == 3) { btnMejora3.GetComponent<Button>().interactable = false; } else {btnMejora3.GetComponent<Button>().interactable = true; }
        }

        //IMÁN

        if (this.gameObject == imán)
        {
            ImanScript torretaScr = this.GetComponent<ImanScript>();
            textoTorreta.text = "Imán";
            textoMejora1.text = "Velocidad:" + torretaScr.nivel1; //torretaScr.bps.ToString();
            textoMejora2.text = "Rango:" + torretaScr.nivel2; //torretaScr.rango.ToString();
            textoMejora3.text = "Ganancia:" + torretaScr.nivel3; //torretaScr.dmg.ToString();
            textoPrecioMejora.text = "$" + torretaScr.precioMejora;
            precioParaMejorar = torretaScr.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.GetComponent<Image>().enabled = true;

            if (torretaScr.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel3 == 3) { btnMejora3.GetComponent<Button>().interactable = false; } else {btnMejora3.GetComponent<Button>().interactable = true; }
        }

        //PARLANTE

        if (this.gameObject == parlante)
        {
            ParlanteScript torretaScr = this.GetComponent<ParlanteScript>();
            textoTorreta.text = "Parlante";
            textoMejora1.text = "Velocidad:" + torretaScr.nivel1; //torretaScr.bps.ToString();
            textoMejora2.text = "Rango:" + torretaScr.nivel2; //torretaScr.rango.ToString();
            textoMejora3.text = "Daño:" + torretaScr.nivel3; //torretaScr.dmg.ToString();
            textoPrecioMejora.text = "$" + torretaScr.precioMejora;
            precioParaMejorar = torretaScr.precioMejora;
            scrbotones.precioParaVender = Mathf.Round(torretaScr.precio / 2) + precioExtraParaVender;

            btnMejora1.enabled = true;
            btnMejora2.enabled = true;
            btnMejora3.GetComponent<Image>().enabled = true;

            if (torretaScr.nivel1 == 3) { btnMejora1.GetComponent<Button>().interactable = false; } else {btnMejora1.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel2 == 3) { btnMejora2.GetComponent<Button>().interactable = false; } else {btnMejora2.GetComponent<Button>().interactable = true; }
            if (torretaScr.nivel3 == 3) { btnMejora3.GetComponent<Button>().interactable = false; } else {btnMejora3.GetComponent<Button>().interactable = true; }
        }

        scrbotones.torretaParaMejorar = this.gameObject;

        if (sceneScripts.GetComponent<ConstruirScriptGeneral>().plataActual - precioParaMejorar < 0)
        {
            btnMejora1.GetComponent<Button>().interactable = false;
            btnMejora2.GetComponent<Button>().interactable = false;
            btnMejora3.GetComponent<Button>().interactable = false;
        }
    }

    }
