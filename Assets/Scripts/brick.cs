using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour
{
    public float life;

    public void RestaVida()
    {
        if (life > 1)
        {
            life--;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
