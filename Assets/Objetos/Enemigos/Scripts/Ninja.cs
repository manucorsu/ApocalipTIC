using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : EnemigoScript
{
    [SerializeField] private CustomRangeFloat transparentAlpha = new CustomRangeFloat(0, 1, 0);
    public bool Invisible { get; private set; }

    protected override void Start()
    {
        base.Start();
        Debug.Log("Apareció un Ninja.");
        SetInvis(true, 0);
    }

    public void SetInvis(bool status, float time)
    {
        StartCoroutine(InvisCoroutine(status, time));
    }

    private IEnumerator InvisCoroutine(bool status, float time)
    {
        this.hpBar.SetActive(!status);
        switch (status)
        {
            case true:
                canBeEaten = false;
                canBeShot = false;
                this.gameObject.tag = "Untagged";
                this.gameObject.layer = 0;
                break;
            case false:
                canBeEaten = true;
                canBeShot = true;
                this.gameObject.tag = "enemigo";
                this.gameObject.layer = 8;
                break;
        }
        Invisible = status;
        if(time > 0)
        {
            float elapsedTime = 0;
            while(elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;

                float newTransparency;
                if(status == true) newTransparency = Mathf.Lerp(1, transparentAlpha, elapsedTime/time);
                else newTransparency = Mathf.Lerp(transparentAlpha,1, elapsedTime/time);

                ChangeSpriteRendererAlpha(newTransparency);

                yield return null;
            }
        }
        if (status == true) ChangeSpriteRendererAlpha(transparentAlpha);
        else ChangeSpriteRendererAlpha(1);
    }

    public void ChangeSpriteRendererAlpha(float alpha)
    {
        CustomRangeFloat a = new CustomRangeFloat(0, 1, alpha);
        this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, a);
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetInvis(!Invisible, 1);
        }
        base.Update();
    }
}
