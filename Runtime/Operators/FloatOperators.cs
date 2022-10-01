namespace Chinchillada
{
    public class Multiply : IOperator<float, float>
    {
        public float Execute(float left, float right) => left * right;
    }
    
    public class Add : IOperator<float, float>
    {
        public float Execute(float left, float right) => left + right;
    }
    
    public class Divide : IOperator<float, float>
    {
        public float Execute(float left, float right) => left / right;
    }
    
    public class Subtract : IOperator<float, float>
    {
        public float Execute(float left, float right) => left - right;
    }
}