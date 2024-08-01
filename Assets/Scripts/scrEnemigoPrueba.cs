using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemigoPrueba : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(-2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
