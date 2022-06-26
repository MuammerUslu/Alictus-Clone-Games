using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DeepCleanInc
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] GameObject levelCompletedPanel;
        public void OnClick_PlayAgainButton()
        {
            GameManager.Instance.RestartLevel();
        }

        private void OpenLevelCompletedPanel()
        {
            StartCoroutine(WaitToOpenLevelCompletedPanel());
        }

        IEnumerator WaitToOpenLevelCompletedPanel()
        {
            yield return new WaitForSeconds(0.4f);
            levelCompletedPanel.SetActive(true);
        }

        private void OnEnable()
        {
            GameManager.OnLevelCompleted += OpenLevelCompletedPanel;
        }
        private void OnDisable()
        {
            GameManager.OnLevelCompleted -= OpenLevelCompletedPanel;

        }
    }

}