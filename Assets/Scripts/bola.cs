using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mierdergames.libs;

public class bola : MonoBehaviour
{
    private Transform camtransform;
    private Shake shakeComp;
    
    Vector2 balldir;
    public float ballspeed;

    void Awake()
    {
        if (!camtransform) camtransform = Camera.main.transform;
        shakeComp = FindObjectOfType<Shake>();
    }

    // Start is called before the first frame update
    void Start()
    {
        balldir = Vector2.one;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(balldir.x * ballspeed * Time.deltaTime, balldir.y * ballspeed * Time.deltaTime, 0);
    }


    private void OnCollisionEnter2D(Collision2D paco)
    {
        if (paco.transform.CompareTag("muros"))
        {
            var choque = paco.contacts[0].normal;

            //rebota o refleja la dirección en la normal opuesta a la dada
            balldir = Vector2.Reflect(balldir, choque);
        }

        else if (paco.transform.CompareTag("bricks"))
        {
            paco.transform.GetComponent<brick>().RestaVida();

            //rebota o refleja la dirección en la normal opuesta a la dada
            var choque = paco.contacts[0].normal;
            balldir = Vector2.Reflect(balldir, choque);

            FastBall();
            
            //prueba de shaking
            StartCoroutine(shakeComp.Shakeit(camtransform,  new Vector2(0.1f,0.075f) ) );
        }
    }

    private void OnTriggerEnter2D(Collider2D paco)
    {
        if (paco.transform.CompareTag("death"))
        {
            ResetBall();
        }
    }


    public void ResetBall()
    {
        transform.position = Vector3.zero;
        ballspeed = 2f;
        balldir = Vector2.one;
    }


    public void FastBall()
    {
        ballspeed += 0.1f;
    }

}
