using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornilloScript : MonoBehaviour
{
    public GameObject torreta;
    private int rotativo;

    private void Start()
    {
        rotativo = Random.Range(-1, 1);
    }

    private void Update()
    { 
        
        transform.position = Vector2.MoveTowards(transform.position, torreta.transform.position, 0.1f);
        transform.rotation = new Quaternion(0, 0, transform.rotation.z + rotativo, 0);

        if (transform.position == torreta.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
