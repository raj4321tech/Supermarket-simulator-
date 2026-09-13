using UnityEngine;
using System;
using System.Collections.Generic;

namespace SupermarketSim
{
    [Serializable] public class Incident {
        public string Id; public IncidentType Type; public IncidentPriority Priority;
        public string LocationId; public bool Resolved; public long CreatedUnix;
    }
    public sealed class IncidentService : MonoBehaviour {
        public List<Incident> Active = new();
        public event Action<Incident> IncidentCreated;
        public void Create(IncidentType type, IncidentPriority priority, string location) {
            var x = new Incident { Id = Guid.NewGuid().ToString("N"), Type = type, Priority = priority, LocationId = location, CreatedUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds() };
            Active.Add(x); IncidentCreated?.Invoke(x);
        }
        public void Resolve(string id) { var x = Active.Find(i => i.Id == id); if (x != null) x.Resolved = true; }
    }
}