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
        switch (spName[0])
        {
            case 'C':
                patitos[0].transform.localPosition = new Vector3(1.5f, -0.5f);
                patitos[1].transform.localPosition = new Vector3(2.5f, -0.5f);
                patitos[2].transform.localPosition = new Vector3(3.5f, -0.5f);
                break;
            case 'D':
                patitos[0].transform.localPosition = new Vector3(0, -2);
                patitos[1].transform.localPosition = new Vector3(0, -3.5f);
                patitos[2].transform.localPosition = new Vector3(0, -5);
                break;
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
            if(patito != null) patito.GetComponent<Patito>().Liberar();
        }
        base.Morir();
    }
}
