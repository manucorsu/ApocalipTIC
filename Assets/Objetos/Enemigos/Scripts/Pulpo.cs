using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulpo : EnemigoScript
{
    [Header("Pulpo")]
    [SerializeField] private GameObject aceitePfb;

    public override void Morir()
    {
        base.Morir();
        Instantiate(aceitePfb, this.transform.position, Quaternion.identity);
    }
}