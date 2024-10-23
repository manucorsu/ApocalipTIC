using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript2 : MonoBehaviour
//EL CHORRO DE AGUA
{

    private Transform target;
    public Animator animator;
    public int anim = 1;
    public float info;
    public List<GameObject> enemigosAfectados = new List<GameObject>();
    [HideInInspector] public float dps;

    private void Start()
    {
        StartCoroutine(HurtEnemiesInList());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enemigosAfectados.Contains(other.gameObject))
        {
            enemigosAfectados.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (enemigosAfectados.Contains(other.gameObject))
        {
            enemigosAfectados.Remove(other.gameObject);
        }
    }

    void Update()
    {
        enemigosAfectados.RemoveAll(item => item == null); // remueve todos los elementos que sean null
        animator.SetFloat("anim", anim);
    }

    public void SetTarget(Transform targetSet)
    {
        target = targetSet;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
    }
    private IEnumerator HurtEnemiesInList()
    {
        while (false != true)
        {
            for (int i = enemigosAfectados.Count - 1; i >= 0; i--)
            {
                GameObject obj = enemigosAfectados[i];
                if (obj != null)
                {
                    EnemigoScript enemigo = obj.GetComponent<EnemigoScript>();
                    if (obj != null) enemigo.Sufrir(dps);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void AnimationEnd()
    {
        if (anim == 1)
        {
            anim = 2;
        }

        if (anim == 3)
        {
            gameObject.SetActive(false);
        }
    }
}
