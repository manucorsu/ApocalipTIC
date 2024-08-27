using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemigoScript
{
    private void Awake()
    {
        isBoss = true;
        AsignarTodo();
    }
    protected override void AsignarTodo()
    {
        base.AsignarTodo();
        if (EnemySpawner.isBossFight == false)
        {
            Debug.LogWarning("El jefe spawneó cuando EnemySpawner.isBossFight era false.");
        }
    }
    private void Start()
    {
        BuscarPath();
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
