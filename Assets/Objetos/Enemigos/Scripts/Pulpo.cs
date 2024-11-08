using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulpo : EnemigoScript
{
    [Header("Pulpo")]
    [SerializeField] private GameObject aceitePfb;
    public GameObject aceite;

    public override void Morir()
    {
        base.Morir();
        aceite = Instantiate(aceitePfb, this.transform.position, Quaternion.identity);
    }

    public void MorirTacho() => base.Morir();
}