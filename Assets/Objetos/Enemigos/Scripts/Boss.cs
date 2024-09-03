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

    #region behaviours

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

    }
    private void Update()
    {
        #region debug
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
        else if (Input.GetKeyDown(KeyCode.M)) //movement test
        {
            StartCoroutine(Move("icyfgtvuoijokpo"));
        }
        #endregion
        IEnumerator Move(string w)
        {
            Vector3 desiredPos = new Vector3(-5.5f, -0.5f, 0);
            while (this.transform.position != desiredPos)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, desiredPos, spd * Time.deltaTime);
                yield return null; //null == esperar al siguiente frame a lo Update
            }
        }
    }

    string TPQ = "Manuwu Coworsuwunky Gay";

        //JAJAJAJAJAJAJAJAJ MANU ES UN TPQ Y UNA BUTAQUERA AJJAJAJAJajAJAJAJAJAJAJJAJAJAJA
}
