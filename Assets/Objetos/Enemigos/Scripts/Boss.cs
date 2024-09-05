using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemigoScript
{
    private string activeBehaviour; //la intro no cuenta
    private bool idle = false;
    private bool introDone = false;
    private Dictionary<string, float> moveAnims = new Dictionary<string, float> //viva python
    {
        {"MoveDown", 0f},
        {"MoveLeft", 1f},
        {"MoveRight", 2f},
        {"MoveUp", 3f},
    };

    #region behaviour
    private void SetMoveAnim(string an)
    {
        if (moveAnims.ContainsKey(an))
        {
            animator.SetFloat("move", moveAnims[an]);
        }
        else
        {
            Debug.LogError($"SetMoveAnim: No existe la key {an} en el diccionario de moveAnims!!");
        }
    }
    #region la intro
    private void DoIntro()
    { //recuerdos de scratch
        SetMoveAnim("MoveLeft");
        StartCoroutine(MoveTo(new string[] { "W1" }));
    }
    private void DoIntroLaugh()
    {
        animator.SetTrigger("introLaughTrigger");
    }
    #endregion
    private IEnumerator MoveTo(string[] wpNames, bool thenSpawnEnemies = false) // v3ify pero corrutina
    {
        Vector3 target = new Vector3();
        string targetName = "";//viva este lenguaje
        while (false != true)
        {
            for (byte i = 0; i < wpNames.Length; i++)
            {
                for (byte j = 0; j < waypoints.Count; j++)
                {
                    if (waypoints[j].name == wpNames[i])
                    {
                        target = waypoints[j].position;
                        targetName = wpNames[i];
                        break;
                    }
                }
            }
            while (this.transform.position != target)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, target, spd * Time.deltaTime);
                yield return null; //null == esperar al siguiente frame a lo Update
            }
            if (!introDone) //en la intro se mueve a un solo wp antes de reírse, pero además hace otras cosas
            {
                DoIntroLaugh();
                yield return new WaitForSeconds(3f);
                canBeShot = true;
                introDone = true;
                idle = true;
                break;
            }
            else if (thenSpawnEnemies == true)
            {
                GameObject[] pfbsEnemigos = GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().pfbsEnemigos;
                byte cuantos = (byte)Random.Range(1, 4);
                for (byte i = 0; i > cuantos; i++)
                {
                    //animator.SetTrigger("spawnEnemyTrigger");
                    GameObject prefabElegido;
                    byte rie = (byte)Random.Range(0, pfbsEnemigos.Length);
                    prefabElegido = pfbsEnemigos[rie];
                    GameObject nuevoEnemigo = Instantiate(prefabElegido, new Vector3(this.transform.position.x, this.transform.position.y - 1, 0f), Quaternion.identity);
                    nuevoEnemigo.GetComponent<EnemigoScript>().spName = targetName;
                    EnemySpawner.botsVivos.Add(nuevoEnemigo);
                }
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
        canBeShot = false;
        idle = false;
        GameObject padreSpawners = GameObject.Find("Spawners");
        foreach (Transform s in padreSpawners.transform)
        {
            if (s != padreSpawners)
            {
                this.waypoints.Add(s.transform);
            }
        }
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