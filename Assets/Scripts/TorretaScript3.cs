using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaScript3 : MonoBehaviour
{

    //Objetos

    public Transform target;
    public LayerMask enemigos;

    //Variables

    public float rango;
    public float cooldown;
    public bool canEat = true;
    public int anim = 1;
    public float spd;

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
    }

    private void FindTarget()
    {
        RaycastHit2D[]  hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);
        if (hits.Length > 0)
        {
            target = hits[0].transform;

            if (canEat)
            {
                StartCoroutine(Comer());
            }
        }
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

    private IEnumerator Comer()
    {
        scrEnemigoPrueba targetscr = target.GetComponent<scrEnemigoPrueba>();
        targetscr.puedeMoverse = false;
        
        target.position = Vector3.MoveTowards(target.position, transform.position, spd * Time.deltaTime);

        yield return new WaitForSeconds(cooldown);
    }
}
