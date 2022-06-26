using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobMaster3D
{
    public class LimbSelector : MonoBehaviour
    {
        Transform limbTarget;

        private Vector3 screenPoint;
        private Vector3 offset;

        ThiefController thief;

        private void Awake()
        {
            thief = FindObjectOfType<ThiefController>();
        }
        private void Update()
        {
            if (!GameManager.Instance.AreWeInGamePlay())
                return;
            if (thief.currentState != ThiefController.State.Jump)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
                {
                    limbTarget = hit.collider.transform;
                    screenPoint = Camera.main.WorldToScreenPoint(limbTarget.transform.position);
                    offset = limbTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (limbTarget != null)
                    limbTarget.transform.GetComponent<TargetScript>().ReturnToLimb();

                limbTarget = null;
            }

            if (limbTarget == null)
                return;

            //Debug.Log(limbTarget);

            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            limbTarget.transform.position = curPosition;
        }
    }
}