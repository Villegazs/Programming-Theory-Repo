using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredTarget : Target
{
    private MeshRenderer mRenderer;
    private void Start()
    {
        Health = 2;
        mRenderer = GetComponent<MeshRenderer>();
        mRenderer.material.color = Color.gray;
    }

    // Polymorphism
    public override void OnHit()
    {
        base.OnHit();
        if (Health == 1)
        {
            mRenderer.material.color = Color.white;
        }
    }
}
