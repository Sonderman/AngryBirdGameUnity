using Services;
using UnityEngine;

namespace Controller.Managers
{
    public class InputManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Locator.Instance.eventService.OnEscapeKeyPressed?.Invoke();
            }
        }
    }
}