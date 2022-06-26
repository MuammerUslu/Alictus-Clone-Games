using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RobMaster3D
{
    public class ThiefController : MonoBehaviour
    {
        ThiefMover _thiefMover;
        ThiefAnim _thiefAnim;

        [SerializeField] float forwardSpeed;

        public static Action OnJump, OnLanding;
        [SerializeField] List<Rig> rigs;

        public enum State { Idle, Walk, Jump }
        public State currentState;

        public Transform character;

        private void Awake()
        {
            character = transform.GetChild(0);
            _thiefMover = new ThiefMover(this);
        }

        void Update()
        {
            if (!GameManager.Instance.AreWeInGamePlay())
                return;

            if (currentState == State.Jump)
                _thiefMover.Move(forwardSpeed / 5f);
            else
                _thiefMover.Move(forwardSpeed);

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "JumpCollider")
            {
                //Debug.Log("Jump");
                OnJump?.Invoke();
                other.enabled = false;
                currentState = State.Jump;
                StartCoroutine(Jump());
            }

            if (other.tag == "LandingCollider")
            {
                //Debug.Log("Land");
                OnLanding?.Invoke();
                other.enabled = false;
                currentState = State.Walk;
                StartCoroutine(Land());
            }

            if (other.tag == "FinishCollider")
            {
                GameManager.OnLevelCompleted?.Invoke();
            }
        }

        IEnumerator Land()
        {
            while (character.position.y >= 0)
            {
                character.position -= Vector3.up * Time.deltaTime * 2f;
                yield return null;
            }
            character.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        IEnumerator Jump()
        {
            while (character.position.y <= 1)
            {
                character.position += Vector3.up * Time.deltaTime * 2f;
                yield return null;
            }
            character.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }
}
