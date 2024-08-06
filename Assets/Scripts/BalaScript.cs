using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour
{

    //Objetos

    public Rigidbody2D rb;
    private Transform target;

    //Variables

    public float balaSpd;
    public float balaDmg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * balaSpd;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "enemigo")
        {
            if (collision.transform == target)
            {
                EnemigoScript enemigoScript = collision.gameObject.GetComponent<EnemigoScript>();
                if (enemigoScript != null)
                {
                    enemigoScript.hp = (enemigoScript.hp - balaDmg);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
