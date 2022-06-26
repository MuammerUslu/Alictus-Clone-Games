using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyBuster
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager instance;
        public static ScoreManager Instance  // Not Persistent Instance.
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ScoreManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(ScoreManager).Name;
                        instance = obj.AddComponent<ScoreManager>();
                    }
                }
                return instance;
            }
        }

        public static int scoreMoney;

        public static Action OnMoneyChanged;
        private void Start()
        {
            scoreMoney = 0;
        }

        public void IncreaseMoney()
        {
            scoreMoney += 10;
            OnMoneyChanged?.Invoke();
        }

        public void DecraseMoney()
        {
            scoreMoney -= 10;
            OnMoneyChanged?.Invoke();
        }

        private void OnEnable()
        {
            Money.OnFailure += DecraseMoney;
            Money.OnSucces += IncreaseMoney;
        }

        private void OnDisable()
        {
            Money.OnFailure -= DecraseMoney;
            Money.OnSucces -= IncreaseMoney;
        }
    }
}
