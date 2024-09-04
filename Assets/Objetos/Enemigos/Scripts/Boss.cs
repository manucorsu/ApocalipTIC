using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemigoScript
{
    private bool idle; //si true, puede buscar un nuevo behaviour, si false está haciendo algo entonces no puede
    private Dictionary<string, float> anims = new Dictionary<string, float> //viva python
    {
        {"MoveDown", 0f},
        {"MoveLeft", 1f},
        {"MoveRight", 2f},
        {"MoveUp", 3f},

        {"SpawnEnemy", 4f},

        {"IntroLaugh", 5f},
        {"Stun", 6f},
        {"Die", 7f}
    };

    #region behaviour
    private void DoIntro()
    { //recuerdos de scratch
        idle = false;
        this.gameObject.tag = "Untagged";
        this.gameObject.layer = 0;
        //animator.SetFloat("anim", anims["MoveLeft"]);
        StartCoroutine(MoveTo("C3"));
        //animator.SetFloat("anim", anims["MoveDown"]); 
        StartCoroutine(MoveTo("W1"));
        DoIntroLaugh();
        this.gameObject.tag = "enemigo";
        this.gameObject.layer = 8;
        idle = true;
    }
    private IEnumerator DoIntroLaugh()
    {
        while (false != true)
        {
            Debug.Log("risa malvada");
            //animator.SetFloat("anim", anims["IntroLaugh"]);
            yield return new WaitForSeconds(3);
            break;
        }
    }
    private IEnumerator MoveTo(string wp)
    {
        while (false != true)
        {
            Vector3 desiredPos = new Vector3();
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (waypoints[i].name == wp)
                {
                    desiredPos = waypoints[i].position;
                    break;
                }
            }
            while (this.transform.position != desiredPos)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, desiredPos, spd * Time.deltaTime);
                yield return null; //null == esperar al siguiente frame a lo Update
            }
            break;
        }
    }
    #endregion

    private void Awake()
    {
        isBoss = true;
        AsignarTodo();
    }
    protected override void AsignarTodo()
    {
        base.AsignarTodo();
        GameObject padreSpawners = GameObject.Find("Spawners");
        foreach (Transform s in padreSpawners.transform)
        {
            if (s != padreSpawners)
            {this.waypoints.Add(s.transform);
            }
        }
        idle = true;
        if (EnemySpawner.isBossFight == false) Debug.LogWarning("El jefe spawneó cuando EnemySpawner.isBossFight era false.");
    }
    private void Start()
    {
        DoIntro();
    }
    private void Update()
    {

    }
}