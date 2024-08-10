using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript2 : MonoBehaviour
{

    private Transform target;
    public Animator animator;
    public int anim = 1;
    public float info;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("anim", anim);
    }

    public void SetTarget(Transform targetSet)
    {
        target = targetSet;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
    }

    public void AnimationEnd()
    {
        if (anim == 1)
        {
            anim = 2;
        }

        if (anim == 3)
        {
            gameObject.SetActive(false);
        }
    }
   
}
