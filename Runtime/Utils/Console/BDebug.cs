using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigasTools{
    public static class BDebug
    {
        public static List<object> cache = new List<object>();
        //
        // Use this to debug log with color and a "title" it's useful so you can search by logs with the same title;
        // To check logs related to some class you are testing;
        // You can use the cache bool to limit update logs.
        public static void Log(object message, string title = "Log", Color? color = null, bool isCache = false){
            if(cache.Contains(message) && isCache)return;
            var c = color == null ? Color.yellow : color.Value;
            if(isCache)cache.Add(message);
            Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}><b>{3}</b> || </color>{4}", (byte)(c.r * 255f), (byte)(c.g * 255f), (byte)(c.b * 255f), title, message)); 
        }
        public static void EraseCache(){
            cache = new List<object>();
        }
    }
}
