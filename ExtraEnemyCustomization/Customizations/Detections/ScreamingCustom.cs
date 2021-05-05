using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations.Detections
{
    public class ScreamingCustom : EnemyCustomBase
    {
        //TODO: Implement me Daddy

        public DetectedBehaviourType DetectedBehaviour = DetectedBehaviourType.DoPropagateScream;
        public bool ImmortalDuringScream = true;

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
