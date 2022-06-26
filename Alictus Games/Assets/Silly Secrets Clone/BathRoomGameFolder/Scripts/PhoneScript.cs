using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SillySecrets
{
    public class PhoneScript : MonoBehaviour
    {
        [SerializeField] private GameObject glowStar;

        void Flash()
        {
            StartCoroutine(BlastFlash());
        }

        IEnumerator BlastFlash()
        {
            glowStar.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            glowStar.SetActive(false);
        }

        private void OnEnable()
        {
            MatchScript.OnTakePhoto += Flash;
        }

        private void OnDisable()
        {
            MatchScript.OnTakePhoto -= Flash;
        }
    }
}
