using System;
using DiabloRL.Actions.Basic;
using DiabloRL.Behaviors;
using DiabloRL.Components;
using DiabloRL.Components.Stats;
using DiabloRL.Things;
using GoRogue.Components;
using GoRogue.DiceNotation;
using GoRogue.DiceNotation.Terms;
using SadRogue.Integration;
using SadRogue.Integration.Keybindings;
using SadRogue.Primitives;
using Action = DiabloRL.Actions.Action;

namespace DiabloRL.Entities
{
    public class Player : GameEntity
    {
        public Vitality Vitality => AllComponents.GetFirstOrDefault<Vitality>();
        public Mana Mana => AllComponents.GetFirstOrDefault<Mana>();
        
        public Player() : base(foreground: Color.Yellow, background: Color.Black, glyph: '@', walkable: false, transparent: false, 1)
        {
            // Add component for controlling player movement via keyboard.  Other (non-movement) keybindings can be
            // added as well
            var motionControl = new PlayerKeyboardControls();
            motionControl.SetMotions(PlayerKeybindingsComponent.ArrowMotions);
            motionControl.SetMotions(PlayerKeybindingsComponent.NumPadAllMotions);
            AllComponents.Add(motionControl);

            // Add component for updating map's player FOV as they move
            AllComponents.Add(new PlayerFOVController());
        }

        public override Attack GetAttack(GameEntity defender)
        {
            var damage = Dice.Parse("1d3");
            var element = Element.None;
            var effectType = EffectType.Melee;
            
            // get modifiers based on equipped weapon(s)
            
            // build the attack
            var attack = new Attack(damage, 1, element, effectType);
            
            // allow the defender to modify the attack
            
            // send it back
            return attack;
        }
    }
}