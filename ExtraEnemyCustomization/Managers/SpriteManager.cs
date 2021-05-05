using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnhollowerBaseLib;
using UnityEngine;

namespace EECustom.Managers
{
    public class SpriteManager
    {
        private static readonly Dictionary<string, Sprite> _SpriteCache = new Dictionary<string, Sprite>();

        public static void TryCacheSprite(string fileName)
        {
            fileName = fileName.ToLower();
            if(!_SpriteCache.ContainsKey(fileName))
            {
                var filePath = Path.Combine(ConfigManager.BasePath, "icons", fileName);
                if (!File.Exists(filePath))
                    return;

                var fileData = File.ReadAllBytes(filePath);
                var texture2D = new Texture2D(2, 2);
                if (!ImageConversion.LoadImage(texture2D, fileData))
                    return;

                var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 64.0f);
                _SpriteCache.Add(fileName, sprite);
            }
        }

        public static Sprite TryGetSprite(string fileName)
        {
            fileName = fileName.ToLower();
            if (_SpriteCache.TryGetValue(fileName, out var sprite))
                return sprite;
            return null;
        }
    }
}
