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

    //Variables

    public bool dv = false; // false si el botón de doble velocidad está inactivo, true si lo está


    private void Start()
    {
        textoMejoraTorreta = GameObject.Find("txtMejoraTorreta").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

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


        if(botones[torreta-1].tag == "botonTorreta")
        {
            foreach (GameObject boton in botonesTorretas)
            {
                Image imagen = boton.GetComponent<Image>();
                imagen.sprite = btTorretaSprite1;
            }

            Image imagen2 = botones[torreta-1].GetComponent<Image>();
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
        if (textoMejoraTorreta.text == "Tiralápices")
        {
            TorretaScript scrTorreta = torretaParaMejorar.GetComponent<TorretaScript>();
            if (boton == 1 && scrTorreta.nivel1 !=3) { scrTorreta.nivel1++; scrTorreta.bps++; }
            if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.5f; }
            if (boton == 3 && scrTorreta.nivel3 != 3) { scrTorreta.nivel3++; scrTorreta.dmg += 5; }
            MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
            scrMejora.Mejorar();
        }

        if (textoMejoraTorreta.text == "Nicho")
        {
            TorretaScript2 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript2>();
            if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++;  scrTorreta.chorroScale += 0.5f;  scrTorreta.bala.transform.position = new Vector2(scrTorreta.bala.transform.position.x - 0.5f, scrTorreta.bala.transform.position.y + 1);  }
            if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.dps += 5; }
            MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
            scrMejora.Mejorar();
        }

        if (textoMejoraTorreta.text == "Tacho")
        {
            TorretaScript3 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript3>();
            if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.cooldown -= 1; }
            if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rango += 0.5f; }
            MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
            scrMejora.Mejorar();
        }

        if (textoMejoraTorreta.text == "Proyector")
        {
            TorretaScript4 scrTorreta = torretaParaMejorar.GetComponent<TorretaScript4>();
            if (boton == 1 && scrTorreta.nivel1 != 3) { scrTorreta.nivel1++; scrTorreta.cooldown -= 0.5f; }
            if (boton == 2 && scrTorreta.nivel2 != 3) { scrTorreta.nivel2++; scrTorreta.rayoScale += 0.5f; if (scrTorreta.nivel2 == 3) { scrTorreta.bala.transform.position = new Vector2(scrTorreta.bala.transform.position.x, scrTorreta.bala.transform.position.y); } }
            MejorasScript scrMejora = torretaParaMejorar.GetComponent<MejorasScript>();
            scrMejora.Mejorar();
        }
    }

    public void CerrarCuadroMejora()
    {
        Image cuadroMejora = GameObject.Find("cuadroMejora").GetComponent<Image>();
        cuadroMejora.rectTransform.position = new Vector2(1000, 1000);
    }

}

