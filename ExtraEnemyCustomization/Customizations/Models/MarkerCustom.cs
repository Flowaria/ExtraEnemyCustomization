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
        public bool BlinkIn = false;
        public bool Blink = false;
        public float BlinkDuration = 30.0f;
        public float BlinkMinDelay = 1.0f;
        public float BlinkMaxDelay = 5.0f;


        [JsonIgnore]
        private Sprite _Sprite = null;
        [JsonIgnore]
        private bool _PrespawnOnce = false;

        public override string GetProcessName()
        {
            return "Marker";
        }

        public override bool HasPrespawnBody => true;
        public override void Prespawn(EnemyAgent agent)
        {
            if (!_PrespawnOnce)
            {
                if (!string.IsNullOrEmpty(SpriteName) && !SpriteManager.TryGetSpriteCache(SpriteName, 64.0f, out _Sprite))
                {
                    _Sprite = SpriteManager.GenerateSprite(SpriteName);
                }
                _PrespawnOnce = true;
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
            //marker.SetTitle("wew");
            //marker.SetVisualStates(NavMarkerOption.Enemy | NavMarkerOption.Title, NavMarkerOption.Enemy | NavMarkerOption.Title, NavMarkerOption.Empty, NavMarkerOption.Empty);
            //MINOR: Adding Text for Marker maybe?

            if (_Sprite != null)
            {
                var renderer = marker.m_enemySubObj.GetComponentInChildren<SpriteRenderer>();
                renderer.sprite = _Sprite;
            }

            if (BlinkIn)
            {
                CoroutineManager.BlinkIn(marker.m_enemySubObj.gameObject, 0.0f);
                CoroutineManager.BlinkIn(marker.m_enemySubObj.gameObject, 0.2f);
            }

            if (Blink)
            {
                if (BlinkMinDelay >= 0.0f && BlinkMinDelay < BlinkMaxDelay)
                {
                    float duration = Math.Min(BlinkDuration, agent.EnemyBalancingData.TagTime);
                    float time = 0.4f + UnityEngine.Random.RandomRange(BlinkMinDelay, BlinkMaxDelay);
                    for (; time <= duration; time += UnityEngine.Random.RandomRange(BlinkMinDelay, BlinkMaxDelay))
                    {
                        CoroutineManager.BlinkIn(marker.m_enemySubObj.gameObject, time);
                    }
                }
            }
        }
    }
}
