using Services;
using TMPro;
using UnityEngine;

namespace View
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI birdsLeftText;
        public GameObject inGameCanvas;
        public GameObject winCanvas;
        public GameObject losingCanvas;
        public GameObject pauseMenuCanvas;
        public float popUpMenuDuration;
        
        private void SubscibeMethods()
        {
            Locator.Instance.eventService.OnLost += OpenLosingCanvas;
            Locator.Instance.eventService.OnWinning += OpenWinningCanvas;
            Locator.Instance.eventService.OnScoreDataChanged += UpdateScoreUI;
            Locator.Instance.eventService.OnBirdsDataChanged += UpdateBirdsUI;
            Locator.Instance.eventService.OnPaused += OpenClosePauseMenu;
        }
        private void Awake()
        {
            SubscibeMethods();
        }

        private void Start()
        {
            inGameCanvas.SetActive(true);
            winCanvas.SetActive(false);
            losingCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(false);
        }

       

        private void OpenLosingCanvas()
        {
            inGameCanvas.SetActive(false);
            losingCanvas.SetActive(true);
        }

        private void OpenWinningCanvas()
        {
            inGameCanvas.SetActive(false);
            if (Locator.Instance.levelManager.IsLastLevel())
            {
                winCanvas.transform.Find("MenuFrame").Find("NextLevelButton").gameObject.SetActive(false);
                winCanvas.transform.Find("MenuFrame").Find("CompleteText").gameObject.SetActive(true);
            }

            winCanvas.SetActive(true);
        }

        private void UpdateScoreUI(int value)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score : " + value;
            }
        }

        private void UpdateBirdsUI(int value)
        {
            if (birdsLeftText != null)
            {
                birdsLeftText.text = "Birds Left : " + value;
            }
        }

        private void OpenClosePauseMenu()
        {
            pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeInHierarchy);
        }

        public void OnResumeButtonPressed()
        {
            Locator.Instance.eventService.OnEscapeKeyPressed?.Invoke();
        }

        public void OnRestartLevelButtonPressed()
        {
            Locator.Instance.eventService.OnRestartLevel?.Invoke();
        }

        public void OnReturnMenuButtonPressed(bool save)
        {
            Locator.Instance.eventService.OnReturnMenu?.Invoke(save);
        }

        public void OnNextLevelButtonPressed()
        {
            Locator.Instance.eventService.OnLevelFinished?.Invoke();
        }
    }
}