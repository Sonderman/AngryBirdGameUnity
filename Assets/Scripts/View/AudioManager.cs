using Services;
using UnityEngine;

namespace View
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] public AudioSource audioSource;
        [SerializeField] public AudioClip throwBirdAudioClip;
        [SerializeField] public AudioClip hitObstacleAudioClip;
        [SerializeField] public AudioClip monsterHurtAudioClip;
        [SerializeField] public AudioClip completeLevelAudioClip;
        [SerializeField] public AudioClip failedLevelAudioClip;
        [SerializeField] public AudioClip woodBlockBreakingAudioClip;
        private Camera _mainCamera;
        private void Start()
        {
            _mainCamera= Camera.main;
            Locator.Instance.eventService.OnBirdLaunched += PlayThrowAudioClip;
            Locator.Instance.eventService.OnBirdHitObs+= PlayHitAudioClip;
            Locator.Instance.eventService.OnWinning+= PlayLevelCompletedAudioClip;
            Locator.Instance.eventService.OnLost+= PlayLevelFailedAudioClip;
            Locator.Instance.eventService.OnMonsterDie += PlayMonsterHurtAudioClip;
            Locator.Instance.eventService.OnBlockBroken += PlayWoodBlockBreakingAudioClip;
        }

        private void PlayThrowAudioClip()
        {
            AudioSource.PlayClipAtPoint(throwBirdAudioClip,_mainCamera.transform.position,0.5f);
        }
        private void PlayHitAudioClip()
        {
            AudioSource.PlayClipAtPoint(hitObstacleAudioClip,_mainCamera.transform.position,1f);
        }

        private void PlayLevelCompletedAudioClip()
        {
            audioSource.Stop();
            AudioSource.PlayClipAtPoint(completeLevelAudioClip,_mainCamera.transform.position,1f);
        }
        private void PlayLevelFailedAudioClip()
        {
            audioSource.Stop();
            AudioSource.PlayClipAtPoint(failedLevelAudioClip,_mainCamera.transform.position,1f);
        }

        private void PlayMonsterHurtAudioClip()
        {
            AudioSource.PlayClipAtPoint(monsterHurtAudioClip,_mainCamera.transform.position,1f);
        }

        private void PlayWoodBlockBreakingAudioClip(Vector3 p)
        {
            AudioSource.PlayClipAtPoint(woodBlockBreakingAudioClip,_mainCamera.transform.position,1f);
        }
    }
}
