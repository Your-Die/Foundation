namespace Chinchillada.Tests
{
    using NUnit.Framework;
    using UnityEngine;

    public abstract class ScriptableObjectTests<T> : UnityObjectTests where T : ScriptableObject
    {
        protected T ScriptableObject { get; private set; }

        [SetUp]
        public void SetUp()
        {
            this.ScriptableObject = UnityTestUtil.CreateScriptableObject<T>();
            this.SetUp(this.ScriptableObject);
        }

        protected abstract void SetUp(T scriptableObject);
    }
}