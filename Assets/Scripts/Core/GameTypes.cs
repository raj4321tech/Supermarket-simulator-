using System;
using System.Collections.Generic;
using UnityEngine;

namespace SupermarketSim
{
    public enum ProductCategory { Grocery, Dairy, Bakery, Produce, Meat, Frozen, Drinks, Snacks, Chocolate, Household, Cleaning, PersonalCare, Electronics, Discount }
    public enum PaymentMethod { Cash, Card, Digital }
    public enum CheckoutState { Closed, Opening, Idle, Serving, Payment, PrintingReceipt, Blocked, Maintenance }
    public enum CustomerState { Enter, Cart, Navigate, Evaluate, Select, Alternative, Skip, Checkout, Queue, Pay, Receipt, Exit, Review }
    public enum EmployeeRole { StockWorker, Cashier, Cleaner, Security, WarehouseWorker, Maintenance, Manager, AssistantManager, ReceivingWorker, Supervisor }
    public enum IncidentType { BrokenShelf, Spill, BlockedAisle, RefrigerationFailure, CheckoutMalfunction, LightingProblem, DeliveryDiscrepancy, DamagedStock, SecurityLoss }
    public enum IncidentPriority { Safety, CriticalStock, Checkout, Access, Cleanliness, Routine }

    [Serializable] public struct Money { public long Paise; public Money(long paise) { Paise = paise; } public float Rupees => Paise / 100f; }
    [Serializable] public class ProductDefinition {
        public string Id, Name, Brand, Barcode, SupplierId;
        public ProductCategory Category;
        public long BasePurchasePaise, BaseRetailPaise;
        public float UnitWeightKg, ShelfCapacity, Demand, Popularity, Quality, ExpiryDays;
        public string[] Tags;
    }
    [Serializable] public class ProductBatchState {
        public string BatchId, ProductId;
        public int Quantity;
        public long ReceivedUnix, ExpiryUnix, UnitCostPaise;
        public string Condition = "Good";
    }
    [Serializable] public class ShelfState {
        public string ShelfId;
        public ProductCategory AllowedCategory;
        public int CapacityUnits, RestockThreshold;
        public List<string> BatchIds = new();
        public int Units { get; set; }
    }
    [Serializable] public class CartLine { public string ProductId; public int Quantity; public long UnitPricePaise; }
    [Serializable] public class Transaction {
        public string Id; public long UnixTime; public List<CartLine> Lines = new();
        public long SubtotalPaise, TaxPaise, TotalPaise, CogsPaise; public PaymentMethod Payment;
    }
}