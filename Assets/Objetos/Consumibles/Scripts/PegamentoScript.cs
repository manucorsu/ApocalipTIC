using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegamentoScript : MonoBehaviour
{

    //Objetos

    public Animator animator;

    //Variables

    public float anim;
    public float precio;

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemigoScript enemigoScr = collision.gameObject.GetComponent<EnemigoScript>();
        if (enemigoScr != null)
        {
            if (enemigoScr.canBeEaten == true)
            {
                if (enemigoScr.isPegamentoed)
                {
                    enemigoScr.spd = enemigoScr.spdSave;
                    enemigoScr.isPegamentoed = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemigo")
        {

            EnemigoScript enemigoScr = collision.gameObject.GetComponent<EnemigoScript>();


            if (enemigoScr.canBeEaten == true || enemigoScr.isBoss)
            //No sé que cosa tiene Marcos con usar canBeEaten para todo aunque esté objetivamente mal
            {
                enemigoScr.spd = enemigoScr.slowSpd;
                enemigoScr.isPegamentoed = true;
            }
            //else
            //{
            //    enemigoScr.spd = 0;
            //}
            //CREO que no hace falta esto
        }

    }


    public void AnimationEnd()
    {
        if (anim == 0)
        {
            anim = 1;
            animator.SetFloat("anim", anim);
            StartCoroutine(Pegamento());
        }
    }

    public IEnumerator Pegamento()
    {
        yield return new WaitForSeconds(15);
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color newColor = sr.color;
        while (newColor.a > 0)
        {
            newColor.a -= 0.02f;
            sr.color = newColor;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
