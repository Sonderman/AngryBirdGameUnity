using Cinemachine;
using Controller.Characters;
using Services;
using UnityEngine;

namespace Controller
{
    public class CameraTargetGroupUpdater : MonoBehaviour
    {
        private CinemachineTargetGroup _cinemachineTargetGroup;
        private Monster[] _enemies;
        [SerializeField] public float birdRadius;
        [SerializeField] public float birdWeight;
        [SerializeField] public float monsterRadius;
        [SerializeField] public float monsterWeight;
        private void Awake()
        {
            _cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
            Locator.Instance.eventService.OnTargetsChanged += UpdateTargets;
        }

        private void UpdateTargets(Transform trsf)
        {
            _enemies = FindObjectsOfType<Monster>();
            var targets = new CinemachineTargetGroup.Target[_enemies.Length+1];
            targets[0].target = trsf;
            if(_enemies.Length==0) targets[0].radius = 5;
            targets[0].radius = birdRadius;
            targets[0].weight = birdWeight;
            for (int i = 0,j=1; i<_enemies.Length;i++,j++)
            {
                targets[j].target = _enemies[i].transform;
                targets[j].radius = monsterRadius;
                targets[j].weight = monsterWeight;
            }
            _cinemachineTargetGroup.m_Targets = targets;
        }
    }
}