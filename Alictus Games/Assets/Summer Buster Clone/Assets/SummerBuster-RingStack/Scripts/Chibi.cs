using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SummerBuster
{
    public class Chibi : MonoBehaviour
    {
        public List<Ring> currentRings;
        public List<Transform> ringPositions;
        public Transform chibiTopPoint;

        private void Start()
        {
            for (int i = 0; i < currentRings.Count; i++)
            {
                currentRings[i].currentChibi = this;
                currentRings[i].transform.position = ringPositions[i].position;
            }
        }

        public void Dance()
        {
            StartCoroutine(WaitDance());
        }

        IEnumerator WaitDance()
        {
            yield return new WaitForSeconds(1f);
            GetComponent<Animator>().SetTrigger("Win");
        }
        public void Shake()
        {
            GetComponent<Animator>().SetTrigger("Shake");
        }
    }
}
