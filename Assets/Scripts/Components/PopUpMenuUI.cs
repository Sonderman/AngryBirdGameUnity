using DG.Tweening;
using Services;
using UnityEngine;

namespace Components
{
    public class PopUpMenuUI : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _rectTransform.DOScale(Vector3.one, Locator.Instance.uiManager.popUpMenuDuration).From(Vector3.zero)
                .SetEase(Ease.Linear);
        }
    }
}