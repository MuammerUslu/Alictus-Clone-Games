using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCleanInc
{
    public abstract class DragableObject : MonoBehaviour
    {
        [SerializeField] private Transform firstTargetPosHolder;
        private Vector3 firstTargetPos;
        protected Vector3 startPos;

        private Quaternion firstTargetRot;
        protected Quaternion startRot;

        Sequence seq;
        Vector3 offset;

        bool dragable;

        private float dragSensitivity = 4f;

        public static DragableObject selectedObject;

        bool firstDragClick;
        private void Awake()
        {
            DOTween.Init();

            startPos = transform.position;
            startRot = transform.rotation;

            firstTargetPos = firstTargetPosHolder.position;
            firstTargetRot = firstTargetPosHolder.rotation;
        }
        private void Update()
        {
            if (selectedObject == this)
            {
                if (!dragable)
                    return;

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePosition_ = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * dragSensitivity, Input.mousePosition.y * dragSensitivity, 1.0f));
                    if (firstDragClick == true)
                        offset = transform.position - mousePosition_;
                    else
                        offset = firstTargetPos - mousePosition_;

                    firstDragClick = true;
                }

                if (Input.GetMouseButton(0))
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * dragSensitivity, Input.mousePosition.y * dragSensitivity, 1.0f));
                    transform.position = Vector3.Lerp(transform.position, new Vector3((mousePosition + offset).x, transform.position.y, (mousePosition + offset).z), Time.deltaTime * 10f);
                }
            }
        }

        public virtual void Select()
        {
            seq = DOTween.Sequence();

            seq.Append(transform.DOMove(firstTargetPos, 1f)).SetEase(Ease.InBounce).Join(transform.DORotateQuaternion(firstTargetRot, 0.5f)).OnComplete(() => dragable = true);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * dragSensitivity, Input.mousePosition.y * dragSensitivity, 1.0f));
            offset = firstTargetPos - mousePosition;
        }

        public virtual void Deselect()
        {
            (transform.DOMove(startPos, 0.5f)).SetEase(Ease.Linear);
            dragable = false;
        }
    }
}
