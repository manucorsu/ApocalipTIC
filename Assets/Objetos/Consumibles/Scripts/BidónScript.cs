using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidónScript : MonoBehaviour
{
    //Objetos

    public Animator animator;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public CircleCollider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    //Variables

    public float daño;
    public float anim;
    public float precio;
    [SerializeField] private AudioClip bbchPopSfx;

    private void Start()
    {
        SoundManager.Instance.PlayUISounds(new AudioClip[] { SoundManager.Instance.BuySfx, bbchPopSfx }, new float[] { 0.4f, 1 }, 0.5f);
    }

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
