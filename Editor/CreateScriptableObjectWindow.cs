using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Adapted from https://bitbucket.org/snippets/Bjarkeck/keRbr4
/// </summary>
public class CreateScriptableObjectWindow : OdinMenuEditorWindow
{
    private ScriptableObject previewObject;

    private string  targetFolder;
    private Vector2 scroll;

    private static readonly HashSet<Type> ScriptableObjectTypes = GetScriptableObjectTypes();

    private Type SelectedType
    {
        get
        {
            var menuItem = this.MenuTree.Selection.LastOrDefault();
            return menuItem?.Value as Type;
        }
    }

    [MenuItem("Assets/Create Scriptable Object", priority = -1000)]
    private static void ShowDialog()
    {
        var path = "Assets";

        var activeObject = Selection.activeObject;

        if (activeObject && AssetDatabase.Contains(activeObject))
        {
            path = AssetDatabase.GetAssetPath(activeObject);

            if (!Directory.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
        }

        var window = CreateInstance<CreateScriptableObjectWindow>();
        window.ShowUtility();

        window.position     = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        window.titleContent = new GUIContent(path);
        window.targetFolder = path.Trim('/');
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        this.MenuWidth     = 270;
        this.WindowPadding = Vector4.zero;

        var tree = new OdinMenuTree(false)
        {
            Config           = { DrawSearchToolbar = true },
            DefaultMenuStyle = OdinMenuStyle.TreeViewStyle
        };

        tree.AddRange(ScriptableObjectTypes.Where(x => !x.IsAbstract), GetMenuPathForType).AddThumbnailIcons();
        tree.SortMenuItemsByName();

        tree.Selection.SelectionConfirmed += _ => this.CreateAsset();
        tree.Selection.SelectionChanged += changeType =>
        {
            if (this.previewObject && !AssetDatabase.Contains(this.previewObject))
            {
                DestroyImmediate(this.previewObject);
            }

            if (changeType != SelectionChangedType.ItemAdded)
            {
                return;
            }

            var t = this.SelectedType;
            if (t != null && !t.IsAbstract)
            {
                this.previewObject = CreateInstance(t) as ScriptableObject;
            }
        };

        return tree;
    }

    private static string GetMenuPathForType(Type type)
    {
        if (type == null || !ScriptableObjectTypes.Contains(type))
            return string.Empty;

        var name = type.Name.Split('`').First().SplitPascalCase();
        return GetMenuPathForType(type.BaseType) + "/" + name;
    }

    protected override IEnumerable<object> GetTargets()
    {
        yield return this.previewObject;
    }

    protected override void DrawEditor(int index)
    {
        this.scroll = GUILayout.BeginScrollView(this.scroll);
        {
            base.DrawEditor(index);
        }
        GUILayout.EndScrollView();

        if (!this.previewObject)
            return;

        GUILayout.FlexibleSpace();
        SirenixEditorGUI.HorizontalLineSeparator(1);

        if (GUILayout.Button("Create Asset", GUILayoutOptions.Height(30)))
        {
            this.CreateAsset();
        }
    }

    private void CreateAsset()
    {
        if (!this.previewObject)
            return;

        var targetName  = this.MenuTree.Selection.First().Name.ToLower();
        var destination = $"{this.targetFolder}/new {targetName}.asset";

        destination = AssetDatabase.GenerateUniqueAssetPath(destination);

        AssetDatabase.CreateAsset(this.previewObject, destination);
        AssetDatabase.Refresh();

        Selection.activeObject      =  this.previewObject;
        EditorApplication.delayCall += this.Close;
    }

    private static HashSet<Type> GetScriptableObjectTypes()
    {
        var customTypes = AssemblyUtilities.GetTypes(AssemblyTypeFlags.CustomTypes);
        var scrobTypes = customTypes.Where(type => type.IsClass                                    &&
                                                typeof(ScriptableObject).IsAssignableFrom(type) &&
                                                !typeof(EditorWindow).IsAssignableFrom(type)    &&
                                                !typeof(Editor).IsAssignableFrom(type));

        return new HashSet<Type>(scrobTypes);
    }
}