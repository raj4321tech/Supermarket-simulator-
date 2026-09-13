using UnityEngine;
using UnityEngine.UI;

namespace SupermarketSim {
    public sealed class RuntimeDebugHUD : MonoBehaviour {
        public Text Text;
        void Update() {
            if(Text==null) return;
            int products = InventoryService.Instance ? InventoryService.Instance.Batches.Count : 0;
            Text.text = $"PRODUCT BATCHES: {products}\\nTIME: {System.DateTime.Now:HH:mm:ss}\\nFPS: {(1f/Mathf.Max(.0001f,Time.unscaledDeltaTime)):0}";
        }
    }
}