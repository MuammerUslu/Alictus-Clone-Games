using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ASMRStudio
{
    public class KnifeController : MonoBehaviour
    {
        // There was no time to clean this code. (Whole project)

        [SerializeField] Transform boxCutter;
        [SerializeField] GameObject maskObj;

        List<Transform> knifePositions = new List<Transform>();
        public int currentSoapLayer;
        private Vector3 startLocPosMaskObj;
        public float maxReachedZPos;
        private bool layerChanging;
        SoapController soapCont;
        Vector3 offset;
        [SerializeField] float dragSensitivity = 5;
        [SerializeField] ParticleSystem particle;
        private float time;

        private void Start()
        {
            DOTween.Init();
            startLocPosMaskObj = maskObj.transform.localPosition;
            soapCont = FindObjectOfType<SoapController>();
            foreach (Transform t in soapCont.knifePosition)
            {
                knifePositions.Add(t);
            }
            maxReachedZPos = boxCutter.transform.position.z;
        }
        private void Update()
        {
            if (layerChanging)
            {
                maskObj.transform.parent = null;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition_ = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * dragSensitivity, Input.mousePosition.y * dragSensitivity, 1.0f));

                offset = boxCutter.transform.position - mousePosition_;
            }

            if (Input.GetMouseButton(0))
            {
                if (boxCutter.transform.position.z <= maxReachedZPos)
                {
                    maskObj.transform.parent = boxCutter;
                    if (Time.time >= time + 0.5f)
                    {
                        time = Time.time;
                        particle.Play();
                    }
                }
                else
                {
                    maskObj.transform.parent = null;
                }

                Move();
            }

            if (boxCutter.transform.position.z <= maxReachedZPos)
                maxReachedZPos = boxCutter.transform.position.z;
        }

        private void Move()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * dragSensitivity, Input.mousePosition.y * dragSensitivity, 1.0f));

            if (boxCutter.transform.position.z > knifePositions[0].position.z + 0.1f)
            {
                boxCutter.transform.position = Vector3.Lerp(boxCutter.transform.position, new Vector3(boxCutter.transform.position.x, knifePositions[0].position.y, knifePositions[0].position.z), Time.deltaTime * 10f);
                return;
            }
            else if (boxCutter.transform.position.z < knifePositions[1].position.z)
            {
                if (Vector3.Distance(boxCutter.transform.position, knifePositions[1].position) <= 0.04f)
                {
                    layerChanging = true;
                    TurnInitialPos();
                }
            }
            else
            {
                boxCutter.transform.position = Vector3.Lerp(boxCutter.transform.position, new Vector3(boxCutter.transform.position.x, knifePositions[0].position.y, (mousePosition + offset).z), Time.deltaTime * 10f);
            }
        }

        private void TurnInitialPos()
        {
            soapCont.DecreasPointPositions();
            GameManager.currentLayerIndex++;
            boxCutter.transform.DOMove(knifePositions[0].position, 1f).OnComplete(() => IncreaseLayerIndex());
        }

        private void IncreaseLayerIndex()
        {
            maskObj.transform.parent = boxCutter;
            layerChanging = false;
            GameManager.OnLayerChanged?.Invoke();
            maxReachedZPos = knifePositions[0].position.z;
            maskObj.transform.localPosition = startLocPosMaskObj;

        }
    }
}
