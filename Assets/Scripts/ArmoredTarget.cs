using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
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

    // POLYMORPHISM
    public override void OnHit()
    {
        base.OnHit();
        if (Health == 1)
        {
            mRenderer.material.color = Color.white;
        }
    }
}
