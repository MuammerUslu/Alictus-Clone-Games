using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SummerBuster
{
    public class CanvasScript : MonoBehaviour
    {
        [SerializeField] private GameObject levelCompletedPanel;

        void OpenLevelCompletedPanel()
        {
            StartCoroutine(WaitOpenlevelCompletedPanel());
        }

        IEnumerator WaitOpenlevelCompletedPanel()
        {
            yield return new WaitForSeconds(2f);
            levelCompletedPanel.SetActive(true);
        }

        public void OnClickPlayAgainButton()
        {
            GameManager.Instance.NextLevel();
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
