﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static bool spawnear = false;
    public static bool isBossFight = false;

    [Header("Arrays")]
    [SerializeField] private GameObject prefabBoss;
    public GameObject[] pfbsEnemigos; //cada tipo de enemigo es un prefab y está en este array
    [SerializeField] private GameObject[] spawners;

    [Header("Sistema de rondas")]
    [SerializeField] private TMP_Text txtRonda;
    [SerializeField] private GameObject btnIniciarRonda; // No sé por qué no anda .enabled si es Button así que voy a usar SetActive 🤷
    [SerializeField] private float dificultad = 0.75f; //scaler de dificultad
    [SerializeField] private byte r1Bots = 6; //bots de la ronda 1, usados de base para todo el resto de las rondas
    [SerializeField] private float bps = 0.5f; //bots por segundo
    public static byte ronda = 1;
    private float tiempoDesdeUltimoSpawn;
    public static List<GameObject> botsVivos = new List<GameObject>();
    private byte botsASpawnear;

    [SerializeField] private Sprite btnPlaySprite1;
    [SerializeField] private Sprite btnPlaySprite2;

    [SerializeField] private Image Oscuro1;
    [SerializeField] private Image Oscuro2;
    [SerializeField] private GameObject[] flechas;

    [SerializeField] private List<GameObject> test;

    void Start()
    {
        foreach(GameObject flecha in flechas)
        {
            if (flecha == flechas[6] || flecha == flechas[7] || flecha == flechas[8])
            {
                flecha.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        botsVivos.Clear();
        ToggleSpawning(false);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B) && spawnear == false) // debug: cambiar a ronda 15
        {
            ronda = 15;
            txtRonda.text = "override ronda 15";
        }
#endif 
        if (spawnear == true)
        {
            if (!isBossFight)
            {
                tiempoDesdeUltimoSpawn += Time.deltaTime;

                if (tiempoDesdeUltimoSpawn >= (1f / bps) && botsASpawnear > 0)
                {
                    SpawnEnemy();
                }
            }
            if (botsVivos.Count <= 0 && botsASpawnear <= 0)
            {
                TerminarRonda();
            }
        }
        test = botsVivos;   
    }
    public void EmpezarRonda()
    {
        if (spawnear == false)
        {
            Image cuadroMejora = GameObject.Find("cuadroMejora").GetComponent<Image>();

            cuadroMejora.rectTransform.position = new Vector2(100000000, 100000000);

            Oscuro1.GetComponent<Image>().enabled = !Oscuro1.GetComponent<Image>().isActiveAndEnabled;
            Oscuro2.GetComponent<Image>().enabled = !Oscuro2.GetComponent<Image>().isActiveAndEnabled;

            if (ronda == 15 || ronda == 30)
            {
                isBossFight = true;
                Boss jefe = Instantiate(prefabBoss, new Vector3(14.5f, 0.5f, 0), Quaternion.identity).GetComponent<Boss>();
                ToggleSpawning(true); /* hay cosas que dependen de esta variable (aunque si es 
                                       * pelea de jefe no va a hacer nada en el update)*/
            }
            else
            {
                isBossFight = false;
                ToggleSpawning(true);
                botsASpawnear = (byte)Mathf.Round(r1Bots * Mathf.Pow(ronda, dificultad));
            }
            Image btnPlayImage = btnIniciarRonda.GetComponent<Image>();
            btnPlayImage.sprite = btnPlaySprite2;

            foreach(GameObject flecha in flechas)
            {
                flecha.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    private void SpawnEnemy()
    {
        if (!isBossFight)
        {
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
                if (loc.name[0] == 'A')
                {
                    if (ronda >= 5)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            GameObject nuevoEnemigo = Instantiate(prefabElegido, loc.position, Quaternion.identity);

            EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
            enemigoScript.spName = spawners[ris].name;

            botsASpawnear--;
            tiempoDesdeUltimoSpawn = 0;
        }
        else
        {
            Debug.Log("spawneando jefe");
            GameObject boss = Instantiate(prefabBoss, spawners[17].transform.position, Quaternion.identity); //spawners[17] = spawner C3.
            boss.GetComponent<Boss>().spName = spawners[17].name;
            botsVivos.Add(boss);
        }
    }

    public void TerminarRonda()
    {
        botsVivos.Clear();
        ronda++;
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
            else if(flecha != flechas[2] || flecha != flechas[3] || flecha != flechas[9])
            {
                if (ronda == 15 || ronda == 30) flecha.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void ToggleSpawning(bool t)
    {
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