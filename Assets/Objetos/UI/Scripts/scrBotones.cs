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
    public GameObject[] zonasConsumibles;

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

    public static bool dv = false; // false si el botón de doble velocidad está inactivo, true si lo está

    private void Start()
    {
        textoMejoraTorreta = GameObject.Find("txtMejoraTorreta").GetComponent<TMP_Text>();
        sceneScripts = GameObject.Find("SCENESCRIPTS");

        scrConstruirGeneral = sceneScripts.GetComponent<ConstruirScriptGeneral>();
    }

    // VALOR DE CADA VARIABLE:
    //
    // 1: TIRALÁPICES
    // 2: NICHO
    // 3: TACHO
    // 4: PROYECTOR
    // 5: 
    // 6: 
    // 7: 
    // 8: 
    // 9: BIDÓN
    // 10: PEGAMENTO 
    // 11:
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
        if (dv == false)
        {
            Time.timeScale = 2;
            dv = true;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.sprite = btDvSprite2;
        }

        else if (dv == true)
        {
            Time.timeScale = 1;
            dv = false;
            Image btDvImage = btDv.GetComponent<Image>();
            btDvImage.sprite = btDvSprite1;
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
                scrMejora.Mejorar();
            }
        }

        //NICHO

        if (textoMejoraTorreta.text == "Nicho")
        {
            TorretaScript2 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript2>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.cooldown -= 0.25f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.dps += 5; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
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
                scrMejora.Mejorar();
            }
        }

        //PROYECTOR

        if (textoMejoraTorreta.text == "Proyector")
        {
            TorretaScript4 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript4>();

            if ((scrConstruirGeneral.plataActual - scrTorreta.precioMejora) >= 0)
            {
                if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.cooldown -= 0.5f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.5f; scrConstruirGeneral.plataActual -= scrTorreta.precioMejora; scrTorreta.precioMejora += 100; }
                MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
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

        scrConstruirGeneral.plataActual += precioParaVender;
        MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
        scrMejora.tileParaRenovar.GetComponent<BoxCollider2D>().enabled = true;
        scrMejora.tileParaRenovar.GetComponent<SpriteRenderer>().enabled = true;
        CerrarCuadroMejora();
        Destroy(torretaParaMejorar);
    }
}

