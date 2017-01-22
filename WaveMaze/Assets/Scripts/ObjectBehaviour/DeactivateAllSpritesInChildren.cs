using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAllSpritesInChildren : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var spriteRs = transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (var renderer in spriteRs)
        {
            renderer.enabled = false;
        }
    }

}
