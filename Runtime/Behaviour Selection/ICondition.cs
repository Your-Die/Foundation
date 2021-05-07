namespace Chinchillada.Behavior
{
    public interface ICondition
    {
        bool Validate();
    }

    public interface ICondition<in T>
    {
        bool Validate(T value);
    }
}