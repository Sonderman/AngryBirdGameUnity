namespace Components
{
    public interface IBreakable
    {
        void DecreaseDuration(int amount);
        void Break();
    }

    public interface IDestroyable
    {
        void Die();
    }
    
}