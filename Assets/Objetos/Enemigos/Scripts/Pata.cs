using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pata : EnemigoScript
{
    [Header("Pata")]
    [SerializeField] private GameObject pfbPatito;
    public bool viva = true;

    protected override void Start()
    {
        if (pfbPatito == null) throw new System.Exception("pfbPatito fue null!");
        base.Start();
        for (byte i = 1; i < 4; i++)
        {
            Patito patito = Instantiate(pfbPatito, new Vector3((this.transform.position.x - i), this.transform.position.y, this.transform.position.z),
                Quaternion.identity).GetComponent<Patito>();
            patito.madre = this;
        }
    }
    public override void Morir()
    {
        viva = false;
        base.Morir();
    }
}
