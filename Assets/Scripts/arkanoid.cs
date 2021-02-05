using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using mierdergames;

[RequireComponent(typeof(LevelController),typeof(PalaController))]
public class arkanoid : MonoBehaviour
{
    private LevelController _levComp;
    private PalaController _palaComp;

    // Start is called before the first frame update
    void Awake()
    {
        _levComp = GetComponent<LevelController>();
        _palaComp = GetComponent<PalaController>();
    }

    void Start()
    {
        //de momento lo hago todo aquí, a saco...
        
        _levComp.InitLevel += StartPreAlgo;
        _levComp.LevelStarted += StartCurrentLevel;
        _levComp.LevelCleared += NextLevel;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void StartPreAlgo()
    {
        print("Nivel preparado!");
    }

    private void StartCurrentLevel()
    {
        print("Level Started!");
        _palaComp.InitPala();
        _palaComp.EnableControl();
    }

    private void NextLevel()
    {
        //de momento, repetimos el mismo level...
        SceneManager.LoadScene(0);
    }

    void OnDestroy()
    {
        _levComp.InitLevel -= StartPreAlgo;
        _levComp.LevelStarted -= StartCurrentLevel;
        _levComp.LevelCleared -= NextLevel;
    }
}
