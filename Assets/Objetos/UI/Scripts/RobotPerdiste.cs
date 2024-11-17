
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotPerdiste : MonoBehaviour
{
    [SerializeField] private CustomRangeFloat speed = new CustomRangeFloat(0, float.MaxValue, 0);
    [SerializeField] private RectTransform target;
    [SerializeField] private GameObject pcPerdiste;

    private bool move = false;
    private RectTransform tr;

    public void Go(EnemyType enemyT, Sprite spr, Color imgColor)
    //a mí nadie me dice que no puedo hacer un constructor
    {
        move = false;
        Image thisImg = GetComponent<Image>();
        thisImg.sprite = spr;
        thisImg.color = imgColor;
        tr = this.gameObject.GetComponent<RectTransform>();
        StartCoroutine(AnimateImage(GetMoveDownAnim(enemyT), 0.6f));
        move = true;
    }


    private void Update()
    {
        if (move)
        {
            tr.anchoredPosition = Vector3.MoveTowards(tr.anchoredPosition, target.anchoredPosition, speed * Time.deltaTime);
            if (Vector3.Distance(tr.anchoredPosition, target.anchoredPosition) <= 0.1f)
            {
                pcPerdiste.GetComponent<PCPerdiste>().TurnRed();
            }
            if(tr.anchoredPosition == target.anchoredPosition)
            {
                move = false;
            }
        }
    }

    private Sprite[] GetMoveDownAnim(EnemyType enemyT)
    //devuelve un array con los dos frames de animación de moverse para abajo del enemigo
    //para hardcodear después la "animación" (corrutina que cambia de frame cada x cantidad de segundos)
    //créanme que ya intenté cosas menos feas pero no se pudo
    {
        Sprite[] spritesheet = Resources.LoadAll<Sprite>($"EnemySprites/{enemyT}");

        //hardcodeado y feo, sí señor
        if (enemyT == EnemyType.Pata || enemyT == EnemyType.Patito || enemyT == EnemyType.Pulpo || enemyT == EnemyType.Vein)
        {
            return new Sprite[] { spritesheet[2], spritesheet[3] };
        }
        else return new Sprite[] { spritesheet[0], spritesheet[1] };
    }

    private IEnumerator AnimateImage(Sprite[] anim, float waitTime)
    {
        int i = 0;
        Image thisImg = GetComponent<Image>();

        while (false != true)
        {
            thisImg.sprite = anim[i];
            i++;
            if (i >= anim.Length) i = 0;
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }
}