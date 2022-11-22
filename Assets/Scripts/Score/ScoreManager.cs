using System;
using Basket;
using Datas.UnityObject;
using Datas.ValueObject;
using Gamemanager;
using UI;
using UnityEngine;

namespace Score
{
    public class ScoreManager : MonoBehaviour
    {
        public ScoreData Data;

        private void Start()
        {
            GetRef();
        }

        private void GetRef()
        {
            Data = GetScoreData;
            Data.score = 0;
            UISignals.Instance.onSetScoreChange?.Invoke(Data.score);
            UISignals.Instance.onSetHighScoreChange?.Invoke(Data.highScore);
        }

        private ScoreData GetScoreData => Resources.Load<CD_Score>("Datas/CD_Score").ScoreData;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BasketSignals.Instance.onChangeBasketDirection += OnGainScore;
            CoreGameSignals.Instance.onReset += OnReset;
        }


        private void UnSubscribeEvents()
        {
            BasketSignals.Instance.onChangeBasketDirection -= OnGainScore;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnGainScore()
        {
            Data.score += 1;
            UISignals.Instance.onSetScoreChange?.Invoke(Data.score);
            UISignals.Instance.onSetGainScoreChange?.Invoke(Data.gainScore);
            UISignals.Instance.onSetTimer?.Invoke();
            if (Data.highScore < Data.score)
            {
                Data.highScore = Data.score;
                UISignals.Instance.onSetHighScoreChange?.Invoke(Data.highScore);
            }
        }
        private void OnReset()
        {
            Data.score = 0;
        }
        
    }
}