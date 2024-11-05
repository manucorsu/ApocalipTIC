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
    public float precio;

    public float precioMejora;

    public float nivel1 = 1;
    public float nivel2 = 1;

    [Header("SFX")]
    [SerializeField] private AudioClip tachoAspirarSfx;
    [SerializeField] private AudioClip tachoMasticarSfx;
    [SerializeField] private AudioClip tachoEructarSfx;

    private void Start()
    {
        if (this.gameObject.GetComponent<Animator>() != null)
        {
            animator = this.gameObject.GetComponent<Animator>();
        }
        animator.SetFloat("anim", anima);
    }

    private void Update()
    {
        if (target == null)
        {
            if (canEat)
            {
                FindTarget();
                return;
            }
        }

        if (canEat == false)
        {
            if (!CheckTargetRange())
            {
                target = null;
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);
        if (hits.Length > 0)
        {
            Ninja ninja = hits[0].collider.GetComponent<Ninja>();
            if (hits[0].collider.GetComponent<Pata>() == null && hits[0].collider.GetComponent<Boss>() == null && (ninja == null || ninja.Invisible == false))
            {
                target = hits[0].transform;

                if (canEat && target.GetComponent<EnemigoScript>().canBeEaten == true)
                {
                    StartCoroutine(Comer());
                }
            }
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
    }
#endif


    private IEnumerator Comer()
    {
        EnemigoScript targetscr = target.GetComponent<EnemigoScript>();

        if (targetscr.canBeEaten == true && targetscr.isBoss == false)
        {
            targetscr.spd = 0;
            targetscr.canBeEaten = false;



            anima = 0;
            animator.SetFloat("anim", anima);
            SoundManager.Instance.PlaySound(tachoAspirarSfx);

            while (target.position != transform.position)
            {
                target.position = Vector3.MoveTowards(target.position, transform.position, spd * Time.deltaTime);
                target.Rotate(new Vector3(0, 0, 1), 200 * Time.deltaTime);
                if (target.localScale.x > 0)
                {
                    target.localScale = new Vector2(target.localScale.x - 0.04f, target.localScale.y - 0.04f);
                }
                else
                {
                    target.localScale = new Vector3(0.01f, 0.01f, 0);
                }
                yield return null;
            }

            Pulpo pulpo = target.gameObject.GetComponent<Pulpo>();
            if (pulpo == null)
            {
                targetscr.Morir(true);
                Debug.Log("el tacho mató un enemigo por primera vez");
            }
            else
            {
                pulpo.MorirTacho();
                Debug.Log("el tacho mató un pulpo");
            }

            GameObject explosion = Instantiate(targetscr.explosionMuerte, transform.position, Quaternion.identity);
            explosion.GetComponent<SpriteRenderer>().color = targetscr.colorExplosion;
            EnemySpawner.botsEliminados++;
            if (!EnemySpawner.isBossFight) EnemySpawner.botsEliminadosRonda++;
            EnemySpawner.botsVivos.Remove(target.gameObject);
            GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>().plataActual += targetscr.plata;
            Destroy(target.gameObject);

            canEat = false;
            SoundManager.Instance.PlaySound(tachoMasticarSfx, 1f);



            animator.enabled = true;
            anima = 1;
            animator.SetFloat("anim", anima);

            yield return new WaitForSeconds(cooldown);

            canEat = true;
            SoundManager.Instance.PlaySound(tachoEructarSfx, 0.75f);
            anima = 2;
            animator.SetFloat("anim", anima);
        }
    }

    private bool CheckTargetRange()
    {
        if (target != null)
        {
            return Vector2.Distance(target.position, transform.position) <= rango;
        }
        else
        {
            return false;
        }
    }

    public void AnimationEnd()
    {
        animator.enabled = false;
    }
}
