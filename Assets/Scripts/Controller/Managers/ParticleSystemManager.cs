using Services;
using UnityEngine;

namespace Controller.Managers
{
    public class ParticleSystemManager : MonoBehaviour
    {
        [SerializeField] private GameObject monsterDeadParticlePrefab;
        [SerializeField] private GameObject woodenBlockBreakingParticlePrefab;
        private ParticleSystem _birdParticleSystem;

        private void Start()
        {
            Locator.Instance.eventService.OnEnemyDeath += EnemyDeathParticle;
            Locator.Instance.eventService.OnBirdFlying += PlayStopFlyingParticles;
            Locator.Instance.eventService.OnBlockBroken += WoodenBlockBreakingParticle;

        }

        private void EnemyDeathParticle(int a, Transform t)
        {
            Instantiate(monsterDeadParticlePrefab, t.position, Quaternion.identity);
        }
        private void WoodenBlockBreakingParticle(Vector3 t)
        {
            Instantiate(woodenBlockBreakingParticlePrefab, t, Quaternion.identity);
        }

        private void PlayStopFlyingParticles(bool isFlying)
        {
            _birdParticleSystem=Locator.Instance.gameManager.GetActiveBird().GetComponent<ParticleSystem>();
            if (isFlying)
            {
                _birdParticleSystem.Play();
            }
            else
            {
                _birdParticleSystem.Stop();
            }
        }
    }
}