using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZonaConsumiblesScript : MonoBehaviour
{

    [SerializeField] private AudioClip buySfx;
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
        isPaused = PauseScript.Instance.IsPaused;
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
                    try { SoundManager.instance.PlaySound(buySfx, 0.5f); }
                    catch (System.NullReferenceException)
                    {
                        Debug.LogError("NullReferenceExeception: El singleton de SoundManager fue null.\n" +
                            "NUNCA inicies Game directamente, siempre pasá por Inicio primero.");
                    }
                    scrConstruir.plataActual -= precioSeleccionado;
                }
                else //Palomas
                {
                    if (!isPalomas)
                    {
                        scrConstruir.plataActual -= precioSeleccionado;
                        StartCoroutine(Palomas());
                    }
                }
            }


            foreach (GameObject boton in GameObject.Find("SCENESCRIPTS").GetComponent<scrBotones>().botones)
            {
                if (GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>().plataActual - boton.GetComponent<scrBotonTorreta>().precio < 0)
                {
                    Image imagen = boton.GetComponent<Image>();
                    imagen.sprite = GameObject.Find("SCENESCRIPTS").GetComponent<scrBotones>().btTorretaSprite1;
                }
            }
        }
    }

    private IEnumerator Palomas()
    {
        isPalomas = true;
        palomas.Play();
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 10; i++)
        {
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("enemigo");
            foreach (GameObject enemigo in enemigos)
            {
                if (enemigo != null)
                {
                    EnemigoScript scrEnemigo = enemigo.GetComponent<EnemigoScript>();
                    scrEnemigo.Sufrir(10);
                }
            }

            yield return new WaitForSeconds(0.7f);
        }
        yield return new WaitForSeconds(1);
        palomas.Stop();
        yield return new WaitForSeconds(1);
        isPalomas = false;
    }
}
