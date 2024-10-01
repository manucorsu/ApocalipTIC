using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ElectricosaScript : MonoBehaviour
{
    public float duracion;
    public float rango;
    private bool isWorking = false;
    public float yPosition;

    public GameObject zonaNetbook;
    private GameObject zona;
    RaycastHit2D[] hits;
    public GameObject aura;
    private List<GameObject> aurasGeneradas = new List<GameObject>();
    

    public float precio;

    private void Start()
    {
        yPosition = transform.position.y;
        transform.position = new Vector2(transform.position.x, 6.5f);
    }

    private void Update()
    {
        FindTarget();

        if (transform.position.y <= yPosition)
        {
            if (GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                zona = Instantiate(zonaNetbook, transform.position, Quaternion.identity);
                isWorking = true;
                GetComponent<Animator>().SetTrigger("anim");
                StartCoroutine(Duracion());
            }
        }
    }


    private void FindTarget()
    {
        hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f);

        foreach(RaycastHit2D torreta in hits)
        {
            if (torreta.collider.gameObject.tag == "torreta")
            {
                GameObject torretaMejorada = torreta.collider.gameObject;

                if (torretaMejorada.GetComponent<MejorasScript>() != null)
                {
                    if (torretaMejorada.GetComponent<MejorasScript>().isPotenciado == false && isWorking == true)
                    {
                        aurasGeneradas.Add(Instantiate(aura, torretaMejorada.transform.position, Quaternion.identity));

                        if (torretaMejorada.GetComponent<TorretaScript>() != null) { torretaMejorada.GetComponent<TorretaScript>().dmg++; torretaMejorada.GetComponent<TorretaScript>().rango += 0.5f; torretaMejorada.GetComponent<TorretaScript>().bps += 0.5f; }
                        if (torretaMejorada.GetComponent<TorretaScript2>() != null) { torretaMejorada.GetComponent<TorretaScript2>().dps++; torretaMejorada.GetComponent<TorretaScript2>().cooldown -= 0.2f; }
                        if (torretaMejorada.GetComponent<TorretaScript3>() != null) { torretaMejorada.GetComponent<TorretaScript3>().cooldown--; torretaMejorada.GetComponent<TorretaScript3>().rango += 0.5f; }
                        if (torretaMejorada.GetComponent<TorretaScript4>() != null) { torretaMejorada.GetComponent<TorretaScript4>().cooldown -= 0.4f; torretaMejorada.GetComponent<TorretaScript4>().rango += 0.5f; }
                        if (torretaMejorada.GetComponent<ImanScript>() != null) { torretaMejorada.GetComponent<ImanScript>().bps += 0.5f; torretaMejorada.GetComponent<ImanScript>().rango += 0.5f; torretaMejorada.GetComponent<ImanScript>().ganancia++; }
                        if (torretaMejorada.GetComponent<ParlanteScript>() != null) { torretaMejorada.GetComponent<ParlanteScript>().dmg++; torretaMejorada.GetComponent<ParlanteScript>().dmgBala++; torretaMejorada.GetComponent<ParlanteScript>().rango += 0.5f; torretaMejorada.GetComponent<ParlanteScript>().ondaSize += 0.5f; torretaMejorada.GetComponent<ParlanteScript>().bps += 0.5f; }

                        torretaMejorada.GetComponent<MejorasScript>().isPotenciado = true;
                    }
                }
            }
        }
        
    }

    public IEnumerator Duracion()
    {

        yield return new WaitForSeconds(duracion); 


        while(zona.transform.localScale.x > 0)
        {
            zona.transform.localScale = new Vector2(zona.transform.localScale.x - 0.1f, zona.transform.localScale.y - 0.1f);
            yield return null;
        }
        Destroy(zona);

        foreach(GameObject aura in aurasGeneradas)
        {
            Destroy(aura);
        }

        foreach (RaycastHit2D torreta in hits)
        {
            if (torreta.collider.gameObject.tag == "torreta")
            {
                GameObject torretaMejorada = torreta.collider.gameObject;

                if (torretaMejorada.GetComponent<MejorasScript>() != null)
                {

                    if (torretaMejorada.GetComponent<MejorasScript>().isPotenciado == true)
                    {
                       
                        if (torretaMejorada.GetComponent<TorretaScript>() != null) { torretaMejorada.GetComponent<TorretaScript>().dmg--; torretaMejorada.GetComponent<TorretaScript>().rango -= 0.5f; torretaMejorada.GetComponent<TorretaScript>().bps -= 0.5f; }
                        if (torretaMejorada.GetComponent<TorretaScript2>() != null) { torretaMejorada.GetComponent<TorretaScript2>().dps--; torretaMejorada.GetComponent<TorretaScript2>().cooldown += 0.2f; }
                        if (torretaMejorada.GetComponent<TorretaScript3>() != null) { torretaMejorada.GetComponent<TorretaScript3>().cooldown++; torretaMejorada.GetComponent<TorretaScript3>().rango -= 0.5f; }
                        if (torretaMejorada.GetComponent<TorretaScript4>() != null) { torretaMejorada.GetComponent<TorretaScript4>().cooldown += 0.4f; torretaMejorada.GetComponent<TorretaScript4>().rango -= 0.5f; }
                        if (torretaMejorada.GetComponent<ImanScript>() != null) { torretaMejorada.GetComponent<ImanScript>().bps -= 0.5f; torretaMejorada.GetComponent<ImanScript>().rango -= 0.5f; torretaMejorada.GetComponent<ImanScript>().ganancia--; }
                        if (torretaMejorada.GetComponent<ParlanteScript>() != null) { torretaMejorada.GetComponent<ParlanteScript>().dmg--; torretaMejorada.GetComponent<ParlanteScript>().dmgBala--; torretaMejorada.GetComponent<ParlanteScript>().rango -= 0.5f; torretaMejorada.GetComponent<ParlanteScript>().ondaSize -= 0.5f; torretaMejorada.GetComponent<ParlanteScript>().bps -= 0.5f; }

                        torretaMejorada.GetComponent<MejorasScript>().isPotenciado = false;
                        isWorking = false;
                    }

                }
            }
        }

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color newColor = sr.color;
        while (newColor.a > 0)
        {
            newColor.a -= 0.02f;
            sr.color = newColor;
            yield return null;
        }

        Destroy(this.gameObject);
        
    }


    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
#endif
    }
}
