using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParlanteScript : MonoBehaviour
{

    //PARLANTE


    //Objetos

    [SerializeField] private AudioClip parlanteSfx;
    public Animator animator;
    public Transform target;
    public Transform firingPoint;
    public GameObject onda;
    public GameObject bala;
    public LayerMask enemigos;
    public Sprite[] notas;

    //Variables

    public float precio;
    public float rango;
    public float bps;
    private float cooldown;
    public float dmg;
    public float dmgBala;
    public float ondaSize = 1;

    public float nivel1 = 1;
    public float nivel2 = 1;
    public float nivel3 = 1;

    public float precioMejora;

    private Vector2[] directions = new Vector2[]
   {
        new Vector2(1, 0),     // Right
        new Vector2(1, 1),     // Up-right
        new Vector2(0, 1),     // Up
        new Vector2(-1, 1),    // Up-left
        new Vector2(-1, 0),    // Left
        new Vector2(-1, -1),   // Down-left
        new Vector2(0, -1),    // Down
        new Vector2(1, -1)     // Down-right
   };

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
                    Boss boss = target.gameObject.GetComponent<Boss>();
                    if (boss != null && boss.introDone && boss.canBeShot == false) return; //que no busque al jefe si no se le puede disparar (salvo durante la intro porque queda épico)

                    StartCoroutine(Disparar());
                    cooldown = 0f;
                }
            }
        }
    }

    private IEnumerator Disparar()
    {
        SoundManager.Instance.PlaySound(parlanteSfx, 0.2f);
        animator.SetTrigger("anim");
        GameObject explosion = Instantiate(onda, firingPoint);
        explosion.transform.localScale = new Vector3(ondaSize, ondaSize, 1);
        OndaScript ondaScript = explosion.GetComponent<OndaScript>();
        ondaScript.daño = dmg;
        Sprite nota = notas[Random.Range(0, notas.Length)];
        for (int i = 0; i < 8; i++)
        {
            GameObject balaMusical = Instantiate(bala, firingPoint);
            balaMusical.GetComponent<SpriteRenderer>().sprite = nota;
            BalaMusicalScript balaScript = balaMusical.GetComponent<BalaMusicalScript>();
            balaScript.direction = directions[i];
            balaScript.daño = dmgBala;
        }
        yield return new WaitForSeconds(0.2f);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
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

