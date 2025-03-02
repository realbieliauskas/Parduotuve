using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore.Metadata;
using Parduotuve.Data.Entities;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Parduotuve.Services
{
    public class Shopping_Cart_Service : IEnumerable<KeyValuePair<int, int>>
    {
        private Dictionary<int, int> ShoppingCart;
        public Shopping_Cart_Service()
        {
            ShoppingCart = new Dictionary<int, int>();
        }
        public void Add (int id)
        {
            if (!ShoppingCart.ContainsKey(id))
            {
                ShoppingCart.Add(id, 1);
            }
            else
            {
                ShoppingCart[id]++;
            }
           
        }
        public void Remove (int id)
        {
            ShoppingCart.Remove(id);
            
        }
        public void Decrease (int id)
        {
            if (ShoppingCart[id]==1)
            {
                Remove(id);
            }
            else if (ShoppingCart[id]>1)
            {
                ShoppingCart[id]--;
            }
            
        }
        public void Increase (int id)
        {
            ShoppingCart[id]++;
           
        }
        public bool IsEmpty ()
        {
            return ShoppingCart.Count == 0;
        }
        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return ShoppingCart.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
