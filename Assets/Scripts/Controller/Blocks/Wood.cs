using Components;
using Services;
using UnityEngine;

namespace Controller.Blocks
{
    public class Wood : BlockBase, IBreakable
    {
        public void DecreaseDuration(int amount)
        {
            durability -= amount;
        }

        public void Break()
        {
            Locator.Instance.eventService.OnBlockBroken?.Invoke(transform.position);
            Locator.Instance.eventService.OnBlockBrokenValue?.Invoke(scoreValue);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bird") && collision.relativeVelocity.magnitude > durability)
            {
                Break();
            }
            else if (collision.relativeVelocity.magnitude > 5)
            {
                Locator.Instance.eventService.OnBirdHitObs?.Invoke();
                durability /= 2;
            }
        }
    }
}