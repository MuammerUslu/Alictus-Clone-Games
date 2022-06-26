using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SummerBuster
{
    public class RingSelector : MonoBehaviour
    {
        private static RingSelector instance;
        public static RingSelector Instance  // Not Persistent Instance.
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<RingSelector>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(RingSelector).Name;
                        instance = obj.AddComponent<RingSelector>();
                    }
                }
                return instance;
            }
        }

        public static Chibi selectedChibi;
        public static Ring selectedRing;

        private float lastSelectionTime;
        public float minSelectionFreq = 0.5f;

        private void Start()
        {
            lastSelectionTime = -10f;
            selectedChibi = null;
            selectedRing = null;
            DOTween.Init();
        }
        private void Update()
        {
            if (!GameManager.Instance.AreWeInGamePlay())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (selectedRing != null)
                    return;

                if (Time.time - lastSelectionTime <= minSelectionFreq)
                    return;

                lastSelectionTime = Time.time;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.GetComponent<Chibi>() == null)
                        return;

                    if (hit.transform.GetComponent<Chibi>().currentRings.Count == 0)
                        return;

                    selectedChibi = hit.transform.GetComponent<Chibi>();
                    selectedChibi.Shake();
                    selectedRing = selectedChibi.currentRings[selectedChibi.currentRings.Count - 1];
                    selectedRing.OnSelected?.Invoke();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (selectedRing == null)
                    return;
                selectedRing.OnDeselected?.Invoke();
                selectedRing = null;

            }
        }
    }
}
