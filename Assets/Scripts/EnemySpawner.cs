using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public bool spawnear = false;

    [Header("Arrays")]
    [SerializeField] private GameObject[] pfbsEnemigos; //cada tipo de enemigo es un prefab y está en este array
    [SerializeField] private GameObject[] spawners;

    [Header("Sistema de rondas")]
    [SerializeField] private GameObject btnIniciarRonda;
    [SerializeField] private Text txtRonda;
    [SerializeField] private float dificultad = 0.75f; //scaler de dificultad
    [SerializeField] private byte r1Bots = 6; //bots de la ronda 1, usados de base para todo el resto de las rondas
    [SerializeField] private float bps = 0.5f; //bots por segundo
    private byte ronda = 1;
    private float tiempoDesdeUltimoSpawn;
    public byte botsVivos;
    private byte botsASpawnear;

    void Start()
    {
        ToggleSpawning(false);
    }

    void Update()
    {
        if (spawnear == true)
        {
            tiempoDesdeUltimoSpawn += Time.deltaTime;

            if (tiempoDesdeUltimoSpawn >= (1f / bps) && botsASpawnear > 0)
            {
                SpawnEnemy();
            }
            if (botsVivos == 0 && botsASpawnear == 0)
            {
                TerminarRonda();
            }
        }
    }
    public void EmpezarRonda()
    {
        ToggleSpawning(true);
        botsASpawnear = (byte)Mathf.Round(r1Bots * Mathf.Pow(ronda, dificultad));
    }
    private void SpawnEnemy()
    {
        botsASpawnear--;
        botsVivos++;
        tiempoDesdeUltimoSpawn = 0;

        byte rie = (byte)Random.Range(0, pfbsEnemigos.Length); //RIE = Random Index para el array de Enemigos™
        Debug.Log(rie);
        GameObject prefabElegido = pfbsEnemigos[rie];

        byte ris = (byte)Random.Range(0, spawners.Length); //RIS = Random Index para el array de Spawners™
        Transform loc = spawners[ris].transform;

        GameObject nuevoEnemigo = Instantiate(prefabElegido, loc.position, Quaternion.identity);

        EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
        enemigoScript.spName = spawners[ris].name;
    }

    private void TerminarRonda()
    {
        ronda++;
        ToggleSpawning(false);
        tiempoDesdeUltimoSpawn = 0;
    }

    private void ToggleSpawning(bool t)
    {
        btnIniciarRonda.SetActive(!t);
        if (t == true)
        {
            txtRonda.text = "";
        }
        else
        {
            txtRonda.text = $"Empezando RONDA {ronda}/30";
        }
        spawnear = t;
    }
}