using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pata : EnemigoScript
{
    [Header("Pata")]
    [SerializeField] private GameObject[] patitos;

    protected override void Start()
    {
        if (patitos.Length == 0)
        {
            throw new System.Exception("El array de patitos está vacío.");
        }
        foreach (GameObject patito in patitos)
        {
            Patito patitoScript = patito.GetComponent<Patito>();
            if (patitoScript == null)
            {
                throw new System.Exception("Todos los patitos deben tener el script `Patito`.");
            }
            else patitoScript.madre = this;
        }
        base.Start();
    }

    public override void Morir()
    {
        foreach (GameObject patito in patitos)
        {
            patito.transform.parent = null;
            Patito pScr = patito.GetComponent<Patito>();
            pScr.v3Camino = this.v3Camino;
            pScr.secuenciaAnims = this.secuenciaAnims;
            pScr.wi = this.wi;
            pScr.currentWaypoint = this.currentWaypoint;
            pScr.Liberar();
        }
        base.Morir();
    }
}
