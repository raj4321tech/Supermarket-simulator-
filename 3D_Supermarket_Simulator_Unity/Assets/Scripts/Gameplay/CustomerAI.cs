using UnityEngine;
using System.Collections.Generic;

namespace SupermarketSim
{
    public sealed class CustomerAI : MonoBehaviour
    {
        public CustomerState State { get; private set; } = CustomerState.Enter;
        public int Patience = 100;
        public float ShoppingSpeed = 1f;
        public List<CartLine> ShoppingList = new();
        float stateTimer;

        void Update() {
            stateTimer += Time.deltaTime;
            switch (State) {
                case CustomerState.Enter: Transition(CustomerState.Cart); break;
                case CustomerState.Cart: Transition(CustomerState.Navigate); break;
                case CustomerState.Navigate: if (stateTimer > 1f / Mathf.Max(.1f, ShoppingSpeed)) Transition(CustomerState.Evaluate); break;
                case CustomerState.Evaluate: Transition(ShoppingList.Count > 0 ? CustomerState.Select : CustomerState.Checkout); break;
                case CustomerState.Select: Transition(CustomerState.Checkout); break;
                case CustomerState.Checkout: Transition(CustomerState.Queue); break;
                case CustomerState.Queue: Patience -= Mathf.CeilToInt(Time.deltaTime * 2f); if (Patience <= 0) Transition(CustomerState.Exit); break;
                case CustomerState.Pay: Transition(CustomerState.Receipt); break;
                case CustomerState.Receipt: Transition(CustomerState.Exit); break;
                case CustomerState.Exit: Transition(CustomerState.Review); break;
            }
        }
        void Transition(CustomerState next) { State = next; stateTimer = 0; }
    }
}