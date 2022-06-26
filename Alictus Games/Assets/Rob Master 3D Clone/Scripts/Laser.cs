using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobMaster3D
{
    public class Laser : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "SphereEffector")
            {
                if (GameManager.levelFailed)
                    return;

                GameManager.OnLevelFailed?.Invoke();
                Debug.Log("LevelFailed");
            }
        }
    }
}
