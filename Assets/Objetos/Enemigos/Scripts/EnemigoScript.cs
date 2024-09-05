using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoScript : MonoBehaviour
{

    #region anim
    protected Animator animator;
    protected List<float> secuenciaAnims = new List<float>(); //0 = DOWN; 1 = LEFT; 2 = UP
    #endregion

    #region stats
    [Header("Stats")]
    public bool isBoss;
    public byte minRonda; //algunos enemigos más difíciles solo pueden aparecer en rondas más avanzadas. asignar desde inspector.
    public float hp;
    public float spd; //speed
    [HideInInspector] public float spdSave;
    public float plata; //cuánta $ recibe el jugador al mater a este enemigo.
    public bool canBeEaten = true;
    public bool canBeShot = true;
    #endregion

    [HideInInspector] private ConstruirScriptGeneral construirscr; // ni idea fue Marcos

    #region nav
    private GameObject padreWaypoints; //no es un array porque eso requeriría que cada waypoint sea un prefab
    protected List<Transform> waypoints = new List<Transform>(); //todos los waypoints
    public string spName; //en qué spawn point (ubicación) apareció. setteado EnemySpawner SACANDO que sea el jefe, que asigna su propio spawnpoint
    protected List<Vector3> v3Camino = new List<Vector3>();
    protected byte wi = 0; //waypoint index
    protected bool siguiendo = false; //ver final de V3ify()
    #endregion

    private IEnumerator sufrirNicho; private float sufrirNichoDPS; private float nichoCooldown;


    void Awake()
    {
        AsignarTodo();
    }

    void Start()
    {
        if (spName == null || spName == string.Empty && !isBoss)
        {
            Debug.LogError("spName == null o string.Empty. En general esto pasa cuando un enemigo no fue generado por EnemySpawner.");
            Destroy(this.gameObject); //ÚNICA vez en toda la HISTORIA donde un enemigo se destruye directamente y no llamando a Morir()
        }
        BuscarPath();
    }

    protected virtual void AsignarTodo() //asigna todos los valores que no quería asignar desde el inspector
    {
        secuenciaAnims.Clear(); //cuenta como asignación? 
        animator = this.gameObject.GetComponent<Animator>();
        padreWaypoints = GameObject.Find("PadreWaypoints");
        foreach (Transform hijo in padreWaypoints.transform)
        {
            if (hijo != padreWaypoints)
            {
                waypoints.Add(hijo.transform);
            }
        }
        spdSave = this.spd;
        construirscr = GameObject.Find("SCENESCRIPTS").GetComponent<ConstruirScriptGeneral>();
        if (construirscr == null) Debug.LogError("construirscr fue null en EnemigoScript!!");
    }

    private void BuscarPath()
    {
        /*jajaja no puedo usar switch porque apareció en 2019 y unity 2018 no lo acepta 
         jajajaja

        Si no se entiende nada, ver SPAWNERSGUIDE*/

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

    void Update()
    {
        if (siguiendo == true)
        {
            animator.SetFloat("anim", secuenciaAnims[wi]);
            this.transform.position = Vector3.MoveTowards(this.transform.position, v3Camino[wi], spd * Time.deltaTime);

            if (transform.position == v3Camino[wi])
            {
                wi++;
            }

            if (wi == v3Camino.Count)
            {
                Perder();
            }
        }
    }

    public virtual void Sufrir(float dmg)
    { // Sufrir daño causado por PROYECTILES (balas que usan el BalaScript).
      //BAJO NINGUNA CIRCUNSTANCIA usar para balas "especiales" (como el chorro de agua o el proyector)
        hp -= dmg;
        if (hp <= 0) Morir();
        else { return; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bala2") //el chorro de agua
        {
            TorretaScript2 nicho = collision.gameObject.transform.root.gameObject.GetComponent<TorretaScript2>();
            sufrirNichoDPS = nicho.dps; sufrirNicho = SufrirNicho();
            nichoCooldown = nicho.cooldown;
            StartCoroutine(sufrirNicho);
        }

        else if (collision.gameObject.name == "Bala4") //el proyector
        {
            TorretaScript4 proyector = collision.gameObject.transform.root.gameObject.GetComponent<TorretaScript4>();
            StartCoroutine(Stun(proyector.dps, proyector.stunTime));
        }

        else if (collision.gameObject.tag == "ignorar") //pegamento, bidón.
        {
            return;
        }

        else
        {
            BalaScript bala = collision.gameObject.GetComponent<BalaScript>();
            Sufrir(bala.balaDmg);
            Destroy(bala.gameObject);
        }
    }

    private IEnumerator SufrirNicho()
    {
        while (false != true)
        {
            yield return new WaitForSeconds(nichoCooldown);
            hp -= sufrirNichoDPS;
            if (hp <= 0)
            {
                Morir();
                break;
            }
        }
    }

    public IEnumerator Stun(float daño, float tiempo)
    {
        float spdSave = this.spd;
        hp -= daño;
        if (hp <= 0)
        {
            Morir();
        }
        this.spd = 0;
        yield return new WaitForSeconds(tiempo);
        this.spd = spdSave;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bala2")
        {
            if (sufrirNicho != null)
            {
                StopCoroutine(sufrirNicho);
            }
        }
    }

    public virtual void Morir()
    {
        this.spd = 0;
        EnemySpawner.botsVivos.Remove(this.gameObject);
        construirscr.plataActual += plata;
        Destroy(this.gameObject);
    }

    private void Perder()
    {
        Morir();
        Debug.LogWarning("PERDISTE");
        SceneManager.LoadScene("GameOver");
    }
}