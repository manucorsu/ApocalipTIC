using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImanScript : TorretaScript
{
    [SerializeField] private AudioClip imanShootSfx;
    private float cooldownIman;
    public bool isAbsorbing = false;
    public GameObject tuerca;
    public int ganancia = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (target == null)
        {
            if (isAbsorbing)
            {
                animator.SetFloat("anim", 0);
                isAbsorbing = false;
            }
            FindTarget();
            return;
        }
        else
        {
            if (target.GetComponent<EnemigoScript>().isBoss) return;
            else
            {
                animator.SetFloat("anim", 1);
                isAbsorbing = true;
            }
        }

        RotateTowardsTarget();
        if (!CheckTargetRange())
        {
            target = null;
        }
        else
        {

            if (isAbsorbing)
            {
                cooldownIman += Time.deltaTime;

                if (cooldownIman >= 1f / bps)
                {
                    if (target.GetComponent<EnemigoScript>().canBeShot)
                    {
                        SoundManager.instance.PlaySound(imanShootSfx, 0.1f);
                        GameObject basura = Instantiate(tuerca, new Vector2(target.position.x, target.position.y), Quaternion.identity);
                        TornilloScript basuraScript = basura.GetComponent<TornilloScript>();
                        basuraScript.torreta = this.gameObject;
                        basuraScript.puntaTorreta = punta.gameObject;
                        basuraScript.ganancia = ganancia;
                        cooldownIman = 0f;
                    }
                }
            }
        }
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
        Boss boss = target.gameObject.GetComponent<Boss>();
        if (boss != null && boss.introDone && boss.canBeShot == false) return; //que no busque al jefe si no se le puede disparar (salvo durante la intro porque queda épico)
        Ninja ninja = target.GetComponent<Ninja>();
        if (ninja != null && ninja.Invisible) return;
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
#if UNITY_EDITOR
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
#endif  
    }
}
