using UnityEngine;
using System;
using System.Collections.Generic;

namespace SupermarketSim
{
    [Serializable] public class SupplierDefinition {
        public string Id, Name; public long LandedCostPaise; public float LeadTimeDays = 1, Reliability = .95f;
        public int MinimumOrderQuantity = 1; public float BulkDiscount;
    }
    [Serializable] public class OrderState {
        public string Id, SupplierId; public List<CartLine> Lines = new(); public string Status = "Ordered";
        public long OrderedUnix; public long ExpectedUnix;
    }
    public sealed class SupplierService : MonoBehaviour {
        public List<SupplierDefinition> Suppliers = new();
        public List<OrderState> Orders = new();
        public void Place(OrderState order) { order.Id = Guid.NewGuid().ToString("N"); order.OrderedUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); Orders.Add(order); }
    }
}