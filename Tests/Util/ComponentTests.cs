namespace Chinchillada.Tests
{
    using NUnit.Framework;
    using UnityEngine;

    public abstract class UnityObjectTests
    {
        [TearDown]
        public void TearDown() => UnityTestUtil.TearDownObjects();
    }

    public abstract class ComponentTests<T> : UnityObjectTests where T : Component
    {
        protected T Component { get; private set; }

        [SetUp]
        public void SetUp() => this.Component = UnityTestUtil.CreateGameObjectWithSetup<T>(this.Setup);

        protected abstract void Setup(T component);
    }
}