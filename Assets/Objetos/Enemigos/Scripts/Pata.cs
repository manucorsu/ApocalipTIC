using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pata : EnemigoScript
{
    [Header("Pata")]
    [SerializeField] private GameObject pfbPatito;
    public bool viva = true;
    private List<Patito> patitos = new List<Patito>();

    protected override void Start()
    {
        if (pfbPatito == null) throw new System.Exception("pfbPatito fue null!");
        base.Start();
        StartCoroutine(SpawnearPatitos());
    }

    public override void Morir()
    {
        viva = false;
        foreach (Patito patito in patitos) patito.spd = patito.baseSpd;
        base.Morir();
    }
    private IEnumerator SpawnearPatitos() {
        for (byte i = 1; i < 4; i++)
        {
            Patito patito = Instantiate(pfbPatito, this.transform.position, Quaternion.identity).GetComponent<Patito>();
            patito.madre = this;
            patito.baseSpd = patito.spd;
            patito.spd -= (this.spd/2.5f);
            patitos.Add(patito);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
