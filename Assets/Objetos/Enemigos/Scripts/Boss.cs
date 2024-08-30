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

    #region nav/behaviour
    #region interface y derivados
    public interface IBossCmd
    {
        void Execute(Boss boss);
    }
    public class MoveToWPCmd : IBossCmd
    {
        private string waypoint;

        public MoveToWPCmd(string waypoint)
        {
            this.waypoint = waypoint;
        }

        public void Execute(Boss boss)
        {
            boss.MoveTo(waypoint);
        }
    }
    public class GenerarEnemigoCmd : IBossCmd
    {
        public void Execute(Boss boss)
        {
            boss.GenerarEnemigo();
        }
    }
    #endregion

    #region acciones
    public void MoveTo(string wp)
    {
        Vector3 wpv3 = V3ify(new string[] { wp })[0];
        this.transform.position = Vector3.MoveTowards(this.transform.position, wpv3, spd * Time.deltaTime);
    }
    public void GenerarEnemigo()
    {
        Debug.Log("spawning enemy");
    }
    #endregion
    private Queue<IBossCmd> colaCmds = new Queue<IBossCmd>();
    #endregion

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
        colaCmds.Enqueue(new MoveToWPCmd("J1"));
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
    }
}
