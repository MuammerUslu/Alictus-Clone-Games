using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyBuster
{
    public class MoneySpriteController : MonoBehaviour
    {
        [SerializeField] private GameObject fakeUVSprite, realUVSprite;
        [SerializeField] private GameObject fakeMoney, realMoney;

        Money money;
        private GameObject selectedUVSprite, selectedMoneySprite;
        private void Start()
        {
            money = GetComponent<Money>();
            RandomlySetSprites();
        }

        private void RandomlySetSprites()
        {
            int rand1 = Random.Range(0, 2);
            int rand2 = Random.Range(0, 2);

            if (rand1 == 0 || rand2 == 0)
                money.isReal = false;
            else
                money.isReal = true;

            if (rand1 == 0)
            {
                fakeUVSprite.SetActive(true);
                realUVSprite.SetActive(false);
                selectedUVSprite = fakeUVSprite;
            }
            else
            {
                realUVSprite.SetActive(true);
                fakeUVSprite.SetActive(false);
                selectedUVSprite = realUVSprite;
            }
            if (rand2 == 0)
            {
                fakeMoney.SetActive(true);
                realMoney.SetActive(false);
                selectedMoneySprite = fakeMoney;
            }
            else
            {
                realMoney.SetActive(true);
                fakeMoney.SetActive(false);
                selectedMoneySprite = realMoney;
            }
        }

        private void DisableUnnecessarySprites()
        {
            if (SelectObjects.selectedObject.tag == "UVLight")
            {
                selectedUVSprite.SetActive(true);
                selectedMoneySprite.SetActive(false);
            }
            else if (SelectObjects.selectedObject.tag == "MagnifyingGlass")
            {
                selectedUVSprite.SetActive(false);
                selectedMoneySprite.SetActive(true);
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
