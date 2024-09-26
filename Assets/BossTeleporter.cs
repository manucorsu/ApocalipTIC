using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleporter : MonoBehaviour
{
    [SerializeField] private Vector3 whereTo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Boss boss = collision.gameObject.GetComponent<Boss>();
        if(boss != null && boss.teleportMe)
        {
            boss.gameObject.transform.position = whereTo;
        }
    }
}
