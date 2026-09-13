using UnityEngine;
using System;

namespace SupermarketSim
{
    public sealed class EconomyService : MonoBehaviour
    {
        public long CashPaise { get; private set; }
        public long RevenuePaise { get; private set; }
        public long CogsPaise { get; private set; }
        public long ExpensesPaise { get; private set; }
        public long NetProfitPaise => RevenuePaise - CogsPaise - ExpensesPaise;

        public void Attach(CheckoutService checkout) => checkout.TransactionCompleted += OnTransaction;
        void OnTransaction(Transaction tx) { RevenuePaise += tx.TotalPaise; CogsPaise += tx.CogsPaise; CashPaise += tx.TotalPaise; }
        public void Expense(long paise) { ExpensesPaise += paise; CashPaise -= paise; }
    }
}