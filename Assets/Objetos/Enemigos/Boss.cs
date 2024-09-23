using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemigoScript
{
    public static bool isSpawningEnemies;
    private bool idle = false;
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
        btnDvImg.color = Color.white;
        StartCoroutine(MoveTo(new string[] { "W2" }, new string[] { "MoveLeft" }, false));
    }
    private void DoIntroLaugh()
    {
        animator.SetTrigger("introLaughTrigger");
    }
    #endregion
    private IEnumerator MoveTo(string[] wpNames, string[] mans, bool thenSpawnEnemies, string finalAnim = "", float waitTime = 3f, bool thenGoBack = true) // v3ify pero corrutina
    {
        /* Explicación:
         * Se por el camino indicado en el array wpNames, 
         * cambiando a la animación del mismo índice del array mans.
         * Cuando llega a su objetivo final, cambia a la animación finalAnim y espera
         * el tiempo indicado en segundos por el parámetro waitTime (default: 3), antes 
         * de moverse de nuevo al centro del mapa recorriendo el camino dado al revés.
         * (solo si thenGoBack es true)
         * 
         * Al llegar al centro del mapa, espera la mitad de waitTime antes de volver a poner
         * idle en true.*/
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
        Vector3 target;
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
                yield return new WaitForSeconds(3f); //por supuesto que esta animación iba a estar hardcodeada y fea
                canBeShot = true;
                introDone = true;
                BossInvulnToggler.active = true;
                if (scrBotones.dv == 1)
                {
                    Time.timeScale = 2.5f;
                    btnDvImg.sprite = dvOnSpr;
                }
                else if (scrBotones.dv == 2)
                {
                    Time.timeScale = 5;
                    btnDvImg.sprite = dvOnSpr;
                    btnDvImg.color = Color.cyan;
                }
                else Time.timeScale = 1;
                btnDv.interactable = true;
                break;
            }
            if (finalAnim != "")
            {
                SetMoveAnim(finalAnim);
            }
            if (thenSpawnEnemies == true)
            {
                GameObject[] pfbsEnemigos = GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().pfbsEnemigos;
                byte cuantos = (byte)Random.Range(1, 4);
                animator.SetBool("spawnEnemy", true);
                while (!isSpawningEnemies) yield return null;
                if (isSpawningEnemies)
                {
                    for (byte i = 0; i < cuantos; i++)
                    {
                        GameObject prefabElegido;
                        byte rie = (byte)Random.Range(0, pfbsEnemigos.Length);
                        prefabElegido = pfbsEnemigos[rie];
                        GameObject nuevoEnemigo = Instantiate(prefabElegido, new Vector3(this.transform.position.x, (this.transform.position.y - 2), 0f), Quaternion.identity);
                        nuevoEnemigo.GetComponent<EnemigoScript>().spName = targetName;
                        EnemySpawner.botsVivos.Add(nuevoEnemigo);
                        yield return new WaitForSeconds(1);
                    }
                    animator.SetBool("spawnEnemy", false);
                    while (isSpawningEnemies) yield return null;
                }
            }
            yield return new WaitForSeconds(waitTime / 3);
            if (thenGoBack)
            {
                for (int i = (wpNames.Length - 1); i >= 0; i--)
                {
                    for (int j = (waypoints.Count - 1); j >= 0; j--)
                    {
                        if (waypoints[j].name == wpNames[i])
                        {
                            target = waypoints[j].position;
                            while (this.transform.position != target)
                            {
                                switch (mans[i])
                                {
                                    case "MoveDown":
                                        SetMoveAnim("MoveUp");
                                        break;
                                    case "MoveLeft":
                                        SetMoveAnim("MoveRight");
                                        break;
                                    case "MoveRight":
                                        SetMoveAnim("MoveLeft");
                                        break;
                                    case "MoveUp":
                                        SetMoveAnim("MoveDown");
                                        break;
                                    default:
                                        Debug.LogError("Boss: Se encontró una animación válida cuando se quiso volver al centro");
                                        break;
                                }
                                this.transform.position = Vector3.MoveTowards(this.transform.position, target, spd * Time.deltaTime);
                                yield return null; //null == esperar al siguiente frame a lo Update
                            }
                        }
                    }
                }
                SetMoveAnim("MoveDown");
                yield return new WaitForSeconds(waitTime / 3);
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
        if (idle)
        {
            DoRandomBehaviour();
        }
    }

    private void DoRandomBehaviour()
    {
        idle = false;
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0: //spawnear enemigos cerca de la entrada
                StartCoroutine(MoveTo(new string[] { "W2", "J1", "J2" }, new string[] { "MoveLeft", "MoveLeft", "MoveUp" }, true, "MoveDown"));
                break;
            case 1: // Ver case 1 jefe.jpg.
                int randSGroup = Random.Range(0, 1);
                switch (randSGroup)
                {
                    case 0: // A -> B
                        StartCoroutine(MoveTo(
                            new string[] { "J1", "A5", "J3", "W2" },
                            new string[] { "MoveLeft", "MoveUp", "MoveRight", "MoveDown" },
                            false, "MoveDown", 3f, false
                            ));
                        break;
                    case 1: // A -> C
                        break;
                    case 2: // A -> D
                        break;

                    case 3: // B -> A
                        break;
                    case 4: // B -> C
                        break;
                    case 5: // B -> D
                        break;

                    case 6: // C -> A
                        break;
                    case 7: // C -> B
                        break;
                    case 8: // C -> D
                        break;

                    case 9: // D -> A
                        break;
                    case 10: // D -> B
                        break;
                    case 11: // D -> C
                        break;
                }
                break;
            case 2: //to do: spawnear enemigos en B
                break;
            case 3: //to do: destruir alguna torreta
                break;
        }
    }
}