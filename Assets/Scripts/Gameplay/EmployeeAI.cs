using UnityEngine;

namespace SupermarketSim
{
    public sealed class EmployeeAI : MonoBehaviour
    {
        public EmployeeRole Role;
        public float Fatigue;
        public string CurrentTask;
        public enum TaskState { FindTask, Reserve, Navigate, Acquire, Execute, Validate, Release, Fatigue }
        public TaskState State = TaskState.FindTask;

        void Update() {
            Fatigue = Mathf.Clamp01(Fatigue + Time.deltaTime * .0005f);
            switch (State) {
                case TaskState.FindTask: CurrentTask = Role == EmployeeRole.StockWorker ? "Restock" : Role.ToString(); State = TaskState.Reserve; break;
                case TaskState.Reserve: State = TaskState.Navigate; break;
                case TaskState.Navigate: State = TaskState.Acquire; break;
                case TaskState.Acquire: State = TaskState.Execute; break;
                case TaskState.Execute: State = TaskState.Validate; break;
                case TaskState.Validate: State = TaskState.Release; break;
                case TaskState.Release: State = Fatigue > .9f ? TaskState.Fatigue : TaskState.FindTask; break;
                case TaskState.Fatigue: if (Fatigue < .5f) State = TaskState.FindTask; else Fatigue -= Time.deltaTime * .02f; break;
            }
        }
    }
}