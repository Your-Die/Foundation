using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Chinchillada.Editor
{
    public class InterfaceReferenceCodeGenerator : OdinEditorWindow
    {
        [SerializeField, Sirenix.OdinInspector.FilePath]
        private string path;

        private List<Type> interfaceTypes;
        private List<Type> types;

        [ShowInInspector, ReadOnly] private List<string> interfaceTypeNames;

        [ShowInInspector, ReadOnly] private List<string> typeNames;

        [MenuItem("Window/Interface reference Code Generator")]
        public static void ShowWindow()
        {
            var window = GetWindow<InterfaceReferenceCodeGenerator>();
            window.Show();
        }

        [Button]
        public void CollectInterfaces()
        {
            var gameTypes = AssemblyUtilities.GetTypes(AssemblyTypeFlags.GameTypes);

            this.interfaceTypes = gameTypes.Where(type => type.IsInterface)
                                           .Where(type => type.HasAttribute<GenerateReferencesAttribute>()).ToList();

            this.interfaceTypeNames = this.interfaceTypes.Select(type => type.Name).ToList();
        }


        [Button]
        public void CollectTypesForInterfaceAtIndex(int index)
        {
            Type interfaceType = this.GetInterfaceAt(index);
            
            this.types = CollectTypesForInterface(interfaceType).ToList();
            this.typeNames = this.types.Select(type => type.Name).ToList();
        }

        private Type GetInterfaceAt(int index)
        {
            if (!this.interfaceTypes.ContainsIndex(index))
                throw new IndexOutOfRangeException($"index: {index}, count: {this.interfaceTypes.Count}");

            Type interfaceType = this.interfaceTypes[index];
            return interfaceType;
        }

        [Button]
        public void GenerateReferencesForInterfaceAtIndex(int index)
        {
            Type interfaceType = this.GetInterfaceAt(index);
            var implementingTypes = CollectTypesForInterface(interfaceType);

        }


        private static IEnumerable<Type> CollectTypesForInterface(Type interfaceType)
        {
            var gameTypes = AssemblyUtilities.GetTypes(AssemblyTypeFlags.GameTypes);
            Type unityObjectType = typeof(UnityEngine.Object);

            foreach (Type gameType in gameTypes)
            {
                if (IsValidTarget(gameType) && !IsValidTarget(gameType.BaseType)) 
                    yield return gameType;
            }
            

            bool IsValidTarget(Type type)
            {
                if (type == null)
                    return false;

                return interfaceType.IsAssignableFrom(type) && 
                       unityObjectType.IsAssignableFrom(type);
            }

        }
        

    }
}