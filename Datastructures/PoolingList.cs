using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PoolingList<TItem> : IReadOnlyList<TItem> where TItem : Component, IPoolable
{
    [SerializeField] private TItem _prefab;
    [SerializeField] private Transform _parent;

    private readonly List<TItem> _items = new List<TItem>();
    private readonly Stack<TItem> _unusedItems = new Stack<TItem>();

    public event Action<TItem> ItemAdded;
    public event Action<TItem> ItemDeactivated;

    public int Count => _items.Count;

    public TItem this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public PoolingList(TItem prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public int ApplyWith<TOther>(IList<TOther> list, Action<TOther, TItem> action)
    {
        var count = list.Count;
        var delta = Scope(count);

        for (var i = 0; i < list.Count; i++)
        {
            var other = list[i];
            var item = _items[i];

            action(other, item);
        }

        return delta;
    }

    public void ForEach(Action<TItem> action)
    {
        foreach (var item in _items)
        {
            action(item);
        }
    }

    public int Scope(int count)
    {
        var delta = 0;

        while (_items.Count < count)
        {
            delta++;
            Acquire();
        }

        while (_items.Count > count)
        {
            var item = _items.ExtractLast();
            item.OnRelease();

            item.gameObject.SetActive(false);
            _unusedItems.Push(item);

            delta--;
            ItemDeactivated?.Invoke(item);
        }

        return delta;
    }

    public void Clear()
    {
        Scope(0);
    }

    public TItem Acquire()
    {
        var item = _unusedItems.Any()
            ? _unusedItems.Pop()
            : Object.Instantiate(_prefab, _parent);

        item.gameObject.SetActive(true);
        _items.Add(item);

        ItemAdded?.Invoke(item);
        return item;
    }

    public IEnumerator<TItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int IndexOf(TItem item) => _items.IndexOf(item);
}