using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SillySecrets
{
    public class GameCanvasScript : MonoBehaviour
    {
        [SerializeField] private GameObject[] conversation;
        [SerializeField] private GameObject arrowPanel, levelCompletedPanel;
        [SerializeField] private GameObject[] photoPrefab;
        [SerializeField] private Transform photoSpawnPoint;

        public static Action OnOpenArrowPanel;

        void Start()
        {
            StartCoroutine(StartConversation());
        }

        public IEnumerator StartConversation()
        {
            yield return new WaitForSeconds(2f);
            conversation[0].SetActive(true);
            yield return new WaitForSeconds(2f);
            conversation[1].SetActive(true);
            yield return new WaitForSeconds(2f);
            conversation[2].SetActive(true);
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < conversation.Length; i++)
            {
                conversation[i].SetActive(false);
            }
            arrowPanel.SetActive(true);
            OnOpenArrowPanel?.Invoke();
        }
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
            yield return new WaitForSeconds(4f);
            levelCompletedPanel.SetActive(true);
        }
        public void DisplayPhotos()
        {
            int randomPos_y = UnityEngine.Random.Range(0, 20);
            int randomRot_z = UnityEngine.Random.Range(-15, 25);

            GameObject photo = Instantiate(photoPrefab[MatchScript.Instance.poseIndex], photoSpawnPoint.position, photoPrefab[MatchScript.Instance.poseIndex].transform.rotation);

            SpreadPhotos.Instance.allPhotos.Add(photo);
            photo.transform.SetParent(photoSpawnPoint.transform);
            photo.transform.position += Vector3.up * randomPos_y;
            photo.transform.eulerAngles += Vector3.forward * randomRot_z;
        }

        private void OnEnable()
        {
            MatchScript.OnTakePhoto += DisplayPhotos;
            GameManager.OnLevelCompleted += OpenLevelCompletedPanel;
        }
        private void OnDisable()
        {
            MatchScript.OnTakePhoto -= DisplayPhotos;
            GameManager.OnLevelCompleted -= OpenLevelCompletedPanel;

        }
    }

}
