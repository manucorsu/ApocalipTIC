using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TornilloScript : MonoBehaviour
{
    public GameObject torreta;
    public GameObject puntaTorreta;
    private int rotativo;
    public int ganancia;
    private int plata;

    private void Start()
    {
        rotativo = Random.Range(-2, 2);
        plata = Random.Range(1 + ganancia, 6 + ganancia);
    }

    private void Update()
    { 
        
        transform.position = Vector2.MoveTowards(transform.position, torreta.transform.position, 0.1f);
        transform.Rotate(new Vector3(0, 0, rotativo), 200 * Time.deltaTime);

        if (transform.position == torreta.transform.position)
        {
            StartCoroutine(Guita());
        }
    }

    private IEnumerator Guita()
    {
        ConstruirScriptGeneral construirScript = GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>();
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        SpriteRenderer sr = puntaTorreta.GetComponent<SpriteRenderer>();
        sr.color = Color.yellow;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;
        construirScript.plataActual += plata;
        Destroy(gameObject);
    }
}
