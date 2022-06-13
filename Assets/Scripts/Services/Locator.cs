using Controller.Managers;
using UnityEngine;
using View;

namespace Services
{
    public class Locator : MonoBehaviour
    {
        public static Locator Instance;
        [SerializeField] public EventService eventService;
        [SerializeField] public GameManager gameManager;
        [SerializeField] public UIManager uiManager;
        [SerializeField] public GameDataManager gameDataManager;
        [SerializeField] public ParticleSystemManager particleSystemManager;
        [SerializeField] public LevelManager levelManager;
        [SerializeField] public InputManager inputManager;
        [SerializeField] public AudioManager audioManager;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance.gameObject);
                return;
            }
            Instance = this;
        }
    }
    
}