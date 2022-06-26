using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace MoneyBuster
{
    public class Money : DragableObject
    {
        public bool isReal;

        MoneyStack_Holder moneyStack_Holder;
        MoneyShredder moneyShredder;

        public static Action OnDeclineMoney;
        public static Action OnAcceptMoney;
        public static Action OnSucces, OnFailure, OnDestroyMoney;
        Sequence sequence;

        public enum Aprroved { yes, no }
        public Aprroved approve;

        private void Start()
        {
            moneyShredder = FindObjectOfType<MoneyShredder>();
            moneyStack_Holder = FindObjectOfType<MoneyStack_Holder>();
        }

        public override void Deselect()
        {

            if (transform.position.z <= 1.5f)
            {
                base.Deselect();
            }
            else
            {
                if (transform.position.x <= 0)
                {
                    GoForShred();
                    OnDeclineMoney?.Invoke();
                }
                else
                {
                    GoForCollect();
                    OnAcceptMoney?.Invoke();
                }
            }
        }

        private void GoForShred()
        {
            sequence = DOTween.Sequence();

            approve = Aprroved.no;

            sequence.Append(transform.DOMove(moneyShredder.transform.position - Vector3.forward * 2f + Vector3.up * 0.56f, 0.5f))
                .Join(transform.DORotate(new Vector3(90, 0, 0), 0.5f))
                .Append(transform.DOMove(moneyShredder.transform.position + Vector3.up * 0.56f, 0.5f));
            sequence.OnComplete(() => SetScore());
        }

        private void GoForCollect()
        {
            sequence = DOTween.Sequence();

            approve = Aprroved.yes;
            sequence.Append(transform.DOMove(moneyStack_Holder.transform.position - Vector3.forward * 2f, 0.5f))
                .Join(transform.DORotate(new Vector3(90, 0, 0), 0.5f))
                .Append(transform.DOMove(moneyStack_Holder.transform.position, 0.5f));
            sequence.OnComplete(() => SetScore());
        }

        private void SetScore()
        {
            if (isReal && approve == Aprroved.yes)
            {
                OnSucces?.Invoke();
                return;
            }
            else if (!isReal && approve == Aprroved.no)
            {
                OnSucces?.Invoke();

            }
            else
            {
                OnFailure?.Invoke();
            }
            OnDestroyMoney?.Invoke();
            Destroy(gameObject);
        }
    }
}
