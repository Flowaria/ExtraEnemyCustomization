using Enemies;
using EECustom.Utils;
using System;

namespace EECustom.Customizations.Strikers
{
	using EaseFunc = Func<float, float, float, float, float>;

	public class StrikerTentacleCustom : EnemyCustomBase
    {
        public GPUCurvyType[] TentacleTypes = new GPUCurvyType[0];
		public TentacleSettingData[] TentacleSettings = new TentacleSettingData[0];

        public override string GetProcessName()
        {
            return "Tentacle";
        }

        public override bool HasPrespawnBody => false;

        public override void Prespawn(EnemyAgent agent)
        {
            
        }

        public override bool HasPostspawnBody => true;
        public override void Postspawn(EnemyAgent agent)
        {
            var tentacleComps = agent.GetComponentsInChildren<MovingEnemyTentacleBase>(true);
            var isTypeEnabled = TentacleTypes.Length > 0;
            var isSettingEnabled = TentacleSettings.Length > 0;

            for (int i = 0; i < tentacleComps.Length; i++)
            {
                var tentacle = tentacleComps[i];

                if (isTypeEnabled)
                {
                    var tenType = TentacleTypes[i % TentacleTypes.Length];
                    tentacle.m_GPUCurvyType = tenType;
                    LogDev($" - Applied Tentacle Type!, index: {i} type: {tenType}");
                }

                if (isSettingEnabled)
                {
                    var setting = TentacleSettings[i % TentacleSettings.Length];
                    tentacle.m_easingIn = setting.GetInEaseFunction();
                    tentacle.m_easingOut = setting.GetOutEaseFunction();
                    tentacle.m_attackInDuration = setting.InDuration.GetAbsValue(tentacle.m_attackInDuration);
                    tentacle.m_attackOutDuration = setting.OutDuration.GetAbsValue(tentacle.m_attackOutDuration);
                    tentacle.m_attackHangDuration = setting.HangDuration.GetAbsValue(tentacle.m_attackHangDuration);
                    LogDev($" - Applied Tentacle Setting!, index: {i}");
                }
            }
        }
    }

    public class TentacleSettingData
    {
		public eEasingType InEaseType = eEasingType.EaseInExpo;
        public eEasingType OutEaseType = eEasingType.EaseOutCirc;

		public ValueBase InDuration = ValueBase.Unchanged;
		public ValueBase OutDuration = ValueBase.Unchanged;
		public ValueBase HangDuration = ValueBase.Unchanged;

		public EaseFunc GetInEaseFunction()
        {
			return GetEaseFunction(InEaseType);
        }

		public EaseFunc GetOutEaseFunction()
		{
			return GetEaseFunction(OutEaseType);
		}

		public static EaseFunc GetEaseFunction(eEasingType easeType)
        {
            return easeType switch
            {
                eEasingType.EaseInQuad => new EaseFunc(Easing.EaseInQuad),
                eEasingType.EaseOutQuad => new EaseFunc(Easing.EaseOutQuad),
                eEasingType.EaseInOutQuad => new EaseFunc(Easing.EaseInOutQuad),
                eEasingType.EaseInCubic => new EaseFunc(Easing.EaseInCubic),
                eEasingType.EaseOutCubic => new EaseFunc(Easing.EaseOutCubic),
                eEasingType.EaseInOutCubic => new EaseFunc(Easing.EaseInOutCubic),
                eEasingType.EaseInQuart => new EaseFunc(Easing.EaseInQuart),
                eEasingType.EaseOutQuart => new EaseFunc(Easing.EaseOutQuart),
                eEasingType.EaseInOutQuart => new EaseFunc(Easing.EaseInOutQuart),
                eEasingType.EaseInQuint => new EaseFunc(Easing.EaseInQuint),
                eEasingType.EaseOutQuint => new EaseFunc(Easing.EaseOutQuint),
                eEasingType.EaseInOutQuint => new EaseFunc(Easing.EaseInOutQuint),
                eEasingType.EaseInSine => new EaseFunc(Easing.EaseInSine),
                eEasingType.EaseOutSine => new EaseFunc(Easing.EaseOutSine),
                eEasingType.EaseInOutSine => new EaseFunc(Easing.EaseInOutSine),
                eEasingType.EaseInExpo => new EaseFunc(Easing.EaseInExpo),
                eEasingType.EaseOutExpo => new EaseFunc(Easing.EaseOutExpo),
                eEasingType.EaseInOutExpo => new EaseFunc(Easing.EaseInOutExpo),
                eEasingType.EaseInCirc => new EaseFunc(Easing.EaseInCirc),
                eEasingType.EaseOutCirc => new EaseFunc(Easing.EaseOutCirc),
                eEasingType.EaseInOutCirc => new EaseFunc(Easing.EaseInOutCirc),
                _ => new EaseFunc(Easing.LinearTween),
            };
        }
    }
}