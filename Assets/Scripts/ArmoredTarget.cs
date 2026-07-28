using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredTarget : Target
{
    private MeshRenderer mRenderer;
    protected override void Start()
    {
        Health = 2;
        mRenderer = GetComponent<MeshRenderer>();
        mRenderer.material.color = Color.gray;
        base.Start();
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
