using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mierdergames.libs
{
    public class Shake : MonoBehaviour
    {
        private Transform target;
        
        /// <summary>
        /// CoRoutine that does shakes on the specified transform
        /// </summary>
        /// <param name="transf"></param>
        /// <param name="duration"></param>
        /// <param name="magnitude"></param>
        /// <returns></returns>
        public IEnumerator Shakeit(Transform transf, float duration, float magnitude)
        {
            target = transf;
            Vector3 orignalPosition = target.transform.position;
            float elapsed = 0f;
        
            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                target.transform.position = new Vector3(x, y, -10f);
                elapsed += Time.deltaTime;
                yield return 0;
            }
            target.transform.position = orignalPosition;
        }
        
        public IEnumerator Shakeit(Transform transf, Vector2 shakePreset)
        {
            target = transf;
            Vector3 orignalPosition = target.transform.position;
            float elapsed = 0f;
        
            while (elapsed < shakePreset.x)
            {
                float x = Random.Range(-1f, 1f) * shakePreset.y;
                float y = Random.Range(-1f, 1f) * shakePreset.y;

                target.transform.position = new Vector3(x, y, -10f);
                elapsed += Time.deltaTime;
                yield return 0;
            }
            target.transform.position = orignalPosition;
        }
    }
}
