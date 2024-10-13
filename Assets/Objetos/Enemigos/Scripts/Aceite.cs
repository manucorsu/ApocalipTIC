using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aceite : MonoBehaviour
{
    public static float buff = 4.5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemigoScript enemigo = collision.gameObject.GetComponent<EnemigoScript>();
        if (enemigo != null && enemigo.isPegamentoed == false && enemigo.canBeEaten) 
            //no afecta al jefe, feature not bug
        {
            enemigo.spd = enemigo.aceiteSpd;
            enemigo.isAceitado = true;
            StartCoroutine(ExistirAceite());
        }
    }

    private IEnumerator ExistirAceite()
    {
        yield return new WaitForSeconds(0.666667f + 15f);
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        Color newColor = sr.color;
        while (newColor.a > 0)
        {
            newColor.a -= 0.02f;
            sr.color = newColor;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemigoScript enemigoScr = collision.gameObject.GetComponent<EnemigoScript>();
        if (enemigoScr != null)
        {
            if (enemigoScr.canBeEaten == true)
            {
                if (enemigoScr.isAceitado)
                {
                    enemigoScr.spd = enemigoScr.spdSave;
                    enemigoScr.isAceitado = false;
                }
            }
        }
    }
}