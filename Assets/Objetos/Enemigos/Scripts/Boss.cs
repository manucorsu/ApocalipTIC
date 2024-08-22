using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemigoScript
{
    [SerializeField] private protected SpriteRenderer sr;
    [SerializeField] private protected Sprite[] sprites;
    private protected byte si = 0; //sprite index

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Hacer boss
        {
            Debug.Log("Atacando jefe!");
            this.gameObject.tag = "enemigo";
            this.gameObject.layer = 8;
        }
        else if (Input.GetKeyDown(KeyCode.P)) // Hacer pacífico
        {
            Debug.Log("Ignorando jefe...");
            this.gameObject.tag = "Untagged";
            this.gameObject.layer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // switch sprite
        {
            si++;
            if (si >= sprites.Length)
            {
                si = 0;
            }
            sr.sprite = sprites[si];
            Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
            this.gameObject.AddComponent<PolygonCollider2D>();
        }
    }
}
