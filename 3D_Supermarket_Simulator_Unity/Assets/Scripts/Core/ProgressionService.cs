using UnityEngine;
using System;

namespace SupermarketSim
{
    [Serializable] public class LevelGate { public int Level; public long Xp, CashPaise; public float Reputation; public string Title; }
    public sealed class ProgressionService : MonoBehaviour {
        public int Level { get; private set; } = 1;
        public long XP { get; private set; }
        public float Reputation { get; private set; }
        public LevelGate[] Gates = {
            new LevelGate{Level=1,Xp=0,CashPaise=0,Reputation=0,Title="Small Grocery"},
            new LevelGate{Level=2,Xp=500,CashPaise=2500000,Reputation=10,Title="Local Supermarket"},
            new LevelGate{Level=3,Xp=1500,CashPaise=7500000,Reputation=20,Title="Growing Store"},
            new LevelGate{Level=4,Xp=3500,CashPaise=15000000,Reputation=30,Title="Fresh Market"},
            new LevelGate{Level=5,Xp=7000,CashPaise=30000000,Reputation=40,Title="Busy Supermarket"},
            new LevelGate{Level=6,Xp=12000,CashPaise=60000000,Reputation=50,Title="Large Supermarket"},
            new LevelGate{Level=7,Xp=20000,CashPaise=100000000,Reputation=60,Title="Modern Supermarket"},
            new LevelGate{Level=8,Xp=32000,CashPaise=175000000,Reputation=68,Title="Premium Market"},
            new LevelGate{Level=9,Xp=50000,CashPaise=300000000,Reputation=78,Title="Hypermarket"},
            new LevelGate{Level=10,Xp=80000,CashPaise=500000000,Reputation=88,Title="Supermarket Empire"}
        };
        public void AddXP(int amount) { XP += Math.Max(0, amount); Recalculate(); }
        public void AddReputation(float amount) { Reputation = Mathf.Clamp(Reputation + amount, -100, 100); Recalculate(); }
        void Recalculate() { for (int i = Gates.Length - 1; i >= 0; i--) if (XP >= Gates[i].XP && Reputation >= Gates[i].Reputation) { Level = Gates[i].Level; break; } }
    }
}