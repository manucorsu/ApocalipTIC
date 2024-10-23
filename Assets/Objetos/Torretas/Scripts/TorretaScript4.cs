using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaScript4 : MonoBehaviour
{

    //PROYECTOR

    //Objetos

    public Transform punta;
    public Transform target;
    public GameObject bala;
    public LayerMask enemigos;
    public RaycastHit2D[] hits;
    public RaycastHit2D[] hits2;
    public Transform puntaRecta;
    public AudioClip sfxRayo;

    //Variables

    public float rango;
    public float rotationSpd;
    public bool canshoot = true;
    public float cooldown;
    public float dps;
    public float stunTime;
    public float precio;

    public float precioMejora;

    public float nivel1 = 1;
    public float nivel2 = 1;

    public int idleRotationPoint = 0;
    public bool isIdle;
    private float idleRotationCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        bala.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            if (Quaternion.Angle(punta.rotation, Quaternion.Euler(new Vector3(0f, 0f, idleRotationPoint))) < 0.1f)
            {
                if (idleRotationCooldown > 1)
                {
                    idleRotationPoint = Random.Range(0, 360);
                    idleRotationCooldown = 0;
                }

                idleRotationCooldown += Time.deltaTime;
            }
            else
            {
                punta.rotation = Quaternion.RotateTowards(punta.rotation, Quaternion.Euler(new Vector3(0f, 0f, idleRotationPoint)), rotationSpd * Time.deltaTime);
            }
        }

        if (target == null)
        {
            isIdle = true;
            FindTarget();
            return;
        }


        if (!CheckTargetRange())
        {
            isIdle = true;
            target = null;
        }
        else
        {
            if (canshoot)
            {
                isIdle = false;
                RotateTowardsTarget();

                hits2 = Physics2D.LinecastAll(transform.position, puntaRecta.position, enemigos);
                foreach (RaycastHit2D enemigos in hits2)
                {
                    EnemigoScript enemigoScript = target.gameObject.GetComponent<EnemigoScript>();
                    if (enemigoScript != null && enemigoScript.canBeShot)
                    {
                        if (enemigos.transform == target.transform && enemigoScript.spd > 0)
                        {
                            StartCoroutine(Atacar());
                        }
                    }
                }
            } 
        }

        //Animacion Idle

       

    }

    private void FindTarget()
    {
        hits = Physics2D.CircleCastAll(transform.position, rango, new Vector2(transform.position.x, transform.position.y), 0f, enemigos);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }

    }

    private void RotateTowardsTarget()
    {
        Boss boss = target.gameObject.GetComponent<Boss>();
        if (boss != null && boss.introDone == true && boss.canBeShot == false) return; //que no busque al jefe si no se le puede disparar (salvo durante la intro porque queda épico)

        float angulo = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angulo + 90));

        punta.rotation = Quaternion.RotateTowards(punta.rotation, targetRotation, rotationSpd * Time.deltaTime);

    }

    private bool CheckTargetRange()
    {
        return Vector2.Distance(target.position, transform.position) <= rango;
    }

    public IEnumerator Atacar()
    {
        SoundManager.instance.PlaySound(sfxRayo, 0.3f);
        canshoot = false;
        bala.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        bala.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        canshoot = true;
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, rango);
        Handles.DrawLine(transform.position, puntaRecta.position);
#endif
    }
}
