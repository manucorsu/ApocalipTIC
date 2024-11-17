using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPerdiste : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool red;
    private bool showedGameOverText;
    [SerializeField] private GameObject txtGameOver;
    [SerializeField] private GameObject txtPressAnyKey;

    private void Awake()
    {
        red = false;
        showedGameOverText = false;
    }

    public void TurnRed()
    {
        if (!red)
        {
            red = true;
            animator.SetTrigger("turnRed");
        }
    }

    public void ShowGameOverText()
    {
        if (!showedGameOverText)
        {
            showedGameOverText = true;
            txtGameOver.SetActive(true);
            txtPressAnyKey.SetActive(true);
        }
    }
}