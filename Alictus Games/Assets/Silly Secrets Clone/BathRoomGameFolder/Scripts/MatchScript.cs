using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SillySecrets
{
    public class MatchScript : MonoBehaviour
    {
        private static MatchScript instance;
        public static MatchScript Instance  // Not Persistent Instance.
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MatchScript>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(MatchScript).Name;
                        instance = obj.AddComponent<MatchScript>();
                    }
                }
                return instance;
            }
        }

        [SerializeField] Animator womanAnimator, boxAnimator;
        [SerializeField] int collideArrow = 0;

        public static Action OnTakePhoto;
        public int poseIndex;

        private void Start()
        {
            womanAnimator = GameObject.FindGameObjectWithTag("Woman").GetComponent<Animator>();
            boxAnimator = GetComponent<Animator>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            collideArrow++;
            if (collideArrow >= GameManager.Instance.TotalArrowNumber)
            {
                GameManager.OnLevelCompleted?.Invoke();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<ArrowSprite>() != null)
            {
                SwipeType swipeType = collision.GetComponent<ArrowSprite>().swipeType;

                if (TouchControl.swipeType == swipeType)
                {
                    Debug.Log("Arrow's type" + swipeType + "\n You Made:    " + TouchControl.swipeType);
                    collision.GetComponent<ArrowSprite>().CheckMatch();
                    poseIndex = collision.GetComponent<ArrowSprite>().poseIndex;
                    womanAnimator.SetTrigger("Pose_" + poseIndex);
                    OnTakePhoto?.Invoke();
                    collision.enabled = false;

                }
                else
                {
                    if (TouchControl.swipeType == SwipeType.None)
                        return;

                    Debug.Log("Arrow's type" + swipeType + "\n You Made:    " + TouchControl.swipeType);
                    collision.enabled = false;
                    boxAnimator.SetTrigger("Red");
                }
            }
        }

        public void PlayGreenAnim()
        {
            boxAnimator.SetTrigger("Green");
        }

        private void OnEnable()
        {
            OnTakePhoto += PlayGreenAnim;
        }
        private void OnDisable()
        {
            OnTakePhoto -= PlayGreenAnim;
        }


    }
}
