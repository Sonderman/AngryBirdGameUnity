using UnityEngine;

namespace Controller.Blocks
{
    public class BlockBase : MonoBehaviour
    {
        [SerializeField] protected int durability;
        [SerializeField] protected int scoreValue;
    }
}
