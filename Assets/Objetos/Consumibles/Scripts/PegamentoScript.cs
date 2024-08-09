using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegamentoScript : MonoBehaviour
{

    //Objetos

    public Animator animator;

    //Variables

    public float anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemigoScript enemigoScr = collision.gameObject.GetComponent<EnemigoScript>();
        if (enemigoScr.canBeEaten == true)
        {
            if (collision.gameObject.tag == "enemigo")
            {
                enemigoScr.spd /= 4;
            }
        } else
        {
            enemigoScr.spd = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemigoScript enemigoScr = collision.gameObject.GetComponent<EnemigoScript>();
        if (enemigoScr.canBeEaten == true)
        {
            if (collision.gameObject.tag == "enemigo")
            {
                enemigoScr.spd = enemigoScr.spdSave;
            }
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
        while(newColor.a > 0)
        {
            newColor.a -= 0.02f;
            sr.color = newColor;
            yield return null;
        }
        Destroy(this.gameObject);
    } 
}
