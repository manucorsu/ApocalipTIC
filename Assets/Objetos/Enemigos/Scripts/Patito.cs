using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patito : EnemigoScript
{
    [HideInInspector] public Pata madre;
    [HideInInspector] public float baseSpd;

    protected override void Start()
    {
        if (this.madre == null) throw new System.Exception("Los patitos deben tener una madre cuando aparecen");
        this.spName = madre.spName;
        base.Start();
    }

    protected override void Update()
    {
        //if (madre.viva)
        //{
        //    float x = madre.gameObject.transform.position.x;
        //    float y = madre.gameObject.transform.position.y;

        //    this.transform.position = Vector3.MoveTowards(
        //        this.transform.position,
        //        new Vector3(x, y, this.transform.position.z),
        //        spd * Time.deltaTime);
        //}
        //else
        base.Update();
    }
}
