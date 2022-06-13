using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.Managers
{
    public class LevelManager : MonoBehaviour
    {
        
        private int _currentSceneIndex;

        private void Start()
        {
            Locator.Instance.eventService.OnRestartLevel += RestartLevel;
            Locator.Instance.eventService.OnReturnMenu += ReturnMenu;
            Locator.Instance.eventService.OnLevelFinished += NextLevel;
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public bool IsLastLevel()
        {
            if (SceneManager.sceneCountInBuildSettings - 1 == _currentSceneIndex) return true;
            return false;
        }

        private void RestartLevel()
        {
            DontDestroyOnLoad(new GameObject("NewGame"));
            SceneManager.LoadScene(_currentSceneIndex);
        }

        private void ReturnMenu(bool save)
        {
            
            if (save)
            {
                if(SceneManager.sceneCountInBuildSettings-1 > _currentSceneIndex)
                    Locator.Instance.eventService.OnSaveData?.Invoke(_currentSceneIndex + 1);
                else Locator.Instance.eventService.OnSaveData?.Invoke(_currentSceneIndex);
            }
            SceneManager.LoadScene(0);
        }

        private void NextLevel()
        {
            if (SceneManager.sceneCountInBuildSettings - 1 > _currentSceneIndex)
            {
                SceneManager.LoadScene(_currentSceneIndex + 1);
            }
        }

        
    }
}