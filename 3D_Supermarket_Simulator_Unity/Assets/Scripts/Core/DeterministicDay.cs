using UnityEngine;

namespace SupermarketSim {
    public sealed class DeterministicDay : MonoBehaviour {
        public int DayIndex = 1;
        public int Seed => unchecked(7919 * DayIndex + 104729);
        public System.Random Random => new System.Random(Seed);
        public void NextDay() => DayIndex++;
    }
}