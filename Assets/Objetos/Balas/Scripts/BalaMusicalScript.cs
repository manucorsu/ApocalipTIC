using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMusicalScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 direction;
    public float spd;
    public float daño;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction.normalized * spd;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemigo")
        {
            EnemigoScript enemigoScript = collision.GetComponent<EnemigoScript>();
            enemigoScript.Sufrir(daño);
            Destroy(this.gameObject);
        }

        if (collision.tag == "Límite")
        {
            Destroy(this.gameObject);
        }
    }
}
