using Services;
using UnityEngine;

namespace Controller.Characters
{
    public class Bird : MonoBehaviour
    {
        private Vector3 _initialPosition;
        private float _timeSittingAround;
        private bool _isBirdLaunched;

        private void Start()
        {
            _initialPosition = transform.position;
            Locator.Instance.eventService.OnBirdLaunched += BirdLaunched;
        }

        private void BirdLaunched()
        {
            _isBirdLaunched = true;
        }

        private void FixedUpdate()
        {
            GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
            GetComponent<LineRenderer>().SetPosition(0, transform.position);

            if (_isBirdLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
            {
                _timeSittingAround += Time.deltaTime;
            }

            if (_timeSittingAround > Locator.Instance.gameDataManager.birdKillSteadyTime)
            {
                KillBird();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Obstacle"))
            {
                Locator.Instance.eventService.OnBirdFlying?.Invoke(false);
            }
        }

       

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Border"))
            {
                KillBird();
            }
        }

        private void KillBird()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            Locator.Instance.eventService.OnBirdDestroyed?.Invoke();
        }
    }
}