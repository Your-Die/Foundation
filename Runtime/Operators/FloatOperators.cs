namespace Chinchillada
{
    public class Multiply : IOperator<float>
    {
        public float Execute(float left, float right) => left * right;
    }
    
    public class Add : IOperator<float>
    {
        public float Execute(float left, float right) => left + right;
    }
    
    public class Divide : IOperator<float>
    {
        public float Execute(float left, float right) => left / right;
    }
    
    public class Subtract : IOperator<float>
    {
        public float Execute(float left, float right) => left - right;
    }
}