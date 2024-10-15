using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patito : EnemigoScript
{
    [HideInInspector] public Pata madre;

    protected override void Start()
    {
        if (this.madre == null) throw new System.Exception("Los patitos deben tener una madre cuando aparecen");
        this.spName = madre.spName;
        base.Start();
    }

    protected override void Update()
    {
        while (madre.viva)
        {
            this.spd /= this.madre.spd;
            
        }
        base.Update();
    }
}
