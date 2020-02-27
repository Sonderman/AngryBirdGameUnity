
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
