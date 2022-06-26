using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobMaster3D
{
    public class SphereEffector : MonoBehaviour
    {
        [SerializeField] private Material redMaterial, greenMaterial;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "SafeZoneCollider")
            {
                ToGreen();
            }
        }

        void ToRed()
        {
            GetComponent<MeshRenderer>().material = redMaterial;
        }

        void ToGreen()
        {
            GetComponent<MeshRenderer>().material = greenMaterial;
        }
        private void OnEnable()
        {
            ToRed();
            RigScript.OnPlayModeDisabled += ToRed;

        }
        private void OnDisable()
        {
            ToRed();
            RigScript.OnPlayModeDisabled -= ToRed;

        }
    }
}
