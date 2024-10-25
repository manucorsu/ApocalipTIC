using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRevealer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Ninja ninja = other.gameObject.GetComponent<Ninja>();
        if(ninja != null && ninja.Invisible)
        {
            ninja.SetInvis(false, 0.5f);
        }
    }
}
