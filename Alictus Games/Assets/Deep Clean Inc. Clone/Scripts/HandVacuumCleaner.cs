using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DeepCleanInc
{
    public class HandVacuumCleaner : MonoBehaviour
    {
        [SerializeField] Transform dirtEnter, dirtDestroy;

        private Vector2 dirtEnterPos, dirtDestroyPos;

        Sequence seq;

        private void Start()
        {
            DOTween.Init();
            dirtEnterPos = dirtEnter.transform.localPosition;
            dirtDestroyPos = dirtDestroy.transform.localPosition;
        }

        private void MoveCheese(Transform cheese)
        {
            seq = DOTween.Sequence();

            seq.Append(cheese.DOMove(dirtEnterPos, 0.3f))
                .Append(cheese.DOMove(dirtDestroyPos, 0.3f));

            seq.OnComplete(() => Destroy(cheese.gameObject));

            GameManager.OnACheesePieceCleaned?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "CheeseDirt")
            {
                MoveCheese(other.transform);
                other.enabled = false;
            }
        }
    }
}
