using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SummerBuster
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

        public Chibi[] allChibis;

        public static bool levelCompleted;

        public static Action OnLevelCompleted;


        private void Start()
        {
            levelCompleted = false;
            allChibis = FindObjectsOfType<Chibi>();
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


        public void CheckIfLevelCompleted()
        {
            Chibi freeChibi = null;
            foreach (Chibi chibi in allChibis)
            {
                if (chibi.currentRings.Count == 0)
                    freeChibi = chibi;
                for (int i = 0; i < chibi.currentRings.Count; i++)
                {
                    if (i + 1 > chibi.currentRings.Count - 1)
                        continue;

                    if (chibi.currentRings[i].ringColor != chibi.currentRings[i + 1].ringColor)
                        return;
                }
            }

            if (freeChibi != null)
            {
                freeChibi.Dance();
                levelCompleted = true;
                OnLevelCompleted?.Invoke();
            }
        }
    }
}
