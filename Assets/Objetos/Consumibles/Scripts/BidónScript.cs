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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationEnd()
    {
        if(anim == 0)
        {
            anim = 1;
            animator.SetFloat("anim", 1);
            collider.enabled = true;
            return;
        }

        if (anim == 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemigo")
        {         
           EnemigoScript enemigoScr = collision.gameObject.GetComponent<EnemigoScript>();
           enemigoScr.hp -= daño;
        }
    }
}
