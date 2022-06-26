using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobMaster3D
{
    public class ThiefAnim : MonoBehaviour
    {
        Animator animator;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }
        void Walk()
        {
            animator.SetTrigger("Walk");
        }

        void Jump()
        {
            animator.SetTrigger("Jump");
        }

        void Dance()
        {
            animator.SetTrigger("Dance");
        }

        private void OnEnable()
        {
            GameManager.OnLevelStarted += Walk;
            ThiefController.OnJump += Jump;
            ThiefController.OnLanding += Walk;
            GameManager.OnLevelCompleted += Dance;
        }

        private void OnDisable()
        {
            GameManager.OnLevelStarted -= Walk;
            ThiefController.OnJump -= Jump;
            ThiefController.OnLanding -= Walk;
            GameManager.OnLevelCompleted -= Dance;
        }
    }
}
