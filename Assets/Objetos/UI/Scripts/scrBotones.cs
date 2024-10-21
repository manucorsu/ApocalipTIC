using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class scrBotones : MonoBehaviour
{
    [Header("Audio clips")]
    [SerializeField] private AudioClip sellSfx;
    [SerializeField] private AudioClip velSfx1;
    [SerializeField] private AudioClip velSfx2;
    [SerializeField] private AudioClip velSfx3;
    [SerializeField] private AudioClip upgradeSfx;
    [SerializeField] private AudioClip upgradeMaxSfx;

    [Header("Objetos")]
    private ConstruirScript scrConstruir;
    private ZonaConsumiblesScript scrZonaConsumible;
    public GameObject[] tiles;
    public GameObject[] torretas;
    public GameObject cuadroTorreta;
    public GameObject[] zonasConsumibles;
    public GameObject consumibleSeleccionado;

    public GameObject[] botones;
    public GameObject[] botonesTorretas;
    public GameObject[] botonesConsumibles;

    public GameObject btDv;
    public Sprite btDvSprite1;
    public Sprite btDvSprite2;

    public GameObject btPlay;
    public Sprite btPlaySprite1;
    public Sprite btPlaySprite2;

    public Sprite btTorretaSprite1;
    public Sprite btTorretaSprite2;

    public TMP_Text textoMejoraTorreta;
    public GameObject torretaParaMejorar;

    public GameObject sceneScripts;
    private ConstruirScriptGeneral scrConstruirGeneral;
    public GameObject tile;

    public TutorialData[] tutorialData;

    Color[] coloresBotones;


    [Header("Variables")]

    public static int dv = 0; // 0 si el botón de doble velocidad está inactivo, 1 si lo está, 2 si está en TRIPLE velocidad
    public float precioParaVender;
    public int pasoTutorial = 0;

    private void Start()
    {
        textoMejoraTorreta = GameObject.Find("txtMejoraTorreta").GetComponent<TMP_Text>();
        sceneScripts = GameObject.Find("SCENESCRIPTS");

        scrConstruirGeneral = sceneScripts.GetComponent<ConstruirScriptGeneral>();
    }

    private void Update()
    {

    }



    // VALOR DE CADA VARIABLE:
    //
    // 1: TIRALÁPICES
    // 2: NICHO
    // 3: TACHO
    // 4: PROYECTOR
    // 5: TIRALAPICERAS
    // 6: LANZABOMBUCHAS
    // 7: IMÁN
    // 8: PARLANTE
    // 9: BIDÓN
    // 10: PEGAMENTO 
    // 11: PALOMAS
    // 12: NETBOOK

    public void Click(int torreta)
    {

        //TIRALÁPICES
        if (torreta == 1)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[0];

                    TorretaScript scrTiralápices = torretas[0].GetComponent<TorretaScript>();
                    scrConstruir.precioSeleccionado = scrTiralápices.precio;

                }
            }


        }

        //NICHO
        if (torreta == 2)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[1];

                    TorretaScript2 scrNicho = torretas[1].GetComponent<TorretaScript2>();
                    scrConstruir.precioSeleccionado = scrNicho.precio;
                }
            }
        }

        //TACHO
        if (torreta == 3)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[2];
                    TorretaScript3 scrTacho = torretas[2].GetComponent<TorretaScript3>();
                    scrConstruir.precioSeleccionado = scrTacho.precio;
                }
            }
        }

        //PROYECTOR
        if (torreta == 4)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[3];

                    TorretaScript4 scrProyector = torretas[3].GetComponent<TorretaScript4>();
                    scrConstruir.precioSeleccionado = scrProyector.precio;
                }
            }
        }

        //TIRALAPICERAS
        if (torreta == 5)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[4];

                    TorretaScript scrTiralapiceras = torretas[4].GetComponent<TorretaScript>();
                    scrConstruir.precioSeleccionado = scrTiralapiceras.precio;
                }
            }
        }

        //LANZABOMBUCHAS
        if (torreta == 6)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[5];

                    TorretaScript scrLanzabombuchas = torretas[5].GetComponent<TorretaScript>();
                    scrConstruir.precioSeleccionado = scrLanzabombuchas.precio;
                }
            }
        }

        //IMÁN
        if (torreta == 7)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[6];

                    ImanScript scrImán = torretas[6].GetComponent<ImanScript>();
                    scrConstruir.precioSeleccionado = scrImán.precio;
                }
            }
        }

        //PARLANTE
        if (torreta == 8)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[7];

                    ParlanteScript scrParlante = torretas[7].GetComponent<ParlanteScript>();
                    scrConstruir.precioSeleccionado = scrParlante.precio;
                }
            }
        }


        //BIDÓN
        if (torreta == 9)
        {
            foreach (GameObject zona in zonasConsumibles)
            {
                scrZonaConsumible = zona.GetComponent<ZonaConsumiblesScript>();
                scrZonaConsumible.consumibleSeleccionado = torretas[9];

                BidónScript scrBidón = torretas[9].GetComponent<BidónScript>();
                scrZonaConsumible.precioSeleccionado = scrBidón.precio;
            }
        }

        //PEGAMENTO
        if (torreta == 10)
        {
            foreach (GameObject zona in zonasConsumibles)
            {
                scrZonaConsumible = zona.GetComponent<ZonaConsumiblesScript>();
                scrZonaConsumible.consumibleSeleccionado = torretas[10];

                PegamentoScript scrPegamento = torretas[10].GetComponent<PegamentoScript>();
                scrZonaConsumible.precioSeleccionado = scrPegamento.precio;
            }
        }

        //PALOMAS
        if (torreta == 11)
        {
            foreach (GameObject zona in zonasConsumibles)
            {
                scrZonaConsumible = zona.GetComponent<ZonaConsumiblesScript>();
                scrZonaConsumible.consumibleSeleccionado = torretas[11];

                scrPalomas scrPaloma = torretas[11].GetComponent<scrPalomas>();
                scrZonaConsumible.precioSeleccionado = scrPaloma.precio;
            }
        }

        //NETBOOK
        if (torreta == 12)
        {
            foreach (GameObject zona in zonasConsumibles)
            {
                scrZonaConsumible = zona.GetComponent<ZonaConsumiblesScript>();
                scrZonaConsumible.consumibleSeleccionado = torretas[12];

                ElectricosaScript scrNetbook = torretas[12].GetComponent<ElectricosaScript>();
                scrZonaConsumible.precioSeleccionado = scrNetbook.precio;
            }
        }




        if (botones[torreta - 1].tag == "botonTorreta")
        {
            foreach (GameObject boton in botonesTorretas)
            {
                Image imagen = boton.GetComponent<Image>();
                imagen.sprite = btTorretaSprite1;
            }

            Image imagen2 = botones[torreta - 1].GetComponent<Image>();
            imagen2.sprite = btTorretaSprite2;

        }

        if (botones[torreta - 1].tag == "botonConsumible")
        {
            foreach (GameObject boton in botonesConsumibles)
            {
                Image imagen = boton.GetComponent<Image>();
                imagen.sprite = btTorretaSprite1;
            }

            Image imagen2 = botones[torreta - 1].GetComponent<Image>();
            imagen2.sprite = btTorretaSprite2;

        }


        if (pasoTutorial == 3)
        {
            tutoBotón();
        }

        SoundManager.instance.PlayUIClick();
    }

    public void DobleVeclocidad()//veclocidad
    {
        if (pasoTutorial == 11)
        {
            return;
        }

        if (dv == 0)
        {
            SoundManager.instance.PlaySound(velSfx2);
            Time.timeScale = 2.5f;
            dv = 1;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.sprite = btDvSprite2;
        }

        else if (dv == 1)
        {
            SoundManager.instance.PlaySound(velSfx3);
            Time.timeScale = 5;
            dv = 2;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.color = Color.cyan;
        }
        else if (dv == 2)
        {
            SoundManager.instance.PlaySound(velSfx1);
            Time.timeScale = 1;
            dv = 0;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.sprite = btDvSprite1;
            btDvImage.color = Color.white;
        }
    }

    public void BtnMejora(int boton)
    {
        if (pasoTutorial == 7 || pasoTutorial == 8)
        {
            return;
        }

        //TIRALÁPICES / TIRALAPÍCERAS / LANZABOMBUCHAS

        if (textoMejoraTorreta.text == "Tiralápices" || textoMejoraTorreta.text == "Tiralapiceras" || textoMejoraTorreta.text == "Lanzabombuchas")
        {
            TorretaScript scrTorreta = torretaParaMejorar.GetComponent<TorretaScript>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3)
                {
                    scrTorreta.nivel1++;
                    if (scrTorreta.nivel1 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.bps++;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 2 && scrTorreta.nivel2 != 3)
                {
                    scrTorreta.nivel2++;
                    if (scrTorreta.nivel2 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.rango += 0.5f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 3 && scrTorreta.nivel3 != 3)
                {
                    scrTorreta.nivel3++;
                    if (scrTorreta.nivel3 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.dmg += 5;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }

                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
                scrMejora.precioExtraParaVender += 100;
                scrMejora.Mejorar();
            }
        }

        //NICHO

        if (textoMejoraTorreta.text == "Nicho")
        {
            TorretaScript2 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript2>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3)
                {
                    scrTorreta.nivel1++;
                    if (scrTorreta.nivel1 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.cooldown -= 0.2f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 2 && scrTorreta.nivel2 != 3)
                {
                    scrTorreta.nivel2++;
                    if (scrTorreta.nivel2 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.dps += 5;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
                scrMejora.precioExtraParaVender += 100;
                scrMejora.Mejorar();
            }
        }

        //TACHO

        if (textoMejoraTorreta.text == "Tacho")
        {
            TorretaScript3 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript3>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3)
                {
                    scrTorreta.nivel1++;
                    if (scrTorreta.nivel1 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.cooldown -= 1; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 2 && scrTorreta.nivel2 != 3)
                {
                    scrTorreta.nivel2++;
                    if (scrTorreta.nivel2 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.rango += 0.5f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
                scrMejora.precioExtraParaVender += 100;
                scrMejora.Mejorar();
            }
        }

        //PROYECTOR

        if (textoMejoraTorreta.text == "Proyector")
        {
            TorretaScript4 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript4>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3)
                {
                    scrTorreta.nivel1++;
                    if (scrTorreta.nivel1 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.cooldown -= 0.4f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 2 && scrTorreta.nivel2 != 3)
                {
                    scrTorreta.nivel2++;
                    if (scrTorreta.nivel2 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.rango += 0.5f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
                scrMejora.precioExtraParaVender += 100;
                scrMejora.Mejorar();
            }
        }

        //IMÁN

        if (textoMejoraTorreta.text == "Imán")
        {
            ImanScript scrTorreta = torretaParaMejorar.GetComponent<ImanScript>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3)
                {
                    scrTorreta.nivel1++;
                    if (scrTorreta.nivel1 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.bps += 0.4f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 2 && scrTorreta.nivel2 != 3)
                {
                    scrTorreta.nivel2++;
                    if (scrTorreta.nivel2 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.rango += 0.5f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 3 && scrTorreta.nivel3 != 3)
                {
                    scrTorreta.nivel3++;
                    if (scrTorreta.nivel3 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.ganancia += 2; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }

                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
                scrMejora.precioExtraParaVender += 100;
                scrMejora.Mejorar();
            }
        }

        //PARLANTE

        if (textoMejoraTorreta.text == "Parlante")
        {
            ParlanteScript scrTorreta = torretaParaMejorar.GetComponent<ParlanteScript>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3)
                {
                    scrTorreta.nivel1++;
                    if (scrTorreta.nivel1 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.bps += 0.3f;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }
                if (boton == 2 && scrTorreta.nivel2 != 3)
                {
                    scrTorreta.nivel2++;
                    if (scrTorreta.nivel2 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.rango += 0.4f; scrTorreta.ondaSize += 0.25f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100;
                }
                if (boton == 3 && scrTorreta.nivel3 != 3)
                {
                    scrTorreta.nivel3++;
                    if (scrTorreta.nivel3 == 3) SoundManager.instance.PlaySound(upgradeMaxSfx, 0.5f);
                    else SoundManager.instance.PlaySound(upgradeSfx, 5);
                    scrTorreta.dmg += 5;
                    scrTorreta.dmgBala += 5;
                    scrConstruirGeneral.plataActual -= scrTorreta.precioMejora;
                    scrTorreta.precioMejora += 100;
                }

                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
                scrMejora.precioExtraParaVender += 100;
                scrMejora.Mejorar();
            }
        }
    }

    public void CerrarCuadroMejora(bool esBotón)
    {
        if (esBotón)
        {
            if (pasoTutorial == 7 || pasoTutorial == 8)
            {
                return;
            }
        }

        Image cuadroMejora = GameObject.Find("cuadroMejora").GetComponent<Image>();
        cuadroMejora.rectTransform.position = new Vector2(10000, 10000);
    }

    public void Vender()
    {
        if (pasoTutorial == 7)
        {
            return;
        }

        MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
        scrConstruirGeneral.plataActual += precioParaVender;
        scrMejora.tileParaRenovar.GetComponent<BoxCollider2D>().enabled = true;
        scrMejora.tileParaRenovar.GetComponent<SpriteRenderer>().enabled = true;
        CerrarCuadroMejora(false);
        Destroy(torretaParaMejorar);
        SoundManager.instance.PlaySound(sellSfx, volume: 2);
        if (pasoTutorial == 8)
        {
            tutoBotón();

            foreach (GameObject tile in GetComponent<ConstruirScriptGeneral>().tiles)
            {
                if (tile != null)
                {
                    tile.GetComponent<BoxCollider2D>().enabled = true;
                    tile.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            Image botonSí = GameObject.Find("btnSí").GetComponent<Image>();
            botonSí.rectTransform.anchoredPosition = new Vector2(botonSí.rectTransform.anchoredPosition.x - 1000, botonSí.rectTransform.anchoredPosition.y);
        }
    }

    public void tutoBotón()
    {
        if (pasoTutorial != 12)
        {
            TutorialData tutoData = tutorialData[pasoTutorial];
            foreach (string cuadro in tutoData.cuadroOcultar)
            {
                GameObject.Find(cuadro).GetComponent<Image>().enabled = false;
            }
            foreach (string cuadro in tutoData.cuadroMostrar)
            {
                GameObject.Find(cuadro).GetComponent<Image>().enabled = true;
            }
            GameObject.Find("txtTutorial").GetComponent<TMP_Text>().text = tutoData.texto;
            GameObject.Find("cuadroTutorial").GetComponent<Image>().rectTransform.anchoredPosition = tutoData.posicionCuadro;

            pasoTutorial++;
        }
        else
        {
            CerrarTutorial();
        }
        try { SoundManager.instance.PlayUIClick(); }
        catch (NullReferenceException)
        {
            Debug.LogError("NullReferenceExeception: El singleton de SoundManager fue null.\n" +
                        "NUNCA inicies Game directamente, siempre pasá por Inicio primero.");
        }
    }

    public void SiTuto()
    {
        if (pasoTutorial == 1)
        {
            GameObject.Find("btnNo").SetActive(false);
            Image botonSí = GameObject.Find("btnSí").GetComponent<Image>();
            botonSí.rectTransform.anchoredPosition = new Vector2(botonSí.rectTransform.anchoredPosition.x - 60, botonSí.rectTransform.anchoredPosition.y);
            GameObject.Find("txtSí").GetComponent<TMP_Text>().text = "OK";
        }

        if (pasoTutorial == 3 || pasoTutorial == 6 || pasoTutorial == 8)
        {
            Image botonSí = GameObject.Find("btnSí").GetComponent<Image>();
            botonSí.rectTransform.anchoredPosition = new Vector2(botonSí.rectTransform.anchoredPosition.x + 1000, botonSí.rectTransform.anchoredPosition.y);
        }


    }

    public void CerrarTutorial()
    {
        GameObject.Find("cuadroTutorial").GetComponent<Image>().enabled = false;
        GameObject.Find("txtTutorial").GetComponent<TMP_Text>().enabled = false;
        GameObject.Find("btnSí").GetComponent<Image>().enabled = false;
        GameObject.Find("txtSí").GetComponent<TMP_Text>().enabled = false;
        if (GameObject.Find("txtNo") != null) { GameObject.Find("txtNo").GetComponent<TMP_Text>().enabled = false; }

        GameObject.Find("Bloqueo (1)").GetComponent<Image>().enabled = false;
        GameObject.Find("Bloqueo (2)").GetComponent<Image>().enabled = false;
        GameObject.Find("Bloqueo (3)").GetComponent<Image>().enabled = false;
        GameObject.Find("Oscuro 1").GetComponent<Image>().enabled = false;
        GameObject.Find("Oscuro 2").GetComponent<Image>().enabled = true;
        if (GameObject.Find("txtNo") != null) { GameObject.Find("btnNo").GetComponent<Image>().enabled = false; }

        GetComponent<ConstruirScriptGeneral>().plataActual = 1000;

        GameObject[] flechas = sceneScripts.GetComponent<EnemySpawner>().flechas;
        foreach (GameObject flecha in flechas)
        {
            flecha.GetComponent<SpriteRenderer>().enabled = true;
            if (flecha == flechas[6] || flecha == flechas[7] || flecha == flechas[8])
            {
                if (EnemySpawner.ronda < 5) flecha.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        try { SoundManager.instance.PlayUIClick(); }
        catch (NullReferenceException)
        {
            Debug.LogError("NullReferenceExeception: El singleton de SoundManager fue null.\n" +
                        "NUNCA inicies Game directamente, siempre pasá por Inicio primero.");
        }
    }
}

