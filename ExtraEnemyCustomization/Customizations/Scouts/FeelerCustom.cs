using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations.Scouts
{
    public class FeelerCustom : EnemyCustomBase
    {
        //TODO: Implement me Daddy
        public override string GetProcessName()
        {
            return "ScoutFeeler";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {

        }
    }
}
