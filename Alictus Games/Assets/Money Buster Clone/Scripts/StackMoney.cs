using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyBuster
{
    public class StackMoney : MonoBehaviour
    {
        [SerializeField] private GameObject realMoney, realUVSprite;
        public float startPosY;

        private void Start()
        {
            startPosY = transform.position.y;
        }
        private void DisableUnnecessarySprites()
        {
            if (SelectObjects.selectedObject.tag == "UVLight")
            {
                realUVSprite.SetActive(true);
                realMoney.SetActive(false);

            }
            else if (SelectObjects.selectedObject.tag == "MagnifyingGlass")
            {
                realUVSprite.SetActive(false);
                realMoney.SetActive(true);
            }
        }

        private void OnEnable()
        {
            SelectObjects.OnSelectObject += DisableUnnecessarySprites;
        }

        private void OnDisable()
        {
            SelectObjects.OnSelectObject -= DisableUnnecessarySprites;
        }
    }
}
