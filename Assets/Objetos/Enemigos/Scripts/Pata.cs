using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pata : EnemigoScript
{
    [Header("Pata")]
    [SerializeField] private GameObject pfbPatito;

    protected override void Start()
    {
        if (pfbPatito == null) throw new System.Exception("pfbPatito fue null!");
        else base.Start();
    }

    public override void Morir() => StartCoroutine(MorirPata());

    private IEnumerator MorirPata()
    {
        for (byte i = 1; i < 4; i++)
        {
            Patito patito = Instantiate(pfbPatito, this.transform.position, Quaternion.identity).GetComponent<Patito>();
            patito.siguiendo = false;
            List<Vector3> movPatito = new List<Vector3>();
            List<float> animsPatito = new List<float>();
            for(byte j = wi; i < this.v3Camino.Count; i++)
            {
                movPatito.Add(this.v3Camino[j]);
                animsPatito.Add(this.secuenciaAnims[j]);
            }
            patito.v3Camino = movPatito;
            patito.secuenciaAnims = animsPatito;
            patito.siguiendo = true;
            yield return new WaitForSeconds(0.5f);
        }
        base.Morir();
    }

    private string GetWaypointNameFromV3(Vector3 v)
    {
        foreach (Transform w in waypoints)
        {
            if (v == w.position) return w.gameObject.name;
        }
        throw new System.Exception("GetWaypointNameFromV3: No se encontró ese v3 en la lista de waypoints");
    }
}
