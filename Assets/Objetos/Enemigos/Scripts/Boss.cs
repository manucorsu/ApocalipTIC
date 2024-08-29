using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemigoScript
{
    private byte a;
    private static bool introDone = false;
    private Dictionary<string, float> anims = new Dictionary<string, float> //viva python
    {
        {"MoveDown", 0f},
        {"MoveLeft", 1f},
        {"MoveRight", 2f},
        {"MoveUp", 3f},

        {"SpawnDown", 4f},
        {"SpawnLeft", 5f},
        {"SpawnRight", 6f},

        {"IntroLaugh", 7f},
        {"Stun", 8f},
        {"Die", 9f}
    };

    #region nav
    private Dictionary<string, dynamic[]> paths = new Dictionary<string, dynamic[]>
    /*string: nombre del camino; dynamic[]: en orden en el que se ejecutan, los puntos de movimiento y las 
      acciones (funciones, en la #region acts) que se ejecutan... Es difícil de explicar, es más fácil si lo miras directo.
      Digan lo que quieran pero C# sigue siendo mejor que JavaScript.*/
    {
        {"Intro", new dynamic[] {new string[] { "C6", "J1" }, (Action)ActIntroLaugh, "END"}},
        {"MovA", new dynamic[] {new string[] { "J1", "W6" }, (Action)ActSpawnEnemy, "END"}}
    };
    private List<string> pathQueue = new List<string>();

    protected override dynamic V3ify(string[] camino)
    {
        return base.V3ify(camino);
    }
    #endregion

    private void Awake()
    {
        pathQueue.Clear();
        isBoss = true;
        AsignarTodo();
    }
    protected override void AsignarTodo()
    {
        base.AsignarTodo();
        if (EnemySpawner.isBossFight == false) Debug.LogWarning("El jefe spawneó cuando EnemySpawner.isBossFight era false.");
    }
    private void Start()
    {
        pathQueue.Add("Intro");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Hacer boss
        {
            Debug.Log("Atacando jefe!");
            this.gameObject.tag = "enemigo";
            this.gameObject.layer = 8;
        }
        else if (Input.GetKeyDown(KeyCode.P)) // Hacer pacífico
        {
            Debug.Log("Ignorando jefe...");
            this.gameObject.tag = "Untagged";
            this.gameObject.layer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (a == 0)
            {
                animator.SetFloat("anim", anims["MoveDown"]);
                a = 1;
            }
            else
            {
                animator.SetFloat("anim", anims["MoveLeft"]);
                a = 0;
            }
        }
        while (pathQueue.Count > 0)
        {
            foreach (string p in pathQueue)
            {
                foreach(dynamic behaviour in paths[p])
                {

                }
            }
        }
    }
    #region acts
    //acá van las funciones ej "Spawnear Enemigo", "Reir" etc cuando tenga tiempo.
    private static void ActIntroLaugh()
    {
        Debug.Log("jefe: intro laugh");
        introDone = true;
    }
    private static void ActSpawnEnemy()
    {
        if (introDone) Debug.Log("jefe: generando enemigo");
    }
    #endregion
}
