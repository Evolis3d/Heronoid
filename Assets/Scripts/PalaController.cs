using System;
using UnityEngine;

namespace mierdergames
{
    [RequireComponent(typeof(Personajitos))]
    public class PalaController : MonoBehaviour
    {
        public Transform pala;
        public float palaspeed;
        private Vector3 palapos;
        private float Ypos;
        [SerializeField] public SO_Personajito[] currentCombo;
        private bool canMove;
            
        private Personajitos _persoComp;

        #if UNITY_ANDROID && !UNITY_EDITOR
            private Camera _cam;
        #endif
       
        
        void Awake()
        {
            _persoComp = GetComponent<Personajitos>();
            Ypos = pala.position.y;
            
            #if UNITY_ANDROID && !UNITY_EDITOR
                _cam = Camera.main;
            #endif
        }
        
        void Start()
        {
            _persoComp.PersonajitosCargados += SetComboPala;
        }
        
        void Update()
        {
            if (!canMove) return;
            
            #if UNITY_EDITOR  
                pala.transform.Translate(Input.GetAxis("Horizontal") * palaspeed * Time.deltaTime, 0, 0);
                if (Input.GetKeyDown(KeyCode.Escape)) UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
            #if UNITY_ANDROID && !UNITY_EDITOR
                if (Input.touchCount >= 1)
                {
                    var newpos = _cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                    //palapos = new Vector3(newpos.x, palapos.y,0);
                    palapos = new Vector3(newpos.x,Ypos,0);

                    pala.transform.position = palapos;
                }
            #endif
        }
        
        private void SetComboPala()
        {
            //de momento, sólo desbloqueo el soldado
            _persoComp.UnlockPersonajito(_persoComp.lista[0]);
        
            //vamos a forzarlo a los 3x soldado.
            currentCombo = new SO_Personajito[3];
            currentCombo[0] = _persoComp.desbloqueados[0];
            currentCombo[1] = _persoComp.desbloqueados[0];
            currentCombo[2] = _persoComp.desbloqueados[0];
        }

        public void EnableControl()
        {
            canMove = true;
        }

        public void DisableControl()
        {
            canMove = false;
        }

        public void InitPala()
        {
            palapos = pala.transform.position;  
        }

        void OnDestroy()
        {
            _persoComp.PersonajitosCargados -= SetComboPala;
        }

        
    }
}