using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour
{

    //Objetos

    public Rigidbody2D rb;
    public Transform target;
    private SpriteRenderer sr;

    //Variables

    public float balaSpd;
    public float balaDmg;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "BalaImpresora")
        {
            sr = GetComponent<SpriteRenderer>();
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            sr.color = randomColor;
        }
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
            Destroy(this.gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * balaSpd;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target)
        {
            Destroy(this.gameObject);
        }
    }
}
