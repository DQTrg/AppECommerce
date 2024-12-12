using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppECommerce.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }

        //public void AddToCart(ShoppingCartItem item, int Quantity)
        //{
        //    var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
        //    if (checkExits != null)
        //    {
        //        checkExits.Quantity += Quantity;
        //        checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
        //    }
        //    else
        //    {
        //        Items.Add(item);
        //    }
        //}

        public bool AddToCart(ShoppingCartItem item, int quantity, int availableStock)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existingItem != null)
            {
                // Check if adding the new quantity exceeds available stock
                if (existingItem.Quantity + quantity > availableStock)
                {
                    return false; // Adding would exceed stock
                }

                existingItem.Quantity += quantity;
                existingItem.TotalPrice = existingItem.Price * existingItem.Quantity;
            }
            else
            {
                // Check if initial addition exceeds available stock
                if (quantity > availableStock)
                {
                    return false; // Adding would exceed stock
                }

                item.TotalPrice = item.Price * quantity;
                item.Quantity = quantity;
                Items.Add(item);
            }

            return true; // Addition successful
        }


        public void Remove(int id)
        {
            var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                Items.Remove(checkExits);
            }
        }

        //public void UpdateQuantity(int id, int quantity)
        //{
        //    var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
        //    if (checkExits != null)
        //    {
        //        checkExits.Quantity = quantity;
        //        checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
        //    }
        //}

        public bool UpdateQuantity(int id, int quantity, int availableStock)
        {
            var existingItem = Items.SingleOrDefault(x => x.ProductId == id);
            if (existingItem != null)
            {
                // Check if the new quantity exceeds available stock
                if (quantity > availableStock)
                {
                    return false; // Update fails due to insufficient stock
                }

                existingItem.Quantity = quantity;
                existingItem.TotalPrice = existingItem.Price * existingItem.Quantity;
                return true; // Update successful
            }

            return false; // Item not found in cart
        }


        public decimal GetTotalPrice()
        {
            return Items.Sum(x => x.TotalPrice);
        }
        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            Items.Clear();
        }
    }
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}