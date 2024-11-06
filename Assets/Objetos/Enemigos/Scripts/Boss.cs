using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemigoScript
{
    [Header("Jefe")]
    private float baseSpd;
    private float fastSpd = 12f;

    public static bool isSpawningEnemies;

    private bool idle = false;

    public bool introDone = false;
    private Button btnDv;
    private Image btnDvImg;
    [SerializeField] private Sprite dvOnSpr;
    [SerializeField] private Sprite dvOffSpr;

    private Dictionary<string, float> moveAnims = new Dictionary<string, float> //viva python
    {
        {"MoveDown", 0f},
        {"MoveLeft", 1f},
        {"MoveRight", 2f},
        {"MoveUp", 3f}
    };

    public bool killMe = false;

    private void Awake()
    {
        isBoss = true;
        AsignarTodo();
    }

    protected override void AsignarTodo()
    {
        base.AsignarTodo();
        if (EnemySpawner.ronda == 15) hpBar.min = baseHP / 2;
        else hpBar.min = 1;
        baseSpd = spd;
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
        explosionMuerte.GetComponent<SpriteRenderer>().sortingOrder = 32767;
        if (EnemySpawner.isBossFight == false) Debug.LogError("El jefe spawneó cuando EnemySpawner.isBossFight era false.");
    }

    protected override void Start()
    {
        DoIntro();
        base.Start();
    }
    private void DoIntro()
    {
        PauseScript.Instance.canPause = false;
        Time.timeScale = 1f;
        btnDv.interactable = false;
        btnDvImg.sprite = dvOffSpr;
        btnDvImg.color = Color.white;
        GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().ActivarConsumibles(false);
        StartCoroutine(MoveTo(new string[] { "J4" }, new string[] { "MoveLeft" }, false));
    }

    private IEnumerator MoveTo(string[] wpNames, string[] mans, bool thenSpawnEnemies, string finalAnim = "MoveDown", float waitTime = 3f, bool thenGoBack = true) // v3ify pero corrutina
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
                animator.SetTrigger("introLaughTrigger");
                yield return new WaitForSeconds(3f); //por supuesto que esta animación iba a estar hardcodeada y fea
                canBeShot = true;
                introDone = true;
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

                GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().ActivarConsumibles(true);
                PauseScript.Instance.canPause = true;
                break;
            }
            if (thenSpawnEnemies == true)
            {
                GameObject[] pfbsEnemigos = GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().pfbsEnemigos;
                int cuantos = Random.Range(3, 7);
                animator.SetBool("spawnEnemy", true);
                while (!isSpawningEnemies) yield return null;
                if (isSpawningEnemies)
                {
                    for (byte i = 0; i < cuantos; i++)
                    {
                        GameObject prefabElegido;
                        while (false != true)
                        {
                            byte rie = (byte)Random.Range(0, pfbsEnemigos.Length);
                            prefabElegido = pfbsEnemigos[rie];
                            if (prefabElegido.GetComponent<EnemigoScript>().IsSpawnableByJefe) break;
                            yield return null;
                        }
                        GameObject nuevoEnemigo = Instantiate(prefabElegido, new Vector3(this.transform.position.x, (this.transform.position.y - 2), 0f), Quaternion.identity);
                        EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
                        enemigoScript.spName = targetName;
                        while (false != true)
                        {
                            if (enemigoScript.canBeShot) break;
                            yield return null;
                        }
                        yield return new WaitForSeconds(1.5f);
                    }
                    animator.SetBool("spawnEnemy", false);
                    while (isSpawningEnemies) yield return null;
                }
            }
            SetMoveAnim(finalAnim);
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
                                this.transform.position = Vector3.MoveTowards(this.transform.position, target, spd * Time.deltaTime);
                                yield return null; //null == esperar al siguiente frame a lo Update
                            }
                            SetMoveAnim(FindOppositeMoveAnim(mans[i]));
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

    private string FindOppositeMoveAnim(string an)
    {
        switch (an)
        {
            case "MoveDown": return "MoveUp";
            case "MoveLeft": return "MoveRight";
            case "MoveRight": return "MoveLeft";
            case "MoveUp": return "MoveDown";
            default:
                Debug.LogError($"{an} no es una man válida");
                return "MoveDown";
        }
    }
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

    protected override void Update()
    {
        if (EnemySpawner.ronda == 15 && hp < baseHP / 2) hp = baseHP / 2;
        else if (hp < 1) hp = 1;

        if (idle)
        {
            if (hp <= (baseHP / 2) && !killMe) StartCoroutine(Marearse());
            else if (killMe && hp <= 1) StartCoroutine(Marearse());
            else DoRandomBehaviour();
        }
        if (killMe && EnemySpawner.ronda == 15)
        {
            this.spd = 8f;
            this.fastSpd = 8f;
        }
    }

    private void DoRandomBehaviour()
    {
        idle = false;
        this.spd = this.baseSpd;
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0: //spawnear enemigos cerca de la entrada
                StartCoroutine(MoveTo(
                    new string[] { "J4", "J1", "J2" },
                    new string[] { "MoveLeft", "MoveLeft", "MoveUp" },
                    true, "MoveDown"));
                break;
            case 1: // Ver case 1 jefe.jpg.
                int randSGroup = Random.Range(0, 12);
                switch (randSGroup)
                {
                    case 0: // A -> B
                        fastSpd = 4;
                        StartCoroutine(MoveTo(
                            new string[] { "J1", "A8", "J3", "J4" },
                            new string[] { "MoveLeft", "MoveUp", "MoveRight", "MoveDown" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 1: // A -> C
                        fastSpd = 12;
                        StartCoroutine(MoveTo(
                            new string[] { "J1", "A8", "J10", "J5", "C2", "J4" },
                            new string[] { "MoveLeft", "MoveUp", "MoveRight", "MoveRight", "MoveDown", "MoveLeft" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 2: // A -> D
                        fastSpd = 12;
                        StartCoroutine(MoveTo(
                            new string[] { "J1", "A8", "J6", "J7", "J14", "J8", "J9", "J4" },
                            new string[] { "MoveLeft", "MoveUp", "MoveLeft", "MoveDown", "MoveRight", "MoveRight", "MoveUp", "MoveLeft" },
                            false, "MoveDown", 3, false
                            ));
                        break;

                    case 3: // B -> A
                        fastSpd = 4;
                        StartCoroutine(MoveTo(
                            new string[] { "J3", "A8", "J1", "J4" },
                            new string[] { "MoveUp", "MoveLeft", "MoveDown", "MoveRight" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 4: // B -> C
                        fastSpd = 8;
                        StartCoroutine(MoveTo(
                            new string[] { "J10", "J5", "C2", "J4" },
                            new string[] { "MoveUp", "MoveRight", "MoveDown", "MoveLeft" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 5: // B -> D
                        fastSpd = 12;
                        StartCoroutine(MoveTo(
                            new string[] { "J10", "J5", "J11", "J12", "J8", "J9", "J4" },
                            new string[] { "MoveUp", "MoveRight", "MoveDown", "MoveDown", "MoveLeft", "MoveUp", "MoveLeft" },
                            false, "MoveDown", 3, false
                            ));
                        break;

                    case 6: // C -> A
                        fastSpd = 12;
                        StartCoroutine(MoveTo(
                            new string[] { "C2", "J5", "J10", "A8", "J1", "J4" },
                            new string[] { "MoveRight", "MoveUp", "MoveLeft", "MoveLeft", "MoveDown", "MoveRight" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 7: // C -> B
                        fastSpd = 8;
                        StartCoroutine(MoveTo(
                            new string[] { "C2", "J5", "J10", "J4" },
                            new string[] { "MoveRight", "MoveUp", "MoveLeft", "MoveDown" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 8: // C -> D
                        fastSpd = 8;
                        StartCoroutine(MoveTo(
                            new string[] { "C3", "J12", "J8", "J9", "J4" },
                            new string[] { "MoveRight", "MoveDown", "MoveLeft", "MoveUp", "MoveLeft" },
                            false, "MoveDown", 3, false
                            ));
                        break;

                    case 9: // D -> A
                        fastSpd = 12;
                        StartCoroutine(MoveTo(
                            new string[] { "J9", "J8", "J14", "J7", "J6", "A5", "J1", "J4" },
                            new string[] { "MoveRight", "MoveDown", "MoveLeft", "MoveLeft", "MoveUp", "MoveRight", "MoveDown", "MoveRight" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 10: // D -> B
                        fastSpd = 12;
                        StartCoroutine(MoveTo(
                            new string[] { "J9", "J8", "J12", "J11", "J5", "J3", "J4" },
                            new string[] { "MoveRight", "MoveDown", "MoveRight", "MoveUp", "MoveLeft", "MoveLeft", "MoveDown" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                    case 11: // D -> C
                        fastSpd = 8;
                        StartCoroutine(MoveTo(
                            new string[] { "J9", "J8", "J12", "C3", "J4" },
                            new string[] { "MoveRight", "MoveDown", "MoveRight", "MoveUp", "MoveLeft" },
                            false, "MoveDown", 3, false
                            ));
                        break;
                }
                break;
            case 2: //to do: spawnear enemigos en B
                StartCoroutine(MoveTo(new string[] { "J4", "J13" }, new string[] { "MoveUp", "MoveUp" },
                    true, "MoveDown", 3, true));
                break;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("bits") && introDone)
        {
            if (canBeShot)
            {
                this.spd = fastSpd;
                canBeShot = false;
            }
            else if (!idle)
            {
                this.spd = baseSpd;
                canBeShot = true;
            }
        }
        else if (collision.gameObject.CompareTag("BossKiller") && killMe) StartCoroutine(BossMorir());
    }

    private IEnumerator Marearse()
    {
        idle = false;
        animator.SetTrigger("goDizzy");
        yield return new WaitForSeconds(4.6666667f);
        killMe = true;
        if (EnemySpawner.ronda == 15) Escapar();
        else
        {
            if (hp <= 1)
            {
                Time.timeScale = 1;
                btnDv.image.sprite = dvOffSpr;
                btnDv.interactable = false;
                StartCoroutine(BossMorir());
            }
            else idle = true;
        }
    }

    private void Escapar()
    {
        spd = 8f;
        fastSpd = 8f;
        StartCoroutine(MoveTo(
            new string[] { "C3", "J11" },
            new string[] { "MoveRight", "MoveRight" },
            false
            ));
        return;
    }

    public IEnumerator BossMorir()
    {
        EnemySpawner.botsASpawnear = 0;
        for (int i = EnemySpawner.botsVivos.Count - 1; i >= 0; i--)
        {
            GameObject enemigo = EnemySpawner.botsVivos[i];
            if (enemigo != this.gameObject)
            {
                EnemigoScript enemigoScript = enemigo.GetComponent<EnemigoScript>();
                if (enemigoScript != null)
                {
                    Ninja ninja = enemigo.GetComponent<Ninja>();
                    if (ninja != null) ninja.SetInvis(false, 0.1f);
                    enemigoScript.Morir();
                }
            }
        }
        if (EnemySpawner.ronda == 30)
        {
            animator.SetTrigger("die");
            yield return new WaitForSeconds(1.1666667f);
            for (byte i = 0; i <= 10; i++)
            {
                GameObject explosion = Instantiate(explosionMuerte, this.transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector2(1.5f, 1.5f);
                float x = Random.Range(-1, 3); float y = Random.Range(-1, 3);
                explosion.transform.position = new Vector3(this.transform.position.x - x,
                                                           this.transform.position.y - y);
                explosion.GetComponent<SpriteRenderer>().color = colorExplosion;
                yield return new WaitForSeconds(0.1f);
            }
            SoundManager.Instance.GetComponent<AudioSource>().clip = GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().musica[2];
            SoundManager.Instance.GetComponent<AudioSource>().loop = false;
            SoundManager.Instance.GetComponent<AudioSource>().Play();

            SoundManager.Instance.StopAllSfxLoops();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
        else
        {
            this.Morir();
            EnemySpawner.botsEliminados--; //en la historia el jefe no muere en la ronda 15.
                                           //EnemigoScript incrementa el counter igual así
                                           //que con esto deshago eso y mantengo intacto mi precioso lore
            GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().TerminarRonda();
        }
    }
}