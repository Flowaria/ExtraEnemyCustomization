using EECustom.Events;
using EECustom.Managers;
using Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UnityEngine;

namespace EECustom.Customizations.Models
{
    public class MarkerCustom : EnemyCustomBase
    {
        public string SpriteName = string.Empty;
        public Color MarkerColor = Color.red;


        [JsonIgnore]
        private Sprite _Sprite = null;

        public override string GetProcessName()
        {
            return "Marker";
        }

        public override void Initialize()
        {
            if (!string.IsNullOrEmpty(SpriteName))
            {
                SpriteManager.TryCacheSprite(SpriteName);
                _Sprite = SpriteManager.TryGetSprite(SpriteName);
            }
        }

        public override bool HasPostspawnBody => true;
        public override void Postspawn(EnemyAgent agent)
        {
            EnemyMarkerEvents.RegisterOnMarked(agent, OnMarked);
        }

        private void OnMarked(EnemyAgent agent, NavMarker marker)
        {
            marker.m_enemySubObj.SetColor(MarkerColor);

            if(_Sprite != null)
            {
                var renderer = marker.m_enemySubObj.GetComponentInChildren<SpriteRenderer>();
                renderer.sprite = _Sprite;
            }
        }
    }
}
