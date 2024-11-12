using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    #region las estáticas
    public static byte vidas = 3;
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
    [SerializeField] private CustomRangeFloat dificultad = new CustomRangeFloat(0, 1.114f, 1); //scaler de dificultad
    [SerializeField] private byte r1Bots = 6; //bots de la ronda 1, usados de base para todo el resto de las rondas
    [SerializeField] private float bps = 0.5f; //bots por segundo
    private float tiempoDesdeUltimoSpawn;
    private Boss boss;

    [SerializeField] private Sprite btnPlaySprite1;
    [SerializeField] private Sprite btnPlaySprite2;

    [SerializeField] private Image Oscuro1;
    [SerializeField] private Image Oscuro2;
    public GameObject[] flechas;
    private Transform lastLoc;

    public AudioClip[] musica;

    private readonly List<object> largeSps = new List<object> { "A5", "A8", 'L' };

    void Start()
    {
        ToggleSpawning(false);
        vidas = 3;
        isBossFight = false;
        txtRonda.text = "Empezando RONDA 1/30";
        ronda = 1;
        botsVivos.Clear();
        botsASpawnear = 0;
        botsEliminados = 0;
        botsEliminadosRonda = 0;
    }

    void Update()
    {
        if (MessageBox.Instance.CheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) && !spawnear)
            {
                ronda--;
                if (ronda < 1) ronda = 1;
                txtRonda.text = $"override ronda {ronda}";
                PauseScript.Instance.botsRonda = EnemyFormula();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) && !spawnear)
            {
                ronda++;
                if (ronda > 30) ronda = 30;
                txtRonda.text = $"override ronda {ronda}";
                PauseScript.Instance.botsRonda = EnemyFormula();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ConstruirScriptGeneral construirscr = GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>();
                construirscr.plataActual += 1000;
            }
        }
        if (spawnear)
        {
            tiempoDesdeUltimoSpawn += Time.deltaTime;
            if (tiempoDesdeUltimoSpawn >= (1f / bps) && ((!isBossFight && botsASpawnear > 0) || isBossFight)) SpawnEnemy();
            if (botsVivos.Count <= 0 && botsASpawnear <= 0 && !isBossFight) TerminarRonda();
        }
    }

    public void EmpezarRonda()
    {
        if (GetComponent<scrBotones>().pasoTutorial == 11) return;
        if (spawnear == false)
        {
            SoundManager.Instance.PlayUIClick();
            Image cuadroMejora = GameObject.Find("cuadroMejora").GetComponent<Image>();

            cuadroMejora.rectTransform.position = new Vector2(100000000, 100000000); //jaja

            Oscuro1.GetComponent<Image>().enabled = !Oscuro1.GetComponent<Image>().isActiveAndEnabled;
            Oscuro2.GetComponent<Image>().enabled = !Oscuro2.GetComponent<Image>().isActiveAndEnabled;

            if (ronda == 15 || ronda == 30)
            {
                isBossFight = true;
                SoundManager.Instance.GetComponent<AudioSource>().clip = musica[1];
                SoundManager.Instance.GetComponent<AudioSource>().Play();
                boss = Instantiate(prefabBoss, new Vector3(14.5f, 0.5f, 0), Quaternion.identity).GetComponent<Boss>();
            }
            else
            {
                isBossFight = false;
            }
            if (ronda > 5) bps += 0.05f;
            botsASpawnear = EnemyFormula();
            ToggleSpawning(true);
            Image btnPlayImage = btnIniciarRonda.GetComponent<Image>();
            btnPlayImage.sprite = btnPlaySprite2;

            foreach (GameObject flecha in flechas)
            {
                flecha.GetComponent<SpriteRenderer>().enabled = false;
            }

            foreach (GameObject tile in GetComponent<ConstruirScriptGeneral>().tiles)
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

    private byte EnemyFormula()
    //https://www.desmos.com/calculator/hxfiurzdve
    {
        if (ronda < 1 || ronda > 30)
        {
            throw new System.ArgumentOutOfRangeException("ronda", "ronda debe ser un número entre 1 y 30 incl.");
        }
        ulong res = (ulong)Mathf.Round(r1Bots * Mathf.Pow(ronda, dificultad));
        if (res < 1 || res > 255)
        {
            throw new System.ArgumentOutOfRangeException("r1 y/o d", "r1 y/o d son muy grandes o muy chicos porque la cantidad de bots dio negativa o mayor a 255.");
        }
        else return (byte)res;
    }

    public static void PrintArr(string[] arr, string arrName = "")
    {
        string str = "[" + string.Join(", ", arr) + "]";

        if (arrName == "") Debug.Log(str);
        else Debug.Log($"{arrName}: {str}");
    }

    private void SpawnEnemy()
    {
        if (boss != null && boss.introDone == false) return;
        GameObject prefabElegido;
        while (false != true)
        {
            byte rie = (byte)Random.Range(0, pfbsEnemigos.Length); //RIE = Random Index para el array de Enemigos™
            prefabElegido = pfbsEnemigos[rie];
            if (prefabElegido.GetComponent<EnemigoScript>().minRonda <= ronda)
            {
                break;
            }
        }

        Transform loc;
        byte ris;
        while (false != true)
        {
            ris = (byte)Random.Range(0, spawners.Length); //RIS = Random Index para el array de Spawners™
            loc = spawners[ris].transform;
            if (ronda >= 5 || loc.name[0] != 'A')
            {
                if (!prefabElegido.GetComponent<EnemigoScript>().isLarge)
                {
                    if (loc.name[1] != 'L') break;
                }
                else
                {
                    if (largeSps.Contains(loc.name) || largeSps.Contains(loc.name[0])) break;
                }
            }
        }

        lastLoc = loc;

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
        PauseScript.Instance.botsRonda = EnemyFormula();
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

        if (ronda == 16)
        {
            SoundManager.Instance.GetComponent<AudioSource>().clip = musica[0];
            SoundManager.Instance.GetComponent<AudioSource>().Play();
        }

        //switch (ronda)
        //{
        //    case 15:
        //        MessageBox.Instance.Show("Cuidado...",
        //            "Felicitaciones por llegar a la Ronda 15. Como tal vez te esperabas porque llegaste a la mitad del juego, en esta ronda tendrás tu primer encuentro con el <b><color=red>Jefe</color></b>. Mucha suerte.");
        //        break;
        //    case 30:
        //        MessageBox.Instance.Show("(15 * 2) = 30", "El <b><color=red>Jefe</color></b> ha vuelto, y está el doble de enojado que antes. Si lográs derrotarlo, por fin podrás poner un fin a la invasión y salvar el L4. Mucha suerte.");
        //        break;
        //}
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