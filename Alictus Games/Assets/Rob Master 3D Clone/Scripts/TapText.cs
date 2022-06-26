using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobMaster3D
{
    public class TapText : MonoBehaviour
    {
        void Close()
        {
            GetComponent<Animator>().SetBool("Close", true);
        }

        private void OnEnable()
        {
            GameManager.OnLevelStarted += Close;
        }

        private void OnDisable()
        {
            GameManager.OnLevelStarted -= Close;
        }
    }
}
