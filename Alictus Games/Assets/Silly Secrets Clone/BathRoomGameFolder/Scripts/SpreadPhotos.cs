using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SillySecrets
{
    public class SpreadPhotos : MonoBehaviour
    {
        private static SpreadPhotos instance;
        public static SpreadPhotos Instance  // Not Persistent Instance.
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SpreadPhotos>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(SpreadPhotos).Name;
                        instance = obj.AddComponent<SpreadPhotos>();
                    }
                }
                return instance;
            }
        }

        public List<GameObject> allPhotos = new List<GameObject>();
        GameObject[] photoDisplayPoints;

        Sequence seq;
        private void Start()
        {
            DOTween.Init();
            photoDisplayPoints = GameObject.FindGameObjectsWithTag("ImageDisplayPoint");
        }

        private void FindAndSpreadPhotos()
        {
            seq = DOTween.Sequence();

            for (int i = 0; i < allPhotos.Count; i++)
            {
                seq.Append(allPhotos[i].transform.DOMove(photoDisplayPoints[i].transform.position, 0.2f));
            }
        }

        private void OnEnable()
        {
            GameManager.OnLevelCompleted += FindAndSpreadPhotos;
        }

        private void OnDisable()
        {
            GameManager.OnLevelCompleted -= FindAndSpreadPhotos;
        }
    }
}
