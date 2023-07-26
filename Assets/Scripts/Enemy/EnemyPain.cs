using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPain : MonoBehaviour
{
    private Animator animator;
    private float flashTime= 0.5f;
    private Color normalColor;
    private MeshRenderer Renderer;

    private void Start()
    {
        normalColor = Renderer.material.color;
    }

    private void FlashRed()
    {
        Renderer.material.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        Renderer.material.color = normalColor;
    }

    public void HandlePain(int dmg)
    {
        FlashRed();
    }

    public void HandleDeath()
    {
        animator.SetTrigger("Dead");
    }
}
