using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricosaScript : MonoBehaviour
{
    public float duracion;
    public float rango;
    RaycastHit2D[] hits;

    public float precio;

    private void Start()
    {
        StartCoroutine(Duracion());
    }

    private void Update()
    {
        FindTarget();
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
                    if (torretaMejorada.GetComponent<MejorasScript>().isPotenciado == false)
                    {
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

        foreach(RaycastHit2D torreta in hits)
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
                    }
                }
            }
        }

        Destroy(this.gameObject);
    }
}
