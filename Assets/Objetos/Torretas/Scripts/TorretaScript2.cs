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

    // Start is called before the first frame update
    void Start()
    {
        balascr2 = bala.GetComponent<BalaScript2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetRange())
        {
            target = null;
            isShooting = false;
        }
        else
        {
            if (isShooting == false)
            {
                isShooting = true;
            }
        }

        if (isShooting == true)
        {

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
        }

        if (hits.Length == 0)
        {
            balascr2.anim = 3;
        }

    }

    private void RotateTowardsTarget()
    {
        float angulo = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angulo - 90));

        punta.rotation = Quaternion.RotateTowards(punta.rotation, targetRotation, rotationSpd * Time.deltaTime);

    }

    private bool CheckTargetRange()
    {
        return Vector2.Distance(target.position, transform.position) <= rango;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
    }
}
