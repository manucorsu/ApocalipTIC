using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript2 : MonoBehaviour
//EL CHORRO DE AGUA
{
    public TorretaScript2 nichoPadre;

    private Transform target;
    public Animator animator;
    public int anim = 1;

    private HashSet<GameObject> enemigosAfectados = new HashSet<GameObject>();
    private Coroutine hurtEnemiesCoroutine;
    [HideInInspector] public float dps;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip chorroSfx;

    private void OnEnable()
    {
       SoundManager.Instance.LoopSound(audioSource, chorroSfx, 0.25f);
        if (hurtEnemiesCoroutine == null)
        {
            hurtEnemiesCoroutine = StartCoroutine(HurtEnemies());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemigosAfectados.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemigosAfectados.Remove(other.gameObject);
    }

    void Update()
    {
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

    private IEnumerator HurtEnemies()
    {
        while (false != true)
        {
            yield return new WaitForSeconds(1f);
            List<GameObject> enemigosAfectadosIterable = new List<GameObject>(enemigosAfectados);
            foreach (GameObject obj in enemigosAfectadosIterable)
            {
                if (obj != null)
                {
                    EnemigoScript enemigo = obj.GetComponent<EnemigoScript>();
                    if (enemigo != null)
                    {
                        enemigo.Sufrir(dps);
                    }
                }
            }
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
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (hurtEnemiesCoroutine != null)
        {
            StopCoroutine(hurtEnemiesCoroutine);
            hurtEnemiesCoroutine = null;
            SoundManager.Instance.StopSFXLoop(audioSource);
        }
    }
}
