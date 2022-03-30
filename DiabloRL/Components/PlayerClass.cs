using System;
using System.IO;
using System.Linq;
using DiabloRL.Components.Stats;
using DiabloRL.Entities;
using DiabloRL.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SadRogue.Integration.Components;
using Action = DiabloRL.Actions.Action;

namespace DiabloRL.Components;

public class PlayerClass : RogueLikeComponentBase<Player>
{
    public Experience Experience
    {
        get
        {
            var exp = Parent.AllComponents.GetFirstOrDefault<Experience>();
            // add a new experience component if one not already added
            if (exp == null)
            {
                exp = new Experience(0);
                Parent.AllComponents.Add(exp);
            }

            return exp;
        }
    }
    
    public PlayerClass() : base(isUpdate: false, isRender: false, isMouse: false, isKeyboard: false)
    {
        
    }
}