using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Mon cher ami m'a forcé la main. Vive les bad coding practises.-- manucorsu
    private AudioSource palomasAudioSource;
    [SerializeField] private AudioClip palomasSfx; 

    //Variables 

    public float precioSeleccionado;
    private bool isPaused;
    private bool isPalomas = false;

    void Awake()
    {
        sceneScripts = GameObject.Find("SCENESCRIPTS");
        palomasAudioSource = sceneScripts.GetComponent<AudioSource>();
        if (palomasAudioSource == null)
        {
            throw new System.Exception("pena de muerte para el que le sacó el AudioSource a SCENESCRIPTS!");
        }
        scrConstruir = sceneScripts.GetComponent<ConstruirScriptGeneral>();
        zonaConsumible = GameObject.Find("ConsumiblesZona1");
        scrZona = zonaConsumible.GetComponent<ZonaConsumiblesScript>();
        palomas = GameObject.Find("Palomas").GetComponent<ParticleSystem>();
    }

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
        SoundManager.Instance.PlayBuySfx();
        yield return new WaitForSeconds(0.5f);
        palomas.Play();
        SoundManager.Instance.LoopSound(sceneScripts.GetComponent<AudioSource>(), palomasSfx, 1);    
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 10; i++)
        {
            GameObject[] enemigos = EnemySpawner.botsVivos.ToArray();
            foreach (GameObject enemigo in enemigos)
            {
                if (enemigo != null)
                {
                    EnemigoScript scrEnemigo = enemigo.GetComponent<EnemigoScript>();
                    Ninja ninja = enemigo.GetComponent<Ninja>();
                    if (ninja != null && ninja.Invisible) ninja.SetInvis(false, 0.5f);
                    scrEnemigo.Sufrir(10);
                }
            }

            yield return new WaitForSeconds(0.7f);
        }
        yield return new WaitForSeconds(1);
        palomas.Stop();
        SoundManager.Instance.StopSFXLoop(palomasAudioSource);
        yield return new WaitForSeconds(1);
        isPalomas = false;
    }
}
