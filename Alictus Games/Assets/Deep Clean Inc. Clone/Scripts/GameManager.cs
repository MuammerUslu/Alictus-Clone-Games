using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeepCleanInc
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

        HandVacuum handVacuum;
        WaterVacuum waterVacuum;

        GameObject[] cheeseDirts;
        public int cheeseCount;

        public static Action OnACheesePieceCleaned;
        public static Action OnLevelCompleted;

        public static bool levelCompleted;

        int scoreCounterCubeCount = 0;
        int totalScoreCounterCubes;

        private void Start()
        {
            scoreCounterCubeCount = 0;
            handVacuum = FindObjectOfType<HandVacuum>();
            waterVacuum = FindObjectOfType<WaterVacuum>();
            cheeseDirts = GameObject.FindGameObjectsWithTag("CheeseDirt");
            cheeseCount = cheeseDirts.Length;

            totalScoreCounterCubes = GameObject.FindGameObjectsWithTag("DirtScoreCollider").Length;

            SetTurn(handVacuum);
        }

        public void CheckLevelCompleted()
        {
            if (levelCompleted)
                return;

            scoreCounterCubeCount++;

            if (totalScoreCounterCubes == scoreCounterCubeCount)
                OnLevelCompleted?.Invoke();
        }

        private void LevelComplete()
        {
            levelCompleted = true;
        }
        private void SetTurn(DragableObject dragableObject)
        {
            StartCoroutine(WaitForSettingTurn(dragableObject));
        }
        IEnumerator WaitForSettingTurn(DragableObject dragableObject)
        {
            yield return new WaitForSeconds(0.5f);
            DragableObject.selectedObject = dragableObject;
            dragableObject.Select();
        }

        private void CheckForNextStep()
        {
            cheeseCount--;
            if (cheeseCount == 0)
            {
                SetTurn(waterVacuum);
                handVacuum.Deselect();
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnEnable()
        {
            OnACheesePieceCleaned += CheckForNextStep;
            OnLevelCompleted += LevelComplete;
        }

        private void OnDisable()
        {
            OnACheesePieceCleaned -= CheckForNextStep;
            OnLevelCompleted -= LevelComplete;
        }
    }
}
