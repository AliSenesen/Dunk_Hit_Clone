using System;
using System.Threading.Tasks;
using Enums;
using Gamemanager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI gainScoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private Slider slider;
    
        private bool isOverTime;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetTimer += OnSetTimer;
            UISignals.Instance.onCheckOverTime += OnCheckOverTime;
            UISignals.Instance.onSetScoreChange += OnSetScoreChange;
            UISignals.Instance.onSetGainScoreChange += OnSetGainScoreChange;
            UISignals.Instance.onSetHighScoreChange += OnSetHighScore;
        }

       
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetTimer -= OnSetTimer;
            UISignals.Instance.onCheckOverTime -= OnCheckOverTime;
            UISignals.Instance.onSetScoreChange -= OnSetScoreChange;
            UISignals.Instance.onSetGainScoreChange -= OnSetGainScoreChange;
            UISignals.Instance.onSetHighScoreChange -= OnSetHighScore;

        }

        

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            if (isOverTime)
            {
                SetTimer();
            }
        }

        private void OnPlay()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
        
        private void SetTimer()
        {
            slider.value -= 0.02f;
            if (slider.value <= 0)
            {
                isOverTime = false;
                InputSignals.Instance.onInputClose?.Invoke(isOverTime);
            }
        }
       
        private void OnSetHighScore(int score)
        {
            highScoreText.text = "HighScore: "+ score.ToString();
        }

        private async void OnSetGainScoreChange(int point)
        {
            gainScoreText.gameObject.SetActive(true);
            gainScoreText.text ="+" + point.ToString();
            await Task.Delay(500);
            gainScoreText.gameObject.SetActive(false);
        }

        private void OnSetScoreChange(int currentScore)
        {
            scoreText.text = currentScore.ToString();
        }

        private void OnCheckOverTime()
        {
            if (isOverTime == false)
            {
                CoreGameSignals.Instance.onReset?.Invoke();
            }
        }

        private void OnSetTimer()
        {
            isOverTime = true;
            InputSignals.Instance.onInputClose?.Invoke(isOverTime);
            slider.value = 5;

        }


        public void PlayButton()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Playing);
           
        }
        private void OnOpenPanel(UIPanels panelState)
        {
            uiPanelController.OpenPanel(panelState);
        }

        private void OnClosePanel(UIPanels panelState)
        {
            uiPanelController.ClosePanel(panelState);
        }
        
        private void OnChangeGameState(GameStates currentState)
        {
            if(currentState == GameStates.GameOpen){}
        }
        
        private void OnReset()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            isOverTime = true;
            slider.value = 5;
            scoreText.text = "0";
        }
        
    }
}