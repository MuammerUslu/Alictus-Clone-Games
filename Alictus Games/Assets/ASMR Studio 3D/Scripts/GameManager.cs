using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ASMRStudio
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

        public static int currentLayerIndex;
        public static Action OnLayerChanged;
        public static Action OnLevelCompleted;
        public static bool levelCompleted;

        private void Awake()
        {
            levelCompleted = false;
            currentLayerIndex = 0;
        }
        public void CheckIfLevelCompleted()
        {
            if (currentLayerIndex >= 3)
            {
                OnLevelCompleted?.Invoke();
            }
        }


        private void LevelComplete()
        {
            levelCompleted = true;
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnEnable()
        {
            OnLevelCompleted += LevelComplete;
            OnLayerChanged += CheckIfLevelCompleted;
        }

        private void OnDisable()
        {
            OnLevelCompleted -= LevelComplete;
            OnLayerChanged -= CheckIfLevelCompleted;
        }
    }
}
