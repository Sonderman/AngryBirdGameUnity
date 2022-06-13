using Services;
using UnityEngine;

namespace Controller.Managers
{
    public class GameManager : MonoBehaviour
    {
        private enum GameStates
        {
            Playing,
            Won,
            Lost,
            Paused
        }

        [SerializeField] private Transform spawnLocation;
        private GameObject _activeBird;
        [SerializeField] private GameObject birdPrefab;
        
        private GameStates _state;


        private void Start()
        {
            _state = GameStates.Playing;
            Time.timeScale = 1;
            SpawnBird();
            SubscribeMethods();
        }

        private void SubscribeMethods()
        {
            Locator.Instance.eventService.OnEnemyAmountChanged += WinCheck;
            Locator.Instance.eventService.OnEscapeKeyPressed += PauseResumeGame;
            Locator.Instance.eventService.OnBirdDestroyed += SpawnBird;
        }
        private void PauseResumeGame()
        {
            if (_state == GameStates.Paused)
            {
                Time.timeScale = 1;
                _state = GameStates.Playing;
                Locator.Instance.eventService.OnPaused?.Invoke();
            }
            else if(_state== GameStates.Playing)
            {
                Time.timeScale = 0;
                _state = GameStates.Paused;
                Locator.Instance.eventService.OnPaused?.Invoke();
            }
        }


        private void SpawnBird()
        {
            if (Locator.Instance.gameDataManager.GetGameData().birds > 0 && (_activeBird== null || !_activeBird.activeSelf))
            {
                if (_state == GameStates.Won)
                {
                    Locator.Instance.eventService.OnWinning?.Invoke();
                    return;
                }

                _activeBird = Instantiate(birdPrefab, spawnLocation.position, Quaternion.identity);
                Locator.Instance.eventService.OnTargetsChanged?.Invoke(_activeBird.transform);
            }
            else if (_state == GameStates.Playing)
            {
                _state = GameStates.Lost;
                Locator.Instance.eventService.OnLost?.Invoke();
            }
        }

        private void WinCheck(int amount)
        {
            if (_state == GameStates.Playing && amount == 0)
            {
                _state = GameStates.Won;
            }
        }

        public GameObject GetActiveBird()
        {
            return _activeBird;
        }
    }
}