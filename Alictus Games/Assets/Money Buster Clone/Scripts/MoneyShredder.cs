using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyBuster
{
    public class MoneyShredder : MonoBehaviour
    {
        private void Shred()
        {
            GetComponent<Animator>().SetTrigger("MoneyShredStart");
        }

        private void OnEnable()
        {
            Money.OnDeclineMoney += Shred;
        }

        private void OnDisable()
        {
            Money.OnDeclineMoney -= Shred;
        }
    }
}
