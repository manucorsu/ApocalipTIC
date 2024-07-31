using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript2 : MonoBehaviour
{

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
