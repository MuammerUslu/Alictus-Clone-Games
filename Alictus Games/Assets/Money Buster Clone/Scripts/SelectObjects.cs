using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyBuster
{
    public class SelectObjects : MonoBehaviour
    {
        private static SelectObjects instance;
        public static SelectObjects Instance  // Not Persistent Instance.
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SelectObjects>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(SelectObjects).Name;
                        instance = obj.AddComponent<SelectObjects>();
                    }
                }
                return instance;
            }
        }

        private float lastSelectionTime;
        private float minSelectionFreq = 0.5f;

        public static DragableObject selectedObject;

        public static Action OnSelectObject;
        private void tart()
        {
            selectedObject = null;
        }
        private void Update()
        {
            if (!GameManager.Instance.AreWeInGamePlay())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (selectedObject != null)
                    return;

                if (Time.time - lastSelectionTime <= minSelectionFreq)
                    return;

                lastSelectionTime = Time.time;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.GetComponent<DragableObject>() == null)
                        return;

                    selectedObject = hit.transform.GetComponent<DragableObject>();
                    selectedObject.Select();
                    OnSelectObject?.Invoke();
                    Debug.Log(selectedObject + " select");
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (selectedObject == null)
                    return;
                selectedObject.Deselect();
                Debug.Log(selectedObject + " deselect");

                selectedObject = null;

            }
        }
    }
}
