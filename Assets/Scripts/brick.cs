using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour

{
    public float life;
    private LevelController levComp;

    void Awake()
    {
        levComp = FindObjectOfType<LevelController>();
    }

    public void RestaVida()
    {
        if (life > 1)
        {
            life--;
        }
        else
        {
            //lo quito del Levelcontroller antes de destruirlo
            levComp.RemoveBrick(this.transform);
            Destroy(this.gameObject);
        }
    }
}
