//clase generica que define un skill base.

using System;
using UnityEditor;
using UnityEngine;

namespace mierdergames.atomics
{

    #region clase base

    [Serializable]
    public abstract class BaseSkill
    {
        [SerializeField] public string name;
        [SerializeField] public bool enabled;

        protected BaseSkill(string nam)
        {
            name = nam;
            enabled = false;
        }

        protected BaseSkill()
        {
            name = "test skill";
            enabled = false;
        }

        public virtual void ApplySkill()
        {
            enabled = true;
        }
    }
    #endregion

    #region skill pasiva
    
    [Serializable]
    public class PassiveSkill : BaseSkill
    {
        [SerializeField] public bool isTogglable; //se puede desactivar cuando se quiera?

        public PassiveSkill(string nam, bool istoggle = false)
        {
            name = nam;
            enabled = false;
            isTogglable = istoggle;
        }

        public void ToggleSkill()
        {
            if (isTogglable) enabled = !enabled;
        }
    }
    #endregion

    #region skill de personajito
    
    [Serializable]
    public class PersonajitoSkill : BaseSkill
    {
        public override void ApplySkill()
        {
            enabled = true;
            //la pala o lo que toque...
        }
    }
    #endregion

    #region skill consumible

    public class ConsumibleSkill : BaseSkill
    {
        [SerializeField] public bool isOnlyOnce;
    }
    #endregion
    


}
