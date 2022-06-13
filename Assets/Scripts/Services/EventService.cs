using UnityEngine;
using UnityEngine.Events;

namespace Services
{
    public class EventService : MonoBehaviour
    {
        #region GameData
        public UnityAction<int> OnScoreDataChanged;
        public UnityAction<int> OnBirdsDataChanged;
        public UnityAction<int> OnEnemyAmountChanged;
        public UnityAction<int, Transform> OnEnemyDeath;
        public UnityAction<int> OnSaveData;
        #endregion

        #region GameManager
        public UnityAction OnLost;
        public UnityAction OnWinning;
        public UnityAction OnPaused;
        #endregion

        #region Input
        public UnityAction OnEscapeKeyPressed;
        #endregion

        #region CameraTarget
        public UnityAction<Transform> OnTargetsChanged;
        #endregion

        #region LevelManager
        public UnityAction OnRestartLevel;
        public UnityAction<bool> OnReturnMenu;
        public UnityAction OnLevelFinished;
        
        #endregion

        #region Bird
        public UnityAction OnBirdLaunched;
        public UnityAction<bool> OnBirdFlying;
        public UnityAction OnBirdHitObs;
        public UnityAction OnBirdDestroyed;
        #endregion
        
        #region Monster
        public UnityAction OnMonsterDie;
        #endregion

        #region Block
        public UnityAction<Vector3> OnBlockBroken;
        public UnityAction<int> OnBlockBrokenValue;
        #endregion

    }
}
