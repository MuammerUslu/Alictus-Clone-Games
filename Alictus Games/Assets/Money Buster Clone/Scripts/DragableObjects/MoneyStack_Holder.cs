using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoneyBuster
{
    public class MoneyStack_Holder : MonoBehaviour
    {

        List<StackMoney> allStackMoney = new List<StackMoney>();
        Transform moneyStackHolder;
        // Start is called before the first frame update
        void Start()
        {
            DOTween.Init();

            foreach (Transform child in transform)
            {
                if (child.GetComponent<StackMoney>() != null)
                {
                    allStackMoney.Add(child.GetComponent<StackMoney>());
                }
                else
                {
                    moneyStackHolder = child;
                }
            }
        }

        private void PlayCollectAnim()
        {
            moneyStackHolder.DOScaleY(4, 0.5f).OnComplete(() => moneyStackHolder.DOScaleY(1, 1f));
            foreach (StackMoney money in allStackMoney)
            {
                money.transform.DOMoveY(2, 0.5f).OnComplete(() => money.transform.DOMoveY(money.startPosY, money.startPosY));

            }
        }
        private void OnEnable()
        {
            Money.OnAcceptMoney += PlayCollectAnim;
        }

        private void OnDisable()
        {
            Money.OnAcceptMoney -= PlayCollectAnim;
        }
    }
}
