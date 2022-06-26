using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace SummerBuster
{
    public class Ring : MonoBehaviour
    {
        public Transform ghostRing;
        public Chibi currentChibi, targetChibi;
        public enum RingColor { Yellow, Blue, Pink, Green }
        public RingColor ringColor;

        [SerializeField] private float goUpTime = 0.2f;
        Vector3 startPos;
        public Action OnSelected, OnDeselected;
        Vector3 offset;
        Sequence seq;

        private void Start()
        {
            DOTween.Init();
        }

        private void Update()
        {
            if (RingSelector.selectedRing == this)
            {
                if (transform.position.y < currentChibi.chibiTopPoint.position.y)
                    return;

                GetComponent<SphereCollider>().enabled = true;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = Vector3.Lerp(transform.position, new Vector3((mousePosition + offset).x, transform.position.y, (mousePosition + offset).z), Time.deltaTime * 20f);
            }

        }
        void SettleInTargetPosition()
        {
            seq = DOTween.Sequence();

            if (targetChibi == null)
            {
                seq.Append(transform.DOMove(currentChibi.chibiTopPoint.position, 0.3f)).AppendInterval(0.3f)
                    .Append(transform.DOMove(startPos, 0.4f)).SetEase(Ease.OutBounce);

                GetComponent<SphereCollider>().enabled = false;

            }
            else
            {
                ghostRing.gameObject.SetActive(false);
                currentChibi.currentRings.Remove(this);
                currentChibi = targetChibi;

                seq.Append(transform.DOMove(currentChibi.chibiTopPoint.position, 0.3f)).AppendInterval(0.3f)
                    .Append(transform.DOMove(currentChibi.ringPositions[currentChibi.currentRings.Count].position, 0.4f)).SetEase(Ease.OutBounce).OnComplete(() => GameManager.Instance.CheckIfLevelCompleted());

                targetChibi.currentRings.Add(this);
                GetComponent<SphereCollider>().enabled = false;
            }
        }

        void GoUp()
        {
            transform.DOMove(currentChibi.chibiTopPoint.position, goUpTime);
        }

        void SetInitialValues()
        {
            startPos = transform.position;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - mousePosition;
        }
        void Bounce()
        {
            float startY = transform.position.y;
            transform.DOMoveY(transform.position.y + 2, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => transform.DOMoveY(startY, 0.5f));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Chibi")
            {
                if (other.GetComponent<Chibi>() == currentChibi)
                    return;
                if (other.GetComponent<Chibi>() == RingSelector.selectedChibi)
                    return;
                if (ghostRing.gameObject.activeSelf)
                    return;

                if (other.GetComponent<Chibi>().currentRings.Count == 0 || other.GetComponent<Chibi>().currentRings[other.GetComponent<Chibi>().currentRings.Count - 1].ringColor == ringColor)
                {
                    targetChibi = other.GetComponent<Chibi>();
                    ghostRing.transform.position = targetChibi.ringPositions[targetChibi.currentRings.Count].position;
                    ghostRing.parent = null;
                    ghostRing.gameObject.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Chibi")
            {
                ghostRing.gameObject.SetActive(false);
                targetChibi = null;
            }
        }
        private void OnEnable()
        {
            OnSelected += GoUp;
            OnSelected += SetInitialValues;
            OnDeselected += SettleInTargetPosition;
            GameManager.OnLevelCompleted += Bounce;
        }

        private void OnDisable()
        {
            OnSelected -= GoUp;
            OnSelected -= SetInitialValues;
            OnDeselected -= SettleInTargetPosition;
            GameManager.OnLevelCompleted -= Bounce;

        }
    }
}
