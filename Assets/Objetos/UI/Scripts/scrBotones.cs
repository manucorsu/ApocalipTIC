using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrBotones : MonoBehaviour
{
    //Objetos
    private ConstruirScript scrConstruir;
    private ZonaConsumiblesScript scrZonaConsumible;
    public GameObject[] tiles;
    public GameObject[] torretas;
    public GameObject cuadroTorreta;
    //public GameObject[] zonasConsumibles;
    public ZonaConsumiblesScript[] zonasConsumibles;
    public GameObject consumibleSeleccionado;
    public float consumibleSeleccionadoPrecio;

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
    public float precioParaVender;
    public GameObject tile;

    //Variables

    public static int dv = 0; // 0 si el botón de doble velocidad está inactivo, 1 si lo está, 2 si está en TRIPLE velocidad

    private void Start()
    {
        textoMejoraTorreta = GameObject.Find("txtMejoraTorreta").GetComponent<TMP_Text>();
        sceneScripts = GameObject.Find("SCENESCRIPTS");

        scrConstruirGeneral = sceneScripts.GetComponent<ConstruirScriptGeneral>();
    }

    private void Update()
    {
        zonasConsumibles = FindObjectsOfType<ZonaConsumiblesScript>();

        foreach (ZonaConsumiblesScript zona in zonasConsumibles)
        {
            scrZonaConsumible = zona;
            scrZonaConsumible.consumibleSeleccionado = consumibleSeleccionado;
            scrZonaConsumible.precioSeleccionado = consumibleSeleccionadoPrecio;
        }
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
    // 12:

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
            foreach (ZonaConsumiblesScript zona in zonasConsumibles)
            {
                scrZonaConsumible = zona;
                consumibleSeleccionado = torretas[9];

                BidónScript scrBidón = torretas[9].GetComponent<BidónScript>();
                consumibleSeleccionadoPrecio = scrBidón.precio;
            }
        }

        //PEGAMENTO
        if (torreta == 10)
        {
            foreach (ZonaConsumiblesScript zona in zonasConsumibles)
            {
                scrZonaConsumible = zona;
                consumibleSeleccionado = torretas[10];

                PegamentoScript scrPegamento = torretas[10].GetComponent<PegamentoScript>();
                consumibleSeleccionadoPrecio = scrPegamento.precio;
            }
        }

        //PALOMAS
        if (torreta == 11)
        {
            foreach (ZonaConsumiblesScript zona in zonasConsumibles)
            {
                scrZonaConsumible = zona;
                consumibleSeleccionado = torretas[11];

                scrPalomas scrpalomas = torretas[11].GetComponent<scrPalomas>();
                consumibleSeleccionadoPrecio = scrpalomas.precio;
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
    }

    public void DobleVeclocidad()
    {
        if (dv == 0)
        {
            Time.timeScale = 2.5f;
            dv = 1;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.sprite = btDvSprite2;
        }

        else if (dv == 1)
        {
            Time.timeScale = 5;
            dv = 2;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.color = Color.cyan;
        } else if (dv == 2)
        {
            Time.timeScale = 1;
            dv = 0;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.sprite = btDvSprite1;
            btDvImage.color = Color.white;
        }
    }

    public void BtnMejora(int boton)
    {
        //TIRALÁPICES / TIRALAPÍCERAS / LANZABOMBUCHAS

        if (textoMejoraTorreta.text == "Tiralápices" || textoMejoraTorreta.text == "Tiralapiceras" || textoMejoraTorreta.text == "Lanzabombuchas")
        {
            TorretaScript scrTorreta = torretaParaMejorar.GetComponent<TorretaScript>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.bps++; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.5f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 3 && scrTorreta.nivel3 != 3) { scrTorreta.nivel3++; scrTorreta.dmg += 5; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }

                
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
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.cooldown -= 0.2f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.dps += 5; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
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
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.cooldown -= 1; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.5f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
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
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.cooldown -= 0.4f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.5f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
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
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.bps+= 0.4f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.5f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 3 && scrTorreta.nivel3 != 3) { scrTorreta.nivel3++; scrTorreta.ganancia += 2; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }

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
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.bps += 0.3f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.4f; scrTorreta.ondaSize += 0.25f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 3 && scrTorreta.nivel3 != 3) { scrTorreta.nivel3++; scrTorreta.dmg += 5; scrTorreta.dmgBala += 5; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }

                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
                scrMejora.precioExtraParaVender += 100;
                scrMejora.Mejorar();
            }
        }


    }

    public void CerrarCuadroMejora()
    {
        Image cuadroMejora = GameObject.Find("cuadroMejora").GetComponent<Image>();
        cuadroMejora.rectTransform.position = new Vector2(10000, 10000);
    }

    public void Vender()
    {

        MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
        scrConstruirGeneral.plataActual += precioParaVender;
        scrMejora.tileParaRenovar.GetComponent<BoxCollider2D>().enabled = true;
        scrMejora.tileParaRenovar.GetComponent<SpriteRenderer>().enabled = true;
        CerrarCuadroMejora();
        Destroy(torretaParaMejorar);
    }
}

