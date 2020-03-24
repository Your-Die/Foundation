using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PoolingList<TItem> : IReadOnlyList<TItem> where TItem : Component, IPoolable
{
    [SerializeField] private TItem prefab;
    [SerializeField] private Transform parent;

    [SerializeField, HideInInspector] private List<TItem> items = new List<TItem>();
    [SerializeField, HideInInspector] private Stack<TItem> unusedItems = new Stack<TItem>();

    public event Action<TItem> ItemAdded;
    public event Action<TItem> ItemDeactivated;

    public int Count => this.items.Count;

    public TItem this[int index]
    {
        get => this.items[index];
        set => this.items[index] = value;
    }

    public PoolingList()
    {
        
    }
    
    public PoolingList(TItem prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    public int ApplyWith<TOther>(IList<TOther> list, Action<TOther, TItem> action)
    {
        var count = list.Count;
        var delta = this.Scope(count);

        for (var i = 0; i < list.Count; i++)
        {
            var other = list[i];
            var item = this.items[i];

            action(other, item);
        }

        return delta;
    }

    public void Apply<TOther>(TOther other, Action<TOther, TItem> action)
    {
        this.Scope(1);
        var item = this.items.First();

        action(other, item);
    }

    public void ForEach(Action<TItem> action)
    {
        foreach (var item in this.items)
        {
            action(item);
        }
    }

    public int Scope(int count)
    {
        var delta = 0;

        while (this.items.Count < count)
        {
            delta++;
            this.Acquire();
        }

        while (this.items.Count > count)
        {
            var item = this.items.ExtractLast();
            item.OnRelease();    

            item.gameObject.SetActive(false);
            this.unusedItems.Push(item);

            delta--;
            this.ItemDeactivated?.Invoke(item);
        }

        return delta;
    }

    public void Clear()
    {
        this.Scope(0);
    }

    public TItem Acquire()
    {
        var item = this.unusedItems.Any()
            ? this.unusedItems.Pop()
            : Object.Instantiate(this.prefab, this.parent);

        item.gameObject.SetActive(true);
        this.items.Add(item);

        this.ItemAdded?.Invoke(item);
        return item;
    }

    public IEnumerator<TItem> GetEnumerator()
    {
        return this.items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int IndexOf(TItem item) => this.items.IndexOf(item);
}