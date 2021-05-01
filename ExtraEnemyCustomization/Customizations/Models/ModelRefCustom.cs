using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations.Models
{
    //TODO: Implement me Daddy
    public class ModelRefCustom : EnemyCustomBase
    {
        public override string GetProcessName()
        {
            return "MedelRef";
        }

        public override bool HasPrespawnBody => true;

        public override void Prespawn(EnemyAgent agent)
        {

        }
    }
}
