using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RobMaster3D
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
        public static bool levelStarted;
        public static bool levelFailed;

        public static Action OnLevelStarted;
        public static Action OnLevelCompleted;
        public static Action OnLevelFailed;

        private void Start()
        {
            levelFailed = false;
            levelStarted = false;
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
            if (levelStarted && !levelCompleted && !levelFailed)
            {
                return true;
            }
            return false;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void StartLevel()
        {
            if (!levelStarted)
            {
                levelStarted = true;
                OnLevelStarted?.Invoke();
                Debug.Log("LevelStarted");
            }
        }

        private void LevelComplete()
        {
            levelCompleted = true;
        }
        private void LevelFailed()
        {
            levelFailed = true;
        }

        private void OnEnable()
        {
            OnLevelCompleted += LevelComplete;
            OnLevelFailed += LevelFailed;
        }

        private void OnDisable()
        {
            OnLevelCompleted -= LevelComplete;
            OnLevelFailed -= LevelFailed;

        }
    }
}


