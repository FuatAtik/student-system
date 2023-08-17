using System.Text.Json;
using Kusys.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Kusys.WebUI.Models;

public static class CurrentSession
{
    private static readonly Dictionary<string, object> SessionData = new();

    public static void Set<T>(string key, T value)
    {
        SessionData[key] = value;
    }
    
    public static User? User => Get<User>("login");


    private static T Get<T>(string key)
    {
        return SessionData.ContainsKey(key) ? (T)SessionData[key] : default(T);
    }
    
    public static bool Remove(string key)
    {
        if (!SessionData.ContainsKey(key)) return false;
        SessionData.Remove(key);
        return true;
    }

    public static void Clear()
    {
        SessionData.Clear();
    }
}