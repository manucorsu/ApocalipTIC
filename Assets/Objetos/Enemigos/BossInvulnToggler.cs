using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInvulnToggler : MonoBehaviour
{
    public static bool active = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (active)
        {
            EnemigoScript enemy = collision.gameObject.GetComponent<EnemigoScript>();
            if (enemy != null && enemy.isBoss)
            {
                if (enemy.canBeShot)
                {
                    enemy.canBeShot = false;
                    enemy.spd = 3;
                }
                else
                {
                    enemy.canBeShot = true;
                    enemy.spd = 2;
                }
            }
        }
    }
}
