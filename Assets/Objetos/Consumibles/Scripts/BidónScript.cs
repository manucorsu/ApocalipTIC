using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidónScript : MonoBehaviour
{

    //Objetos

    public Animator animator;
    public CircleCollider2D collider;

    //Variables

    public float daño;
    public float anim;
    public float precio;

    public void AnimationEnd()
    {
        if (anim == 0)
        {
            anim = 1;
            animator.SetFloat("anim", 1);
            collider.enabled = true;
            transform.localScale = new Vector2(1.5f, 1.5f);
            return;
        }

        if (anim == 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemigoScript enemigo = collision.gameObject.GetComponent<EnemigoScript>();
        if (enemigo != null)
        {
            Ninja ninja = collision.gameObject.GetComponent<Ninja>();
            if (enemigo.canBeShot || (ninja != null && ninja.Invisible))
            {
                if (ninja != null && ninja.Invisible) ninja.SetInvis(false, 0.5f);
                enemigo.Sufrir(daño);
            }
        }
    }
}
