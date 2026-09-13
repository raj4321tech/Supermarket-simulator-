using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SupermarketSim
{
    public sealed class InventoryService : MonoBehaviour
    {
        public static InventoryService Instance { get; private set; }
        public readonly Dictionary<string, ProductDefinition> Products = new();
        public readonly Dictionary<string, ProductBatchState> Batches = new();
        public readonly Dictionary<string, ShelfState> Shelves = new();
        public event Action<string> InventoryChanged;

        void Awake() { if (Instance != null && Instance != this) { Destroy(gameObject); return; } Instance = this; }

        public int GetAvailable(string productId) =>
            Batches.Values.Where(b => b.ProductId == productId && b.Condition == "Good").Sum(b => b.Quantity);

        public void AddBatch(ProductBatchState batch) {
            Batches[batch.BatchId] = batch;
            InventoryChanged?.Invoke(batch.ProductId);
        }

        public bool Sell(string productId, int quantity, out long cogs) {
            cogs = 0; if (quantity <= 0) return false;
            var candidates = Batches.Values.Where(b => b.ProductId == productId && b.Condition == "Good" && b.Quantity > 0)
                .OrderBy(b => b.ExpiryUnix).ThenBy(b => b.ReceivedUnix).ToList();
            if (candidates.Sum(b => b.Quantity) < quantity) return false;
            int left = quantity;
            foreach (var b in candidates) {
                int take = Math.Min(left, b.Quantity);
                b.Quantity -= take; left -= take; cogs += take * b.UnitCostPaise;
                if (left == 0) break;
            }
            InventoryChanged?.Invoke(productId); return true;
        }

        public int Expire(long nowUnix) {
            int waste = 0;
            foreach (var b in Batches.Values.Where(x => x.Condition == "Good" && x.ExpiryUnix > 0 && x.ExpiryUnix <= nowUnix)) {
                waste += b.Quantity; b.Condition = "Expired"; b.Quantity = 0;
            }
            return waste;
        }
    }
}