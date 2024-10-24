﻿using System.Collections;
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
            targetscr.hp = 999;
            targetscr.spd = 0;
            targetscr.canBeEaten = false;



            anima = 0;
            animator.SetFloat("anim", anima);
            SoundManager.instance.PlaySound(tachoAspirarSfx);

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
                    target.localScale = new Vector3(0, 0, 0);
                }
                yield return null;
            }

            Pulpo pulpo = target.GetComponent<Pulpo>();
            if (pulpo == null) targetscr.Morir();
            else pulpo.MorirTacho();

            canEat = false;
            SoundManager.instance.PlaySound(tachoMasticarSfx, 0.8f);

            

            animator.enabled = true;
            anima = 1;
            animator.SetFloat("anim", anima);

            yield return new WaitForSeconds(cooldown);

            canEat = true;
            SoundManager.instance.PlaySound(tachoEructarSfx, 0.75f);
            anima = 2;
            animator.SetFloat("anim", anima);
        }
    }

    public void AnimationEnd()
    {
        animator.enabled = false;
    }
}
