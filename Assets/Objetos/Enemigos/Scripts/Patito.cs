using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patito : EnemigoScript
{
    protected override void Start()
    {
        StartCoroutine(WaitUntilReady());
    }

    private IEnumerator WaitUntilReady()
    {
        while (siguiendo == false) yield return null;
        base.Start();
    }

    public override void Morir()
    {
        this.spd = 0;
        if (!isBoss && canBeEaten)
        {
            GameObject explosion = Instantiate(explosionMuerte, transform.position, Quaternion.identity);
            explosion.GetComponent<SpriteRenderer>().color = colorExplosion;
        }
        construirscr.plataActual += plata;
        EnemySpawner.botsEliminados++;
        EnemySpawner.botsVivos.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
