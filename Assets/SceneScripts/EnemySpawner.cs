using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    #region las estáticas
    public static bool spawnear = false;
    public static bool isBossFight = false;
    public static byte ronda = 1;
    public static List<GameObject> botsVivos = new List<GameObject>();
    public static byte botsASpawnear;
    public static uint botsEliminados = 0;
    public static byte botsEliminadosRonda = 0;
    #endregion

    [SerializeField] private GameObject prefabBoss;
    [Header("Arrays")]
    public GameObject[] pfbsEnemigos; //cada tipo de enemigo es un prefab y está en este array
    [SerializeField] private GameObject[] spawners;

    [Header("Sistema de rondas")]
    [SerializeField] private TMP_Text txtRonda;
    [SerializeField] private GameObject btnIniciarRonda; // No sé por qué no anda .enabled si es Button así que voy a usar SetActive 🤷
    [SerializeField] private float dificultad = 0.75f; //scaler de dificultad
    [SerializeField] private byte r1Bots = 6; //bots de la ronda 1, usados de base para todo el resto de las rondas
    [SerializeField] private float bps = 0.5f; //bots por segundo
    private float tiempoDesdeUltimoSpawn;
    private Boss boss;

    [SerializeField] private Sprite btnPlaySprite1;
    [SerializeField] private Sprite btnPlaySprite2;

    [SerializeField] private Image Oscuro1;
    [SerializeField] private Image Oscuro2;
    [SerializeField] private GameObject[] flechas;

    void Start()
    {
        ToggleSpawning(false);
        isBossFight = false;
        txtRonda.text = "Empezando RONDA 1/30";
        ronda = 1;
        botsVivos.Clear();
        botsASpawnear = 0;
        botsEliminados = 0;
        botsEliminadosRonda = 0;
        PauseScript.r1Bots = r1Bots;
        PauseScript.ronda = ronda;
        PauseScript.dificultad = dificultad;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!spawnear)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ronda--;
                if (ronda < 1) ronda = 1;
                txtRonda.text = $"override ronda {ronda}";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ronda++;
                if (ronda > 30) ronda = 30;
                txtRonda.text = $"override ronda {ronda}";
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ConstruirScriptGeneral construirscr = GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>();
                construirscr.plataActual += 10000;
                txtRonda.text = "Te regalé 10 mil pesos. De nada";
            }
        }
