using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mierdergames.libs;

public class bola : MonoBehaviour
{
    //eventos de la bola
    public delegate void NotifyBolaPerdida();  //lleva mucho tiempo sin rebotar contra la pala o un brick...
    public delegate void NotifyBolaMuere();    //cae por abajo...
    public delegate void NotifyBolaEfecto(int efecto);    //la bola cambia al efecto X...
    public delegate void NotifyBolaAburrida(); //lleva tiempo sin tocar un ladrillo...
    //
    public event NotifyBolaPerdida BolaPerdida;
    public event NotifyBolaMuere BolaMuere;
    public event NotifyBolaEfecto BolaEfecto;
    public event NotifyBolaAburrida BolaAburrida;
    
        
    private Transform _camtransform;
    private Shake _shakeComp;
    
    //data de la bola
    [Header("Bola Data")]
    [SerializeField] private Vector2 _balldir;
    [SerializeField] public float ballSpeed;
    [SerializeField] private float _defaultSpeed = 2f;
    [SerializeField] private float _defaultIncrementSpeed = 0.1f;
    
    //metricas
    [SerializeField] private float _timerball; //tiempo que lleva la bola moviendose...
    [SerializeField] private float _limitperdida = 8f; //a partir de 8seg, la considero perdida
    [SerializeField] private bool _isPerdida = false;
    [SerializeField] private float _timerwithoutbreaking; //tiempo que lleva sin romper nada..
    [SerializeField] private float _limitaburrida = 15f; //a partir de 15seg, la considero aburrida
    
    void Awake()
    {
        if (!_camtransform) _camtransform = Camera.main.transform;
        _shakeComp = FindObjectOfType<Shake>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _balldir = Vector2.one;
        ballSpeed = _defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //movimiento
        transform.Translate(_balldir.x * ballSpeed * Time.deltaTime, _balldir.y * ballSpeed * Time.deltaTime, 0);
        
        //metricas de la bola
        if (_isPerdida) return; //y me ahorro todo lo siguiente...

        _timerball += Time.deltaTime;
        _timerwithoutbreaking += Time.deltaTime;
        
        //si pasa tiempo rebotando "raro" es que está perdida...
        if (_timerball > _limitperdida)
        {
            _isPerdida = true;
            BolaPerdida?.Invoke();
        }
        
        //si pasa tiempo sin romper ladrillos, es que está aburrida...
        if (_timerwithoutbreaking > _limitaburrida) BolaAburrida?.Invoke();
    }


    private void OnCollisionEnter2D(Collision2D paco)
    {
        if (paco.transform.CompareTag("pala")) //LA PALA
        {
            ResetTimerBall(); //WIP , habrá que tener en cuenta la bola perdida vertical...
            
            var choque = paco.contacts[0].normal;

            //rebota o refleja la dirección en la normal opuesta a la dada
            _balldir = Vector2.Reflect(_balldir, choque);
        }
        
        if (paco.transform.CompareTag("muros")) //MUROS EXTERIORES
        {
            var choque = paco.contacts[0].normal;

            //si la bola está perdida, le damos algo de chaos a la direccion...
            if (_isPerdida)
            {
                //determino el eje con menor valor y le doy chaos, incremento 0.3f la direccion más leve...
                var eje = Mathf.Min(Mathf.Abs(_balldir.x), Mathf.Abs(_balldir.y));
                _balldir = (eje.Equals(Mathf.Abs(_balldir.x))) //es el eje x?
                    ? new Vector2(_balldir.x + (0.3f * Mathf.Sign(_balldir.x)), _balldir.y)
                    : new Vector2(_balldir.x, _balldir.y + (0.3f * Mathf.Sign(_balldir.y)) ); 
                
                
                _balldir = Vector2.Reflect(_balldir, choque);
                _isPerdida = false;
                ResetTimerBall();
            }
            else
            {
                //rebota o refleja la dirección en la normal opuesta a la dada
                _balldir = Vector2.Reflect(_balldir, choque);
            }
        }

        else if (paco.transform.CompareTag("bricks")) //LOS BRICKS
        {
            ResetTimerBall();
            ResetTimerAburrida();
            
            paco.transform.GetComponent<brick>().RestaVida();

            //rebota o refleja la dirección en la normal opuesta a la dada
            var choque = paco.contacts[0].normal;
            _balldir = Vector2.Reflect(_balldir, choque);

            FastBall();
            
            //prueba de shaking
            StartCoroutine(_shakeComp.Shakeit(_camtransform,  new Vector2(0.1f,0.075f) ) );
        }
    }

    private void OnTriggerEnter2D(Collider2D paco)
    {
        if (paco.transform.CompareTag("death"))
        {
            BolaMuere?.Invoke();
            ResetTimerBall();
            ResetTimerAburrida();
            
            ResetBall();
        }
    }


    public void ResetBall()
    {
        transform.position = Vector3.zero;
        ballSpeed = _defaultSpeed;
        _balldir = Vector2.one;

        _isPerdida = false;
    }


    public void FastBall()
    {
        ballSpeed += _defaultIncrementSpeed;
    }

    
    private void ResetTimerBall()
    {
        _timerball = 0f;
    }

    private void ResetTimerAburrida()
    {
        _timerwithoutbreaking = 0f;
    }

}
