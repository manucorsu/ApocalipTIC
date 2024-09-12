using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConsumiblesScript : MonoBehaviour
{

    //Objetos

    public GameObject consumibleSeleccionado = null;
    public GameObject sceneScripts;
    public ConstruirScriptGeneral scrConstruir;
    public LayerMask zonasConsumibles;
    public GameObject zonaConsumible;
    public ZonaConsumiblesScript scrZona;

    public ParticleSystem palomas;

    //Variables 

    public float precioSeleccionado;
    private bool isPaused;
    private bool isPalomas = false;

    // Start is called before the first frame update
    void Awake()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        scrConstruir = sceneScripts.GetComponent<ConstruirScriptGeneral>();
        zonaConsumible = GameObject.Find("ConsumiblesZona1");
        scrZona = zonaConsumible.GetComponent<ZonaConsumiblesScript>();
        palomas = GameObject.Find("Palomas").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = PauseScript.isPaused;
        consumibleSeleccionado = scrZona.consumibleSeleccionado;
    }

    public void OnMouseDown()
    {
        if (consumibleSeleccionado != null && !isPaused)
        {
            if ((scrConstruir.plataActual - precioSeleccionado) >= 0)
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0f;
                if (consumibleSeleccionado.name != "Palomas") //Bidón / Pegamento
                {
                    Instantiate(consumibleSeleccionado, mouseWorldPos, Quaternion.identity);
                    scrConstruir.plataActual -= precioSeleccionado;
                } else //Palomas
                {
                    if (!isPalomas)
                    {
                        scrConstruir.plataActual -= precioSeleccionado;
                        StartCoroutine(Palomas());
                    }
                }
            }
        }
    }

    private IEnumerator Palomas()
    {
        isPalomas = true;
        palomas.Play();
        yield return new WaitForSeconds(1);

        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("enemigo");
        foreach(GameObject enemigo in enemigos)
        {
            if (enemigo != null)
            {
                EnemigoScript scrEnemigo = enemigo.GetComponent<EnemigoScript>();
                scrEnemigo.Sufrir(10);
            }
        }

        yield return new WaitForSeconds(1);

        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo != null)
            {
                EnemigoScript scrEnemigo = enemigo.GetComponent<EnemigoScript>();
                scrEnemigo.hp -= 10;
            }
        }

        yield return new WaitForSeconds(1);
        palomas.Stop();
        yield return new WaitForSeconds(1);
        isPalomas = false;
    }
}
