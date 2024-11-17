using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vein : EnemigoScript
{
    [Header("Vein")]
    [SerializeField] private Color healColor; //flashea este color cuando se cura
    [SerializeField] private CustomRangeFloat waitTime = new CustomRangeFloat(0, 999, 0); // cada cuánto se cura
    [SerializeField] private CustomRangeFloat divHP = new CustomRangeFloat(1, 999, 0); // la cantidad que se cura es baseHP dividido esto

    protected override void Start()
    {
        base.Start();
        StartCoroutine(VeinHealing((baseHP / divHP), waitTime));
    }

    private IEnumerator VeinHealing(float healAmount, float wt)
    {
        while (false != true)
        {
            if ((hp + healAmount) <= baseHP)
            {
                sr.color = healColor;
                hp += healAmount;
                hpBar.Change(healAmount);
                yield return new WaitForSeconds(0.1f);
                sr.color = baseColor;
            }
            yield return new WaitForSeconds(wt);
        }
    }
}
