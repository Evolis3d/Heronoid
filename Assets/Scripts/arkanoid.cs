using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arkanoid : MonoBehaviour
{
    public Transform pala;
    public float palaspeed;
    private Vector3 palapos;
    
    private Camera cam;
    

    // Start is called before the first frame update
    void Awake()
    {
       cam = Camera.main; 
    }

    void Start()
    {
        palapos = pala.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR  
            pala.transform.Translate(Input.GetAxis("Horizontal") * palaspeed * Time.deltaTime, 0, 0);
            if (Input.GetKeyDown(KeyCode.Escape)) UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        
        #if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount >= 1)
            {
                var newpos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                newpos *= Vector2.right;
                palapos = newpos;
                pala.transform.position = palapos;
            }
        #endif
        
    }
}
