using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arkanoid : MonoBehaviour
{
    public Transform pala;
    public float palaspeed;
    private Vector3 palapos;
    private float Ypos;
    
    private Camera cam;

    private LevelController levComp;
    

    // Start is called before the first frame update
    void Awake()
    {
       cam = Camera.main;
       levComp = FindObjectOfType<LevelController>();
       Ypos = pala.position.y;
    }

    void Start()
    {
        levComp.InitLevel += StartPreAlgo;
        levComp.LevelStarted += StartCurrentLevel;
        levComp.LevelCleared += NextLevel;
        
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
                //palapos = new Vector3(newpos.x, palapos.y,0);
                palapos = new Vector3(newpos.x,Ypos,0);

                pala.transform.position = palapos;
            }
        #endif
        
    }

    private void StartPreAlgo()
    {
        print("Nivel preparado!");
    }

    private void StartCurrentLevel()
    {
        print("Level Started!");
        palapos = pala.transform.position;  
    }

    private void NextLevel()
    {
        //de momento, repetimos el mismo level...
        SceneManager.LoadScene(0);
    }

    void OnDestroy()
    {
        levComp.InitLevel -= StartPreAlgo;
        levComp.LevelStarted -= StartCurrentLevel;
        levComp.LevelCleared -= NextLevel;
    }
}
