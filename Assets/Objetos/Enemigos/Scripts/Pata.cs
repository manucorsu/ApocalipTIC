using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pata : EnemigoScript
{
    [Header("Pata")]
    [SerializeField] private GameObject pfbPatito;
    [HideInInspector] public bool canDie = false;
    protected override void Start()
    {
        if (pfbPatito == null) throw new System.Exception("pfbPatito fue null!");
        base.Start();
    }

    public override void Morir() => StartCoroutine(MorirPata());
    private IEnumerator MorirPata()
    {
        while (canDie == false)
        {
            if (hp < 1) hp = 1;
            yield return null;
        }
        this.spd = 0;
        for (byte i = 1; i < 4; i++)
        {
            EnemigoScript patito = Instantiate(pfbPatito, this.transform.position, Quaternion.identity).GetComponent<EnemigoScript>();
            patito.spName = GetWaypointNameFromV3(currentWaypoint);
            yield return new WaitForSeconds(1);
        }
        base.Morir();
    }

    private string GetWaypointNameFromV3(Vector3 v)
    {
        foreach(Transform w in waypoints)
        {
            if (v == w.position) return w.gameObject.name;
        }
        return "W2";
    }
}
