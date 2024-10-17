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
    }
    protected override void Update()
    {
        if (libre == false)
        {
            this.spd = 0;
            this.hp = float.MaxValue;
            this.canBeShot = false;
            this.canBeEaten = false;
            this.hpBar.SetActive(false);
            this.siguiendo = false;
        }
        base.Update();
    }
    public void Liberar()
    {
        this.spd = patitoBaseSpd;
        this.hp = this.baseHP;
        this.canBeShot = true;
        this.canBeEaten = true;
        this.siguiendo = true;
        this.hpBar.SetActive(true);
        libre = true;
        base.Start();
    }
    public override void Morir()
    {
        base.Morir();
        EnemySpawner.botsEliminadosRonda--;
    }
}
