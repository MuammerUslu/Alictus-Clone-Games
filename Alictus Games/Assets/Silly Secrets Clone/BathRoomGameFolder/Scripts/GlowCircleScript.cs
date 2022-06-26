using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SillySecrets
{
    public class GlowCircleScript : MonoBehaviour
    {
        void Flash()
        {
            StartCoroutine(BlastFlash());
        }

        IEnumerator BlastFlash()
        {
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = false;
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
