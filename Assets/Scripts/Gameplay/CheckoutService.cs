using System;
using UnityEngine;

namespace SupermarketSim
{
    public sealed class CheckoutService : MonoBehaviour
    {
        public event Action<Transaction> TransactionCompleted;
        public bool Process(Transaction tx) {
            if (tx == null || tx.Lines == null || tx.Lines.Count == 0) return false;
            long cogs = 0;
            foreach (var line in tx.Lines)
                if (!InventoryService.Instance.Sell(line.ProductId, line.Quantity, out var lineCogs)) return false;
                else cogs += lineCogs;
            tx.CogsPaise = cogs;
            tx.TotalPaise = tx.SubtotalPaise + tx.TaxPaise;
            tx.Id = Guid.NewGuid().ToString("N");
            tx.UnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            TransactionCompleted?.Invoke(tx);
            return true;
        }
    }
}