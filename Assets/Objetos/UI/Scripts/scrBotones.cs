using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBotones : MonoBehaviour
{

    //Objetos

    private ConstruirScript scrConstruir;
    private ZonaConsumiblesScript scrZonaConsumible;
    public GameObject[] tiles;
    public GameObject[] torretas;
    public GameObject cuadroTorreta;
    public GameObject[] zonasConsumibles;

    //Variables

    public bool dv = false;

    // Start is called before the first frame update
    void Start()
    {

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
    }

    public void DobleVeclocidad()
    {

        if (dv == false)
        {
            Time.timeScale = 2;
            dv = true;
        }

        else
        {
            Time.timeScale = 1;
            dv = false;
        }
    }

}

