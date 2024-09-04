using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemigoScript
{
    private bool introDone = false;
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
        canBeShot = false;
        idle = false;
        StartCoroutine(MoveTo("W1"));
        idle = true;
    }
    private void DoIntroLaugh()
    {
        animator.SetBool("playIntroLaugh", true);
        canBeShot = true;
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
            if (!introDone)
            {
                DoIntroLaugh();
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
        introDone = false;
        GameObject padreSpawners = GameObject.Find("Spawners");
        foreach (Transform s in padreSpawners.transform)
        {
            if (s != padreSpawners)
            {
                this.waypoints.Add(s.transform);
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
        if ("a" == "b")
        {
            // Check if the animation has finished
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                canBeShot = true;
                animator.SetBool("playNewTestAnim", false);
            }
        }
    }
}