using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulpo : EnemigoScript
{
    [Header("Pulpo")]
    [SerializeField] private GameObject aceite;
    [SerializeField] private float speedBuffMultiplier = 1.5f;

    public override void Morir()
    {
        //to do: spawnear la tinta y todo eso
        base.Morir();
    }
}
        