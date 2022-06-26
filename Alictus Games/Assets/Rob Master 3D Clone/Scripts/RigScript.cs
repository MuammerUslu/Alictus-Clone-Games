using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RobMaster3D
{
    public class RigScript : MonoBehaviour
    {

        Rig rig;
        [SerializeField] private TargetScript target;

        public static Action OnPlayModeEnable, OnPlayModeDisabled;

        private void Awake()
        {
            rig = GetComponent<Rig>();
            rig.weight = 0;
        }


        public void CancelRig()
        {
            StartCoroutine(WaitCancelRig());
        }

        public void ActivateRig()
        {
            StartCoroutine(WaitActivateRig());
        }

        IEnumerator WaitActivateRig()
        {
            float time = Time.time;
            while (rig.weight < 1)
            {
                if (Time.time > time + 0.5f)
                    rig.weight += Time.deltaTime * 4f;

                yield return null;
            }
            rig.weight = 1;
            OnPlayModeEnable?.Invoke();
        }

        IEnumerator WaitCancelRig()
        {
            while (rig.weight > 0.05f)
            {
                rig.weight -= Time.deltaTime * 2f;
                yield return null;
            }
            rig.weight = 0;
            target.ReturnInitialPos();
            OnPlayModeDisabled?.Invoke();
        }

        private void OnEnable()
        {
            ThiefController.OnJump += ActivateRig;
            ThiefController.OnLanding += CancelRig;
        }

        private void OnDisable()
        {
            ThiefController.OnJump -= ActivateRig;
            ThiefController.OnLanding -= CancelRig;

        }
    }
}
