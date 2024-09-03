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
        //animator.SetFloat("anim", anims["MoveLeft
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
        Debug.Log("risa malvada");
        //animator.SetFloat("anim", anims["IntroLaugh"]);
        yield return new WaitForSeconds(3);
    }
    private IEnumerator MoveTo(string wp)
    {
        Vector3 desiredPos = V3ify(wp);
        while (this.transform.position != desiredPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, desiredPos, spd * Time.deltaTime);
            yield return null; //null == esperar al siguiente frame a lo Update
        }
        Vector3 V3ify(string w)
        {
            //todo
            return new Vector3(0, 0, 0);
        }
    }
    #endregion

    private void Awake()
    {
        isBoss = true;
        AsignarTodo();
        DoIntro();
    }
    protected override void AsignarTodo()
    {
        base.AsignarTodo();
        idle = true;
        if (EnemySpawner.isBossFight == false) Debug.LogWarning("El jefe spawneó cuando EnemySpawner.isBossFight era false.");
    }
    private void Start()
    {

    }
    private void Update()
    {

    }
}