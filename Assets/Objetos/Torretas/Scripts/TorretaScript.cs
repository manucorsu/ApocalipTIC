using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaScript : MonoBehaviour
{

    //BALLESTA

    //Objetos

    public Transform punta;
    public Transform target;
    public Transform firingPoint;
    public GameObject bala;
    public LayerMask enemigos;
    public Animator animator;

    //Variables

    public float rango;
    public float rotationSpd;
    public float bps;
    private float cooldown;
    public float precio;
    public float dmg;

    // Start is called before the first frame update
    void Start()
    {
            animator = punta.GetComponent<Animator>();
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
        } else
        {
            cooldown += Time.deltaTime;

            if (cooldown >= 1f / bps)
            {
                StartCoroutine(Disparar());
                cooldown = 0f;
            }
        }
    }

    private IEnumerator Disparar()
    {
        animator.SetFloat("anim", 1);
        GameObject balaObj = Instantiate(bala, firingPoint.position, punta.rotation);
        BalaScript balascript = balaObj.GetComponent<BalaScript>();
        balascript.SetTarget(target);
        balascript.balaDmg = dmg;
        yield return new WaitForSeconds(0.2f);
        animator.SetFloat("anim", 0);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);

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

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
    }
}
