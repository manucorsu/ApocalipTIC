using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemigoScript
{
    byte a = 0;
    private Dictionary<string, float> anims = new Dictionary<string, float> //viva python
    {
        {"MoveDown", 0f},
        {"MoveLeft", 1f},
        {"MoveRight", 2f},
        {"MoveUp", 3f},

        {"SpawnDown", 4f},
        {"SpawnLeft", 5f},
        {"SpawnRight", 6f},

        {"Stun", 7f},
        {"Die", 8f}
    };
    private Dictionary<string, string[]> acciones = new Dictionary<string, string[]> { }; //string key, string[] camino que debe ser V3ificado.
    private void Awake()
    {
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

        if (siguiendo == true)
        {
            //{poner cosas de animación acá}
            this.transform.position = Vector3.MoveTowards(this.transform.position, v3Camino[wi], spd * Time.deltaTime);

            if (transform.position == v3Camino[wi])
            {
                wi++;
            }
        }
    }
}
