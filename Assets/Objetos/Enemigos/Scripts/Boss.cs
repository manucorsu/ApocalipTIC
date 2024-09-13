using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemigoScript
{
    private bool idle;
    private bool introDone = false;
    private Dictionary<string, float> moveAnims = new Dictionary<string, float> //viva python
    {
        {"MoveDown", 0f},
        {"MoveLeft", 1f},
        {"MoveRight", 2f},
        {"MoveUp", 3f}
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
    private Button btnDv;
    private Image btnDvImg;
    [SerializeField] private Sprite dvOnSpr;
    [SerializeField] private Sprite dvOffSpr;

    private void DoIntro()
    {
        Time.timeScale = 1f;
        btnDv.interactable = false;
        btnDvImg.sprite = dvOffSpr;
        StartCoroutine(MoveTo(new string[] { "W1" }, new string[] { "MoveLeft" }));
    }
    private void DoIntroLaugh()
    {
        animator.SetTrigger("introLaughTrigger");
    }
    #endregion
    private IEnumerator MoveTo(string[] wpNames, string[] mans, bool thenSpawnEnemies = false) // v3ify pero corrutina
    {
        idle = false;
        List<float> anims = new List<float>();
        if (wpNames.Length != mans.Length)
        {
            Debug.LogError("MoveTo (Boss): el tamaño de wpNames y mans (move anim names) debe ser igual!!");
        }
        else
        {
            foreach (string man in mans)
            {
                if (moveAnims.ContainsKey(man))
                {
                    anims.Add(moveAnims[man]);
                }
                else
                {
                    Debug.LogError($"MoveTo: No existe la key {man} en el diccionario de moveAnims!!");
                }
            }
        }
        Vector3 target = new Vector3();
        string targetName = "";
        while (false != true)
        {
            for (byte i = 0; i < wpNames.Length; i++)
            {
                for (byte j = 0; j < waypoints.Count; j++)
                {
                    if (waypoints[j].name == wpNames[i])
                    {
                        target = waypoints[j].position;
                        while (this.transform.position != target)
                        {
                            SetMoveAnim(mans[i]);
                            this.transform.position = Vector3.MoveTowards(this.transform.position, target, spd * Time.deltaTime);
                            yield return null; //null == esperar al siguiente frame a lo Update
                        }
                        targetName = wpNames[i];
                        break;
                    }
                }
            }

            if (!introDone) //en la intro se mueve a un solo wp antes de reírse, pero además hace otras cosas
            {
                DoIntroLaugh();
                yield return new WaitForSeconds(3f);
                canBeShot = true;
                introDone = true;
                if (scrBotones.dv)
                {
                    Time.timeScale = 2;
                    btnDvImg.sprite = dvOnSpr;
                }
                else Time.timeScale = 1;
                btnDv.interactable = true;
                break;
            }
            else if (thenSpawnEnemies == true)
            {
                GameObject[] pfbsEnemigos = GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().pfbsEnemigos;
                byte cuantos = (byte)Random.Range(1, 4);
                for (byte i = 0; i > cuantos; i++)
                {
                    animator.SetTrigger("spawnEnemyTrigger");
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
        idle = true;
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
        btnDv = GameObject.Find("btnDobleVelocidad").GetComponent<Button>();
        btnDvImg = GameObject.Find("btnDobleVelocidad").GetComponent<Image>();
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
        if (Input.GetKeyDown(KeyCode.S) && idle == true)
        {
            Debug.Log("s");
            StartCoroutine(MoveTo(new string[] { "W1", "W6" }, new string[] { "MoveLeft", "MoveDown" }, true));
        }
    }
}