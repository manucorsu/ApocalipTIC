﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImpresoraScript : MonoBehaviour
{

    //BALLESTA

    //Objetos

    public Transform target;
    public Transform firingPoint;
    public GameObject bala;
    public LayerMask enemigos;

    //Variables

    public float rango;
    public float bps;
    private float cooldown;
    public float dmg;

    // Start is called before the first frame update
    void Start()
    {

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
        }
        else
        {
            cooldown += Time.deltaTime;

            if (cooldown >= 1f / bps)
            {
                if (target.GetComponent<EnemigoScript>().canBeShot)
                {
                    StartCoroutine(Disparar());
                    cooldown = 0f;
                }
            }
        }
    }

    private IEnumerator Disparar()
    {
        Vector2 dir = firingPoint.position - target.position;
        GameObject balaObj = Instantiate(bala, firingPoint.position, Quaternion.Euler(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90));
        BalaScript balascript = balaObj.GetComponent<BalaScript>();
        balascript.SetTarget(target);
        balascript.balaDmg = dmg;
        yield return new WaitForSeconds(0.2f);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);

        if (hits.Length > 0)
        {
            target = hits[0].transform;

            if (this.gameObject.layer == LayerMask.NameToLayer("Tiralapiceras"))
            {
                Debug.DrawLine(firingPoint.transform.position, target.transform.position);
            }
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
