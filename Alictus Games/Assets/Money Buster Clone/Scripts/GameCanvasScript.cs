using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MoneyBuster
{
    public class GameCanvasScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] GameObject minusTextPrefab, plusScoreTextPrefab;

        private void Start()
        {
            UpdateScoreText();
        }
        private void UpdateScoreText()
        {
            scoreText.text = ScoreManager.scoreMoney.ToString();
        }

        private void InstantiateMinusTextPrefab()
        {
            GameObject scoreTextPref = Instantiate(minusTextPrefab.gameObject, transform);
            DestroyPrefab(scoreTextPref);
        }

        private void InstantiatePlusTextPrefab()
        {
            GameObject scoreTextPref = Instantiate(plusScoreTextPrefab.gameObject, transform);
            DestroyPrefab(scoreTextPref);
        }

        IEnumerator DestroyPrefab(GameObject textPrefab)
        {
            yield return new WaitForSeconds(2f);
        }

        private void OnEnable()
        {
            ScoreManager.OnMoneyChanged += UpdateScoreText;
            Money.OnSucces += InstantiatePlusTextPrefab;
            Money.OnFailure += InstantiateMinusTextPrefab;
        }

        private void OnDisable()
        {
            ScoreManager.OnMoneyChanged -= UpdateScoreText;
            Money.OnSucces -= InstantiatePlusTextPrefab;
            Money.OnFailure -= InstantiateMinusTextPrefab;
        }
    }
}
