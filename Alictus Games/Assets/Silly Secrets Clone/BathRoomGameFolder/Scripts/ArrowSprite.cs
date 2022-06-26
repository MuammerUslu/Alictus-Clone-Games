using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SillySecrets
{
    public class ArrowSprite : MonoBehaviour
    {
        float movementSpeed = 250f;

        public bool matchChecked;
        public int poseIndex;
        public SwipeType swipeType;

        void Start()
        {
            StartCoroutine(DestroyArrow());
        }

        private void Update()
        {
            if (matchChecked)
                return;

            Move();
        }

        public void Move()
        {
            transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        }

        public void CheckMatch()
        {
            if (TouchControl.swipeType == swipeType)
            {
                matchChecked = true;
                GetComponent<Animator>().enabled = true;
            }
        }

        public IEnumerator DestroyArrow()
        {
            yield return new WaitForSeconds(8f);
            Destroy(this.gameObject);
        }
    }
}
