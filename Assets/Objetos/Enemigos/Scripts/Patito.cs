using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patito : EnemigoScript
{
    [HideInInspector] public Pata madre;
    private bool libre = false;
    private float patitoBaseSpd;

    protected override void Start()
    {
        if (madre == null)
        {
            throw new System.Exception("Todos los patitos deben tener una madre cuando spawnean.");
        }
        patitoBaseSpd = this.spd;
        this.spd = madre.spd;
        this.spName = madre.spName;
        this.transform.parent = null;
        base.Start();
    }

    protected override void Update()
    {
        if (!libre)
        {
            this.spd = madre.spd;
            this.hp = float.MaxValue;
            this.canBeShot = false; this.gameObject.layer = 0; this.gameObject.tag = "Untagged";
            this.canBeEaten = false;
            this.hpBar.SetActive(false);
        }
        base.Update();
    }

    public void Liberar()
    {
        this.spd = patitoBaseSpd;
        this.hp = this.baseHP;
        this.canBeShot = true; this.gameObject.layer = 8; this.gameObject.tag = "enemigo";
        this.canBeEaten = true;
        this.hpBar.SetActive(true);
        libre = true;
    }

    public override void Morir()
    {
        base.Morir();
        EnemySpawner.botsEliminadosRonda--;
    }
}
