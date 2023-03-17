namespace Chinchillada.Tests
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class UnityTestUtil
    {
        private static readonly List<Object> CreatedObjects = new List<Object>();
        
        public static T CreateGameObjectWithSetup<T>(Action<T> setup) where T : Component
        {
            var gameObject = CreateGameObject();
            
            gameObject.SetActive(false);

            var component = gameObject.AddComponent<T>();
            setup.Invoke(component);

            gameObject.SetActive(true);

            return component;
        }
        
        
        public static T CreateGameObjectWith<T>() where T : Component
        {
            var gameObject = CreateGameObject();
            return gameObject.AddComponent<T>();
        }

        public static T CreateScriptableObject<T>() where T : ScriptableObject
        {
            var scriptableObject = ScriptableObject.CreateInstance<T>();
            
            CreatedObjects.Add(scriptableObject);

            return scriptableObject;
        }
        
        public static GameObject CreateGameObject()
        {
            var gameObject = new GameObject();
            
            CreatedObjects.Add(gameObject);
            
            return gameObject;
        }
        
        public static void TearDownObjects()
        {
            foreach (var gameObject in CreatedObjects) 
                Object.DestroyImmediate(gameObject);

            CreatedObjects.Clear();
        }
    }
}