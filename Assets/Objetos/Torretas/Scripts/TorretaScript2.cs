using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaScript2 : MonoBehaviour
{

    //NICHO

    //Objetos

    public Transform punta;
    public Transform target;
    public GameObject bala;
    public LayerMask enemigos;
    private BalaScript2 balascr2;
    public RaycastHit2D[] hits;

    //Variables

    public float dps;
    public float rango;
    public float rotationSpd;
    public bool isShooting = false;
    public float precio;
    public float cooldown = 1;

    public float precioMejora;

    public float nivel1 = 1;
    public float nivel2 = 1;


    // Start is called before the first frame update
    void Start()
    {
        balascr2 = bala.GetComponent<BalaScript2>();
        balascr2.nichoPadre = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }


        if (!CheckTargetRange())
        {
            target = null;
            isShooting = false;
        }
        else
        {
            RotateTowardsTarget();

            if (isShooting == false)
            {
                isShooting = true;
            }
        }



        if (isShooting)
        {
            balascr2.dps = this.dps;
            bala.gameObject.SetActive(true);
            if (balascr2.anim != 2)
            {
                balascr2.anim = 1;
            }
        }
    }


    private void FindTarget()
    {
        hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
            EnemigoScript targetscr = target.GetComponent<EnemigoScript>();
            if (targetscr.canBeEaten == false && targetscr.canBeShot == false)
            {
                target = null;
            }
        }

        if (hits.Length == 0)
        {
            balascr2.anim = 3;
        }

    }

    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            Boss boss = target.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                if (boss.canBeShot == false && boss.introDone == true) return;
            } //que no busque al jefe si no se le puede disparar (salvo durante la intro porque queda épico)

            Ninja ninja = target.GetComponent<Ninja>();
            if (ninja != null && ninja.Invisible) return;

            float angulo = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angulo - 90));

            punta.rotation = Quaternion.RotateTowards(punta.rotation, targetRotation, rotationSpd * Time.deltaTime);
        }

    }

    private bool CheckTargetRange()
    {
        return Vector2.Distance(target.position, transform.position) <= rango;
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
#endif
    }
}
