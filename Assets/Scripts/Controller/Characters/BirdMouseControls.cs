using Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller.Characters
{
    public class BirdMouseControls : MonoBehaviour
    {
        private Vector3 _initialPosition;
        [SerializeField] private float launchPower = 300f;
        [SerializeField] private float maxDragDistance = 1f;
        private bool _isLaunched;

        private void Awake()
        {
            _initialPosition = transform.position;
            Locator.Instance.eventService.OnBirdLaunched += IsLaunched;
        }

        private void IsLaunched()
        {
            _isLaunched = true;
        }

        private void OnMouseDown()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !_isLaunched)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                GetComponent<LineRenderer>().enabled = true;
            }
        }

        private void OnMouseUp()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !_isLaunched)
            {
                GetComponent<LineRenderer>().enabled = false;
                GetComponent<SpriteRenderer>().color = Color.white;
                Vector2 directionToInitialPosition = _initialPosition - transform.position;
                GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * launchPower);
                GetComponent<Rigidbody2D>().gravityScale = 1;
                Locator.Instance.eventService.OnBirdLaunched?.Invoke();
                Locator.Instance.eventService.OnBirdFlying?.Invoke(true);
            }
        }

        private void OnMouseDrag()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !_isLaunched)
            {
                if (Camera.main != null)
                {
                   Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (Vector3.Distance(mousePos, _initialPosition) > maxDragDistance)
                    {
                        //print("a");
                        var tmp =  _initialPosition + (mousePos-_initialPosition).normalized * maxDragDistance;
                        transform.position = new Vector3(tmp.x > _initialPosition.x ? _initialPosition.x: tmp.x , tmp.y, 0);
                    }
                    else
                    {
                        //print("b");
                        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                    }
                        
                }
            }
        }
    }
}