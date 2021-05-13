using Enemies;

namespace EECustom.Customizations.Detections
{
    public class ScreamingCustom : EnemyCustomBase
    {
        //TODO: Implement me Daddy

        public DetectedBehaviourType DetectedBehaviour { get; set; } = DetectedBehaviourType.DoPropagateScream;
        public bool ImmortalDuringScream { get; set; } = true;

        public override string GetProcessName()
        {
            return "Screaming";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
        }
    }

    public enum DetectedBehaviourType
    {
        DoPropagateScream,
        DoScoutScream
    }
}