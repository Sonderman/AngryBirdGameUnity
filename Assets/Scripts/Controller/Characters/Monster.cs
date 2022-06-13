using Components;
using Services;
using UnityEngine;

namespace Controller.Characters
{
    public class Monster : MonoBehaviour, IDestroyable
    {
        [SerializeField] private int scoreValue;
        [SerializeField] public float health;
        private int _monsterKillOffsetY;
        private void Start()
        {
            _monsterKillOffsetY = Locator.Instance.gameDataManager.monsterKillOffsetY;
        }

        private void Update()
        {
            if (transform.position.y < _monsterKillOffsetY)
            {
                Die();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.magnitude > health)
            {
                Die();
            }else if (collision.relativeVelocity.magnitude > 3)
            {
                health /= 2;
            }
        }


        public void Die()
        {
            Locator.Instance.eventService.OnEnemyDeath?.Invoke(scoreValue, transform);
            Locator.Instance.eventService.OnMonsterDie?.Invoke();
            Destroy(gameObject);
        }
    }
}