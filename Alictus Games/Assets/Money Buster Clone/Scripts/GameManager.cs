using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoneyBuster
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance  // Not Persistent Instance.
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(GameManager).Name;
                        instance = obj.AddComponent<GameManager>();
                    }
                }
                return instance;
            }
        }

        public static bool levelCompleted;
        public static Action OnLevelCompleted;

        [SerializeField] private GameObject moneyPrefab;

        private void Start()
        {
            levelCompleted = false;
        }

        public void NextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        public bool AreWeInGamePlay()
        {
            if (!levelCompleted)
            {
                return true;
            }
            return false;
        }

        private void InstantiateMoney()
        {
            Instantiate(moneyPrefab);
        }

        private void OnEnable()
        {
            Money.OnDestroyMoney += InstantiateMoney;
        }

        private void OnDisable()
        {
            Money.OnDestroyMoney -= InstantiateMoney;
        }
    }
}