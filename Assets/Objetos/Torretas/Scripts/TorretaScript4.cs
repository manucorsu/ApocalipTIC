using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaScript4 : MonoBehaviour
{

    //PROYECTOR

    //Objetos

    public Transform punta;
    public Transform target;
    public GameObject bala;
    public LayerMask enemigos;
    public BalaScript4 balascr4;
    public RaycastHit2D[] hits;
    public RaycastHit2D[] hits2;
    public Transform puntaRecta;

    //Variables

    public float rango;
    public float rotationSpd;
    public bool canshoot = true;
    public float cooldown;
    public float dps;
    public float stunTime;
    public float precio;

    // Start is called before the first frame update
    void Start()
    {
        bala.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();

            return;
        }

        if (canshoot)
        {
            RotateTowardsTarget();
        }


        if (!CheckTargetRange())
        {
            target = null;
        }
        else
        {
            if (canshoot)
            {
                hits2 = Physics2D.LinecastAll(transform.position, puntaRecta.position, enemigos);
                foreach(RaycastHit2D enemigos in hits2)
                {
                    if (enemigos.transform == target.transform)
                    {
                        StartCoroutine(Atacar());
                    } 
                }
            }
        }

    }

    private void FindTarget()
    {
        hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }

    }

    private void RotateTowardsTarget()
    {
        float angulo = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angulo + 90));

        punta.rotation = Quaternion.RotateTowards(punta.rotation, targetRotation, rotationSpd * Time.deltaTime);

    }

    private bool CheckTargetRange()
    {
        return Vector2.Distance(target.position, transform.position) <= rango;
    }

    public IEnumerator Atacar()
    {
        canshoot = false;
        bala.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        bala.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        canshoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
        Handles.DrawLine(transform.position, puntaRecta.position);
    }
}
