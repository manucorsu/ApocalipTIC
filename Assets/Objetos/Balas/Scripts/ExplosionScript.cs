using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float daño;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemigo")
        {
            EnemigoScript enemigoScript = collision.GetComponent<EnemigoScript>();
            enemigoScript.Sufrir(daño);
        }
    }

    public void AnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
