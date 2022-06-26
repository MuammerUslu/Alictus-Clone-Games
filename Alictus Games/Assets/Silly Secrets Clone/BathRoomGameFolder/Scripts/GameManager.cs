using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SillySecrets
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

        [SerializeField] GameObject[] Arrows = new GameObject[4];
        [SerializeField] GameObject spawnPoint;

        private float delay = 2;
        private int arrowCount = 0;
        public int TotalArrowNumber { get => 15; }

        public static int createdArrow = 0;
        public static Action OnLevelCompleted;

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void OnEnable()
        {
            GameCanvasScript.OnOpenArrowPanel += CreateArrows;
        }
        private void OnDisable()
        {
            GameCanvasScript.OnOpenArrowPanel -= CreateArrows;
        }
        public void CreateArrows()
        {
            StartCoroutine(SpawnArrows());
        }

        public IEnumerator SpawnArrows()
        {
            while (createdArrow < TotalArrowNumber)
            {
                arrowCount = UnityEngine.Random.Range(0, Arrows.Length);
                GameObject RandomArrow = Instantiate(Arrows[arrowCount], spawnPoint.transform.position, Arrows[arrowCount].transform.rotation);
                RandomArrow.transform.SetParent(spawnPoint.transform);
                createdArrow++;

                yield return new WaitForSeconds(delay);
            }
        }
    }
}
