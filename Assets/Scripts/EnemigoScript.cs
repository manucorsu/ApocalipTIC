using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoScript : MonoBehaviour
{

    public bool canBeEaten = true;

    private Animator animator;
    private List<float> secuenciaAnims = new List<float>(); //0 = DOWN; 1 = LEFT; 2 = UP

    [Header("Stats")]
    [SerializeField] private byte minRonda; //algunos enemigos más difíciles solo pueden aparecer en rondas más avanzadas
    [SerializeField] private float hpSave;
    public float hp;
    public float spd; //speed


    private GameObject padreWaypoints; //no es un array porque eso requeriría que cada waypoint sea un prefab
    private List<Transform> waypoints = new List<Transform>(); //todos los waypoints
    public string spName; //en qué spawn point (ubicación) apareció. setteado por EnemySpawner
    private List<Vector3> v3Camino = new List<Vector3>();
    private byte wi = 0; //waypoint index
    private bool siguiendo = false; //ver final de V3ify()

    void Start()
    {
        AsignarTodo();
        BuscarPath();
    }

    private void AsignarTodo() //asigna todos los valores que no quería asignar desde el inspector
    {
        secuenciaAnims.Clear();
        animator = this.gameObject.GetComponent<Animator>();
        padreWaypoints = GameObject.Find("PadreWaypoints");
        foreach (Transform hijo in padreWaypoints.transform)
        {
            if (hijo != padreWaypoints)
            {
                waypoints.Add(hijo.transform);
            }
        }
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
        for (int i = 0; i < camino.Length; i++)
        {
            string targetName = camino[i];

            for (int j = 0; j < waypoints.Count; j++)
            {
                if (waypoints[j].name == targetName)
                {
                    v3Camino.Add(waypoints[j].position);
                    break;
                }
            }
            siguiendo = true; //activar el update, básicamente
        }
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
        if (hp < hpSave)
        {
            float deltaHP = (hpSave - hp);
            StartCoroutine(Sufrir(deltaHP));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bala2") //el chorro de agua
        {
            TorretaScript2 nicho = collision.gameObject.transform.root.gameObject.GetComponent<TorretaScript2>();
            StartCoroutine(Sufrir(nicho.dps, true));
        }

        else if (collision.gameObject.name == "Bala4") //el proyector
        {
            TorretaScript4 proyector = collision.gameObject.transform.root.gameObject.GetComponent<TorretaScript4>();
            StartCoroutine(Stun(proyector.dps, proyector.stunTime));
        }
    }

    private IEnumerator Sufrir(float dmg, bool fromNicho = false) //hace la animación de sufrir daño y cambia la barra de vida
    {
        while (false != true)
        {
            if (fromNicho)
            {
                yield return new WaitForSeconds(1f);
                hp -= dmg;
                hpSave = hp;
                if (hp <= 0)
                {
                    Morir();
                    break;
                }
            }
            else
            {
                yield return 0;
                if (hp <= 0)
                {
                    //Debug.Log("se murió un enemigo");
                    Morir();
                    break;
                }
                else
                {
                    hpSave -= dmg;
                }
            }
        }
    }

    public IEnumerator Stun(float daño, float tiempo)
    {
        float spdSave = spd;
        hp -= daño;
        spd = 0;
        yield return new WaitForSeconds(tiempo);
        spd = spdSave;
    }
    public void Morir()
    {
        EnemySpawner enemySpawner = GameObject.Find("ENEMYSPAWNER").GetComponent<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("El script o GameObject de ENEMYSPAWNER no existe!");
            Destroy(this.gameObject);
        }
        else
        {
            enemySpawner.botsVivos--;
            Destroy(this.gameObject);
        }
    }
    private void Perder()
    {
        Morir();
        Debug.LogWarning("PERDISTE");
        //SceneManager.LoadScene("GameOver");
    }
}