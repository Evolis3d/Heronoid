using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace mierdergames
{
    [Serializable]
    public enum Palapos
    {
      Left = 0,
      Center = 1,
      Right = 2,
    }

    [CreateAssetMenu(fileName = "Personajito_XX", menuName = "Heronoid/Personajitos/New Personajito...")]
    [Serializable]
    public class SO_Personajito : ScriptableObject
    {
        [SerializeField] public Palapos tipoPala;
        [SerializeField] public GameObject prefabPj;
    }
}
