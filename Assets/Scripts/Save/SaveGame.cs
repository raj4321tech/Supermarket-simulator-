using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace SupermarketSim
{
    [Serializable] public class SaveGame {
        public int Version = 1;
        public long CreatedUnix;
        public int Level;
        public long XP;
        public float Reputation;
        public long CashPaise;
        public string Checksum;
    }
    public sealed class SaveService : MonoBehaviour {
        const int CurrentVersion = 1;
        string Path => System.IO.Path.Combine(Application.persistentDataPath, "supermarket_save.json");
        public void Save(int level, long xp, float rep, long cash) {
            var dto = new SaveGame { Version=CurrentVersion, CreatedUnix=DateTimeOffset.UtcNow.ToUnixTimeSeconds(), Level=level, XP=xp, Reputation=rep, CashPaise=cash };
            dto.Checksum = Hash(JsonUtility.ToJson(dto));
            string tmp = Path + ".tmp";
            File.WriteAllText(tmp, JsonUtility.ToJson(dto, true));
            if (File.Exists(Path)) File.Copy(Path, Path + ".bak", true);
            File.Copy(tmp, Path, true); File.Delete(tmp);
        }
        public SaveGame Load() {
            if (!File.Exists(Path)) return null;
            try {
                var dto = JsonUtility.FromJson<SaveGame>(File.ReadAllText(Path));
                string old = dto.Checksum; dto.Checksum = null;
                return Hash(JsonUtility.ToJson(dto)) == old ? dto : null;
            } catch { return null; }
        }
        string Hash(string s) { using var sha = SHA256.Create(); return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(s))); }
    }
}