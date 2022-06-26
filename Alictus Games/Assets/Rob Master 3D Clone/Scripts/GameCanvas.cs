using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobMaster3D
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject levelCompletedPanel, levelFailedPanel;
        public void StartLevel()
        {
            GameManager.Instance.StartLevel();
        }

        public void OnClick_RestartButton()
        {
            GameManager.Instance.RestartLevel();
        }

        public void OnClick_NextLevel()
        {
            GameManager.Instance.NextLevel();
        }

        void OpenFailedPanel()
        {
            levelFailedPanel.SetActive(true);
        }

        void OpenLevelCompletedPanel()
        {
            levelCompletedPanel.SetActive(true);
        }
        private void OnEnable()
        {
            GameManager.OnLevelCompleted += OpenLevelCompletedPanel;
            GameManager.OnLevelFailed += OpenFailedPanel;
        }

        private void OnDisable()
        {
            GameManager.OnLevelCompleted -= OpenLevelCompletedPanel;
            GameManager.OnLevelFailed -= OpenFailedPanel;
        }
    }
}
