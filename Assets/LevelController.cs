
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Enemy[] _enemies;
    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }
    private void Update()
    {
        foreach(Enemy enemy in _enemies)
        {
            if(enemy != null)
            {
                return;
            }
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.sceneCountInBuildSettings-1 > currentSceneIndex)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        

    }
}
