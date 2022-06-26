using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace RobMaster3D
{
    public class TargetScript : MonoBehaviour
    {
        private Vector3 startPos;
        private Quaternion startRot;

        private GameObject effector;

        [SerializeField] private Transform followerLimb;
        void Start()
        {
            DOTween.Init();
            effector = transform.GetChild(0).gameObject;
            startPos = transform.localPosition;
            startRot = transform.localRotation;
        }

        public void ReturnInitialPos()
        {
            transform.localPosition = startPos;
            transform.localRotation = startRot;
        }

        void OpenEffector()
        {
            effector.GetComponent<MeshRenderer>().enabled = true;
        }

        void CloseEffector()
        {
            effector.GetComponent<MeshRenderer>().enabled = false;
        }

        public void ReturnToLimb()
        {
            Vector3 targetPos = followerLimb.transform.position;
            transform.DOMove(targetPos, 0.5f);
        }

        private void OnEnable()
        {
            RigScript.OnPlayModeEnable += OpenEffector;
            RigScript.OnPlayModeDisabled += CloseEffector;
        }

        private void OnDisable()
        {
            RigScript.OnPlayModeEnable -= OpenEffector;
            RigScript.OnPlayModeDisabled -= CloseEffector;
        }
    }
}
