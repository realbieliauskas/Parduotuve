using System.Collections;

namespace Parduotuve.Services;

public class ShoppingCartService : IEnumerable<KeyValuePair<int, int>>
{
    private readonly Dictionary<int, int> _shoppingCart;

    public ShoppingCartService()
    {
        _shoppingCart = new Dictionary<int, int>();
    }

    public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
    {
        return _shoppingCart.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(int id)
    {
        if (!_shoppingCart.ContainsKey(id))
            _shoppingCart.Add(id, 1);
        else
            _shoppingCart[id]++;
    }

    public void Clear()
    {
        _shoppingCart.Clear();
    }

    public void Remove(int id)
    {
        _shoppingCart.Remove(id);
    }

    public void Decrease(int id)
    {
        if (_shoppingCart[id] == 1)
            Remove(id);
        else if (_shoppingCart[id] > 1) _shoppingCart[id]--;
    }

    public void Increase(int id)
    {
        _shoppingCart[id]++;
    }

    public bool IsEmpty()
    {
        return _shoppingCart.Count == 0;
    }
}