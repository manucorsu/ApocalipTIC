using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaScript : MonoBehaviour
{

    //TIRALÁPICES

    //Objetos

    public Transform punta;
    public Transform target;
    public Transform firingPoint;
    public GameObject bala;
    public LayerMask enemigos;
    public Animator animator;
    public LineRenderer lr;
    public Transform puntaRaycast;

    //Variables

    public float rango;
    public float rotationSpd;
    public float bps;
    private float cooldown;
    public float precio;
    public float dmg;

    public float nivel1 = 1;
    public float nivel2 = 1;
    public float nivel3 = 1;

    public float precioMejora;

    [Header("SFX")]
    [SerializeField] private AudioClip tlapicesShootSfx;
    [SerializeField] private AudioClip tlapicerasShootSfx;

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

        if (lr != null)
        {
            if (target != null)
            {
                lr.SetPosition(0, firingPoint.transform.position);
                lr.SetPosition(1, target.transform.position);
            } else
            {
                lr.enabled = false;
            }
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
        animator.SetFloat("anim", 1);
        GameObject balaObj = Instantiate(bala, firingPoint.position, punta.rotation);
        if (balaObj.tag == "Bombucha")
        {
            BombuchaScript bombuchaScr = balaObj.GetComponent<BombuchaScript>();
            bombuchaScr.SetTarget(target);
            bombuchaScr.balaDmg = dmg;
        }
        else
        {
            BalaScript balascript = balaObj.GetComponent<BalaScript>();
            balascript.SetTarget(target);
            balascript.balaDmg = dmg;
        }
        if (GetComponent<LineRenderer>() == null)
        {
            SoundManager.instance.PlaySound(tlapicesShootSfx, 2);
        }
        else SoundManager.instance.PlaySound(tlapicerasShootSfx);
        yield return new WaitForSeconds(0.2f);
        animator.SetFloat("anim", 0);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos); 
        

        if (hits.Length > 0)
        {
            target = hits[0].transform;

            if (lr != null)
            {
                //RaycastHit2D[] hits2 = Physics2D.LinecastAll(punta.position, puntaRaycast.position, enemigos);
                if (hits.Length > 0)
                {
                    lr.enabled = true;
                }
            }
        } else
        {
            if (lr != null)
            {
                lr.enabled = false;
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Boss boss = target.gameObject.GetComponent<Boss>();
        if (boss != null && boss.introDone && boss.canBeShot == false) return; //que no busque al jefe si no se le puede disparar (salvo durante la intro porque queda épico)
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
#if UNITY_EDITOR
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
#endif  
    }
}
