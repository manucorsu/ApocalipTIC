using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaScript3 : MonoBehaviour
{

    //TACHO

    //Objetos

    public Transform target;
    public LayerMask enemigos;
    Animator animator;

    //Variables

    public float rango;
    public float cooldown;
    public bool canEat = true;
    public float anima = 2;
    public float spd;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Animator>() != null)
        {
            animator = this.gameObject.GetComponent<Animator>();
        }
        animator.SetFloat("anim", anima);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (canEat)
            {
                FindTarget();
                return;
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);
        if (hits.Length > 0)
        {
            target = hits[0].transform;

            if (canEat)
            {
                StartCoroutine(Comer());
            }
        }

    }


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
    }

    private IEnumerator Comer()
    {
        EnemigoScript targetscr = target.GetComponent<EnemigoScript>();

        if (targetscr.canBeEaten == true)
        {
            targetscr.hp = 999;
            targetscr.spd = 0;
            targetscr.canBeEaten = false;

            anima = 0;
            animator.SetFloat("anim", anima);

            while (target.position != transform.position)
            {
                target.position = Vector3.MoveTowards(target.position, transform.position, spd * Time.deltaTime);
                target.Rotate(new Vector3(0, 0, 1), 200 * Time.deltaTime);
                target.localScale = new Vector2(target.localScale.x - 0.04f, target.localScale.y - 0.04f);
                yield return null;
            }

            canEat = false;
            Destroy(target.gameObject);

            animator.enabled = true;
            anima = 1;
            animator.SetFloat("anim", anima);

            yield return new WaitForSeconds(cooldown);

            canEat = true;

            anima = 2;
            animator.SetFloat("anim", anima);
        } 
    }

    public void AnimationEnd()
    {
        animator.enabled = false;
    }
}
