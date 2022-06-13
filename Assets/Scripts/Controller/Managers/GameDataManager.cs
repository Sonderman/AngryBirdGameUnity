using Controller.Characters;
using Model;
using Services;
using UnityEngine;

namespace Controller.Managers
{
    public class GameDataManager : MonoBehaviour
    {
        private static GameData _gameData;
        private int _enemyAmount;
        [SerializeField] public int monsterKillOffsetY;
        [SerializeField] public float birdKillSteadyTime;
        [SerializeField] private int defaultBirdAmount;

        public GameData GetGameData()
        {
            return _gameData;
        }

        private void Awake()
        {
            if (IsNewGame() || _gameData == null) _gameData = new GameData(defaultBirdAmount);
            else if (_gameData.birds == 0) _gameData.OnRestartLevel();
            
        }

        private void Start()
        {
            Locator.Instance.eventService.OnBirdDestroyed += DecreaseBirdAmount;
            Locator.Instance.eventService.OnScoreDataChanged?.Invoke(_gameData.score);
            Locator.Instance.eventService.OnBirdsDataChanged?.Invoke(_gameData.birds);
            Locator.Instance.eventService.OnEnemyDeath += IncreaseScore;
            Locator.Instance.eventService.OnBlockBrokenValue += IncreaseScore;
            Locator.Instance.eventService.OnSaveData += SaveGame;
            var e = FindObjectsOfType<Monster>();
            _enemyAmount = e.Length;
        }

        private bool IsNewGame()
        {
            var gobj = GameObject.Find("NewGame");
            if (gobj != null)
            {
                Destroy(gobj.gameObject);
                return true;
            }

            return false;
        }

        private void IncreaseScore(int val, Transform t) // In GameManager Class
        {
            _gameData.score += val;
            Locator.Instance.eventService.OnScoreDataChanged?.Invoke(_gameData.score);
            Locator.Instance.eventService.OnEnemyAmountChanged?.Invoke(--_enemyAmount);
        }
        private void IncreaseScore(int val)
        {
            _gameData.score += val;
            Locator.Instance.eventService.OnScoreDataChanged?.Invoke(_gameData.score);
        }

        private void DecreaseBirdAmount()
        {
            Locator.Instance.eventService.OnBirdsDataChanged?.Invoke(--_gameData.birds);
        }

        private void SaveGame(int index)
        {
            _gameData.lastLevelIndex = index;
            GameData.SaveGame(_gameData);
        }
    }
}