#endif
        if (spawnear)
        {
            tiempoDesdeUltimoSpawn += Time.deltaTime;
            if (tiempoDesdeUltimoSpawn >= (1f / bps) && botsASpawnear > 0) SpawnEnemy();
            if (botsVivos.Count <= 0 && botsASpawnear <= 0 && !isBossFight) TerminarRonda();
        }
    }

    public void EmpezarRonda()
    {
        if (GetComponent<scrBotones>().pasoTutorial == 11) return;
        if (spawnear == false)
        {
            Image cuadroMejora = GameObject.Find("cuadroMejora").GetComponent<Image>();

            cuadroMejora.rectTransform.position = new Vector2(100000000, 100000000);

            Oscuro1.GetComponent<Image>().enabled = !Oscuro1.GetComponent<Image>().isActiveAndEnabled;
            Oscuro2.GetComponent<Image>().enabled = !Oscuro2.GetComponent<Image>().isActiveAndEnabled;

            if (ronda == 15 || ronda == 30)
            {
                isBossFight = true;
                boss = Instantiate(prefabBoss, new Vector3(14.5f, 0.5f, 0), Quaternion.identity).GetComponent<Boss>();
            }
            else
            {
                isBossFight = false;
            }
            botsASpawnear = EnemyFormula(r1Bots, ronda, dificultad);
            ToggleSpawning(true);
            Image btnPlayImage = btnIniciarRonda.GetComponent<Image>();
            btnPlayImage.sprite = btnPlaySprite2;

            foreach (GameObject flecha in flechas)
            {
                flecha.GetComponent<SpriteRenderer>().enabled = false;
            }

            foreach(GameObject tile in GetComponent<ConstruirScriptGeneral>().tiles)
            {
                if (tile != null)
                {
                    tile.GetComponent<ConstruirScript>().torretaSeleccionada = null;
                }
            }

            foreach (GameObject boton in GetComponent<scrBotones>().botonesTorretas)
            {
                Image imagen = boton.GetComponent<Image>();
                imagen.sprite = GetComponent<scrBotones>().btTorretaSprite1;
            }
        }
    }

    public static byte EnemyFormula(byte r1, byte x, float d)
    {
        //https://www.desmos.com/calculator/hxfiurzdve
        return (byte)Mathf.Round(r1 * Mathf.Pow(x, d));
    }

    private void SpawnEnemy()
    {
        if (boss != null && boss.introDone == false) return;
        GameObject prefabElegido;
        while (false != true)
        {
            byte rie = (byte)Random.Range(0, pfbsEnemigos.Length); //RIE = Random Index para el array de Enemigos™
            prefabElegido = pfbsEnemigos[rie];
            if (prefabElegido.GetComponent<EnemigoScript>().minRonda <= ronda) break;
        }

        Transform loc;
        byte ris;
        while (false != true)
        {
            ris = (byte)Random.Range(0, spawners.Length); //RIS = Random Index para el array de Spawners™
            loc = spawners[ris].transform;
            bool enemyIsLarge = prefabElegido.GetComponent<EnemigoScript>().isLarge;
            if (enemyIsLarge)
            {
                string sn = spawners[ris].name;
                Debug.Log(sn);
                if (sn == "A5" || sn == "A8" || sn[1] == 'L') { Debug.LogWarning(sn); break; }
            }
            else if (loc.name[0] == 'A')
            {
                if (ronda >= 5) break;
            }
            else break;
        }

        GameObject nuevoEnemigo = Instantiate(prefabElegido, loc.position, Quaternion.identity);

        EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
        enemigoScript.spName = spawners[ris].name;

        botsASpawnear--;
        tiempoDesdeUltimoSpawn = 0;
    }

    public void TerminarRonda()
    {
        botsVivos.Clear();
        if (isBossFight) boss = null;
        ronda++;
        PauseScript.ronda = ronda;
        botsEliminadosRonda = 0;
        ToggleSpawning(false);
        tiempoDesdeUltimoSpawn = 0;

        Image btnPlayImage = btnIniciarRonda.GetComponent<Image>();
        btnPlayImage.sprite = btnPlaySprite1;

        Oscuro1.GetComponent<Image>().enabled = !Oscuro1.GetComponent<Image>().isActiveAndEnabled;
        Oscuro2.GetComponent<Image>().enabled = !Oscuro2.GetComponent<Image>().isActiveAndEnabled;

        foreach (GameObject flecha in flechas)
        {
            flecha.GetComponent<SpriteRenderer>().enabled = true;
            if (flecha == flechas[6] || flecha == flechas[7] || flecha == flechas[8])
            {
                if (ronda < 5) flecha.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        foreach (GameObject zona in GetComponent<ConstruirScriptGeneral>().consumiblesZonas)
        {
            zona.GetComponent<ZonaConsumiblesScript>().consumibleSeleccionado = null;
        }

        foreach (GameObject boton in GetComponent<scrBotones>().botonesConsumibles)
        {
            Image imagen = boton.GetComponent<Image>();
            imagen.sprite = GetComponent<scrBotones>().btTorretaSprite1;
        }
    }

    private void ToggleSpawning(bool t)
    {
        if (t == true) txtRonda.text = "";
        else txtRonda.text = $"Empezando RONDA {ronda}/30";
        spawnear = t;
    }

    public void ActivarConsumibles(bool estado)
    {
        Oscuro2.GetComponent<Image>().enabled = !estado;
        ConstruirScriptGeneral construirScript = GetComponent<ConstruirScriptGeneral>();

        foreach (GameObject zona in construirScript.consumiblesZonas)
        {
            zona.GetComponent<BoxCollider2D>().enabled = estado;
        }
    }
}