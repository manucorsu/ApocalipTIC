using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float daño;

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
        if (collision.tag == "enemigo")
        {
            EnemigoScript enemigoScript = collision.GetComponent<EnemigoScript>();
            enemigoScript.hp -= daño;

            Debug.Log("GOLPE");
        }
    }

    public void AnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
