using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemigoPrueba : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool puedeMoverse = true;

    // Start is called before the first frame update
    void Start()
    {
        if (puedeMoverse == true)
        {
            rb.velocity = new Vector3(-2, 0, 0);
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
