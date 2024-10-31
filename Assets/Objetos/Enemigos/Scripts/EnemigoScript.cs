using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemigoScript : MonoBehaviour
{
    #region Animación
    [Header("Animación")]
    protected Animator animator;
    [HideInInspector] public List<float> secuenciaAnims = new List<float>(); //0 = DOWN; 1 = LEFT; 2 = UP
    protected Color baseColor = Color.white;
    [SerializeField] private Color hurtColor = new Color(217, 54, 54);
    public GameObject explosionMuerte;
    public Color colorExplosion;
    [HideInInspector] public float currentMan;
    #endregion

    #region Enemigos generados por el jefe
    [SerializeField] private Material solidWhite;
    private Material defaultMat;
    #endregion

    #region Stats
    [Header("Stats")]
    public bool isLarge = false;
    [SerializeField] protected float baseHP;
    [HideInInspector] public float hp;
    public bool isBoss;
    public byte minRonda; //algunos enemigos más difíciles solo pueden aparecer en rondas más avanzadas. asignar desde inspector.
    public float spd; //speed
    [HideInInspector] public float spdSave;
    [HideInInspector] public float slowSpd;
    [HideInInspector] public float aceiteSpd;
    public float plata; //cuánta $ recibe el jugador al mater a este enemigo.
    public bool canBeEaten = true;
    public bool canBeShot = true;
    protected SpriteRenderer spriteRenderer;
    public bool isPegamentoed = false;
    public bool isAceitado = false;

    [SerializeField] private bool isSpawnableByJefe;
    public bool IsSpawnableByJefe
    {
        get => isSpawnableByJefe;
        private set => isSpawnableByJefe = value;
    }
    #endregion

    protected ConstruirScriptGeneral construirscr; // ni idea fue Marcos

    #region nav
    private GameObject padreWaypoints; //no es un array porque eso requeriría que cada waypoint sea un prefab
    protected List<Transform> waypoints = new List<Transform>(); //todos los waypoints
    [HideInInspector] public string spName; //en qué spawn point (ubicación) apareció. setteado EnemySpawner SACANDO que sea el jefe, que asigna su propio spawnpoint
    [HideInInspector] public List<Vector3> v3Camino = new List<Vector3>();
    [HideInInspector] public byte wi = 0; //waypoint index
    [HideInInspector] public bool siguiendo = false; //ver final de V3ify()
    [HideInInspector] public Vector3 currentWaypoint;
    [SerializeField] private bool reachedGoal = false;
    #endregion

    protected HPBar hpBar;

    private void Awake()
    {
        AsignarTodo();
    }
    protected virtual void Start()
    {
        if (!isBoss) BuscarPath();
    }

    protected virtual void AsignarTodo() //asigna todos los valores que no quería asignar desde el inspector
    {
        StopAllCoroutines();
        EnemySpawner.botsVivos.Add(this.gameObject);
        hp = baseHP;
        hpBar = GetComponentInChildren<HPBar>();
        if (hpBar != null) hpBar.max = baseHP;
        slowSpd = spd / 4;
        aceiteSpd = spd * Aceite.buff;
        v3Camino.Clear();
        secuenciaAnims.Clear(); //cuenta como asignación? 
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
        padreWaypoints = GameObject.Find("PadreWaypoints");
        defaultMat = new Material(Shader.Find("Sprites/Default"));
        foreach (Transform hijo in padreWaypoints.transform)
        {
            if (hijo != padreWaypoints) waypoints.Add(hijo.transform);
        }
        spdSave = this.spd;
        construirscr = GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>();
        if (construirscr == null) Debug.LogError("construirscr fue null en EnemigoScript!!");
        isPegamentoed = false;
    }
    private IEnumerator BossMinionMove(string[] path, bool flash = true)
    {
        canBeShot = false;
        canBeEaten = false;
        this.gameObject.tag = "Untagged";
        this.gameObject.layer = 0;
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        while (false != true)
        {
            for (byte i = 1; i < 5; i++)
            {
                sr.material = solidWhite;
                yield return new WaitForSeconds(1 / i);
                sr.material = defaultMat;
                yield return new WaitForSeconds(1 / (2 * i));
            }
            break;
        }
        V3ify(path);
        yield return new WaitForSeconds(1.5f);
        canBeShot = true;
        canBeEaten = true;
        this.gameObject.tag = "enemigo";
        this.gameObject.layer = 8;
    }

    private void BuscarPath()
    {
        /*Si no se entiende nada, ver SPAWNERSGUIDE*/

        if (spName == "A1" || spName == "A4" || spName == "A7")
        {
            secuenciaAnims.Add(0);
            V3ify(new string[] { "G1" });
        }
        else if (spName == "A2" || spName == "A5" || spName == "A8")
        {
            secuenciaAnims.Add(0);
            V3ify(new string[] { "G2" });
        }
        else if (spName == "A3" || spName == "A6" || spName == "A9")
        {
            secuenciaAnims.Add(0);
            V3ify(new string[] { "G3" });
        }
        else if (spName == "B1" || spName == "B3" || spName == "B5")
        {
            secuenciaAnims.Add(0); secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "W1", "W5", "G3" });
        }
        else if (spName == "B2" || spName == "B4" || spName == "B6")
        {
            secuenciaAnims.Add(0); secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "W2", "W5", "G3" });
        }
        else if (spName == "BL1" || spName == "BL2")
        {
            secuenciaAnims.Add(0); secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "J4", "J1", "G2" });
        }
        else if (spName == "C1" || spName == "C2" || spName == "C3")
        {
            secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "W5", "G3" });
        }
        else if (spName == "C4" || spName == "C5" || spName == "C6")
        {
            secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "W6", "G2" });
        }
        else if (spName == "CL1" || spName == "CL2")
        {
            secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "J1", "G2" });
        }
        else if (spName == "D1" || spName == "D3" || spName == "D5")
        {
            secuenciaAnims.Add(2); secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "W3", "W6", "G2" });
        }
        else if (spName == "D2" || spName == "D4" || spName == "D6")
        {
            secuenciaAnims.Add(2); secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "W4", "W6", "G2" });
        }
        else if (spName == "DL1" || spName == "DL2")
        {
            secuenciaAnims.Add(2); secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            V3ify(new string[] { "J9", "J1", "G2" });
        }
        else if (spName == "J2")
        {
            secuenciaAnims.Add(0);
            StartCoroutine(BossMinionMove(new string[] { "G2" }));
        }
        else if (spName == "J13")
        {
            secuenciaAnims.Add(0); secuenciaAnims.Add(1); secuenciaAnims.Add(0);
            StartCoroutine(BossMinionMove(new string[] { "J4", "J1", "G2" }));
        }
    }
    private void V3ify(string[] camino)
    {
        List<Vector3> vl = new List<Vector3>();
        for (int i = 0; i < camino.Length; i++)
        {
            string targetName = camino[i];

            for (int j = 0; j < waypoints.Count; j++)
            {
                if (waypoints[j].name == targetName)
                {
                    vl.Add(waypoints[j].position);
                    break;
                }
            }
        }
        v3Camino = vl;
        siguiendo = true; //activar el update, básicamente
    }
    protected void ChangeManParaNoJefes(float m)
    { //fix rápido a algo que debería haberse hecho como en el jefe
        // cuando empecé el proyecto
        currentMan = m;
        animator.SetFloat("anim", m);
    }
    protected virtual void Update()
    {
        if (siguiendo == true)
        {
            ChangeManParaNoJefes(secuenciaAnims[wi]);
            currentWaypoint = v3Camino[wi];
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint, spd * Time.deltaTime);

            if (transform.position == v3Camino[wi]) wi++;

            if (wi == v3Camino.Count) siguiendo = false;
        }
    }
    private IEnumerator HurtVFX(float d = 0.1f)
    {
        this.spriteRenderer.color = hurtColor;
        yield return new WaitForSeconds(d);
        this.spriteRenderer.color = baseColor;

    }
    public virtual void Sufrir(float dmg)
    { // Sufrir daño causado por PROYECTILES (balas que usan el BalaScript).
      //BAJO NINGUNA CIRCUNSTANCIA usar para balas "especiales" (como el chorro de agua o el proyector)
        StartCoroutine(HurtVFX(0.1f));
        hp -= dmg;
        hpBar.Change(-dmg);
        if (hp <= 0 && !isBoss) Morir();
        else return;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bala2") //el chorro de agua
        {
            return; //(se maneja en BalaScript2)
        }

        else if (collision.gameObject.name == "Bala4") //el proyector
        {
            TorretaScript4 proyector = collision.gameObject.transform.root.gameObject.GetComponent<TorretaScript4>();
            StartCoroutine(Stun(proyector.dps, proyector.stunTime));
        }

        else if (collision.gameObject.CompareTag("ignorar")) //pegamento, bidón.
        {
            return;
        }

        else if (collision.gameObject.CompareTag("Bombucha"))
        {
            BombuchaScript bala = collision.gameObject.GetComponent<BombuchaScript>();
            Sufrir(bala.balaDmg);
            Destroy(bala.gameObject);
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            if (!reachedGoal)
            {
                LoseLife();
            }
        }
        else
        {
            BalaScript bala = collision.gameObject.GetComponent<BalaScript>();
            if (bala != null)
            {
                if (bala.target == transform)
                {
                    Sufrir(bala.balaDmg);
                    Destroy(bala.gameObject);
                }
            }
        }
    }

    public IEnumerator Stun(float daño, float tiempo)
    {
        Ninja ninja = this.gameObject.GetComponent<Ninja>(); //overridear corrutinas es un quilombo
        if (ninja != null && ninja.Invisible) ninja.SetInvis(false, 0.5f);
        Sufrir(daño);
        hp -= daño;
        this.spd = 0;
        yield return new WaitForSeconds(tiempo);
        this.spd = spdSave;
    }


    public virtual void Morir()
    {
        this.spd = 0;
        if (!isBoss && canBeEaten)
        {
            GameObject explosion = Instantiate(explosionMuerte, transform.position, Quaternion.identity);
            explosion.GetComponent<SpriteRenderer>().color = colorExplosion;
        }
        EnemySpawner.botsEliminados++;
        if (!EnemySpawner.isBossFight) EnemySpawner.botsEliminadosRonda++;
        EnemySpawner.botsVivos.Remove(this.gameObject);
        construirscr.plataActual += plata;
        Destroy(this.gameObject);
    }

    private void LoseLife()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.plata = 0;
        this.colorExplosion = colorExplosion = new Color(this.colorExplosion.r, this.colorExplosion.g, this.colorExplosion.b, 0);
        Morir();
        Corazones.instance.LoseLife();
        reachedGoal = true;
    }
}