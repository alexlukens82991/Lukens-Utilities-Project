using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukensUtils
{
    public class LukensUtilsRuntime : MonoBehaviour
    {
        public void DelayedFire(Action action, float time)
        {
            StartCoroutine(DelayedFireRoutine(action, time));
        }

        private IEnumerator DelayedFireRoutine(Action action, float time)
        {
            yield return new WaitForSeconds(time);

            action.Invoke();
        }
    }
}