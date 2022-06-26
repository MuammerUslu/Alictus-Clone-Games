using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyBuster
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

        private float dragSensitivity = 8f;
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
            if (SelectObjects.selectedObject == this)
            {
                if (!dragable)
                    return;

                //Debug.Log(transform.name);
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * dragSensitivity, Input.mousePosition.y * dragSensitivity, 1.0f));
                transform.position = Vector3.Lerp(transform.position, new Vector3((mousePosition + offset).x, transform.position.y, (mousePosition + offset).z), Time.deltaTime * 20f);
            }
        }

        public virtual void Select()
        {
            seq = DOTween.Sequence();

            seq.Append(transform.DOMove(firstTargetPos, 0.3f)).SetEase(Ease.Linear).Join(transform.DORotateQuaternion(firstTargetRot, 0.3f)).OnComplete(() => dragable = true);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * dragSensitivity, Input.mousePosition.y * dragSensitivity, 1.0f));
            offset = firstTargetPos - mousePosition;
        }
        public virtual void Deselect()
        {
            seq = DOTween.Sequence();

            seq.Append(transform.DOMove(startPos, 0.3f)).Join(transform.DORotateQuaternion(startRot, 0.3f));
            dragable = false;
        }
    }
}
