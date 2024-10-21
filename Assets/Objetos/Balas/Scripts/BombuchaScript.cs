using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombuchaScript : MonoBehaviour
{
    //Objetos

    public Rigidbody2D rb;
    public Transform target;
    private SpriteRenderer sr;
    public GameObject explosion;
    //Variables

    public float balaSpd;
    public float balaDmg;

    [SerializeField] private AudioClip bbchPopSfx;

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
            Destroy(this.gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * balaSpd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemigo")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(bbchPopSfx, 2);
            Destroy(this.gameObject);
        }
    }
}
