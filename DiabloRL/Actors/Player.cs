using System;
using System.Collections.Generic;
using DiabloRL.Actors;
using DiabloRL.Components;
using DiabloRL.Enums;
using DiabloRL.Models.Equipment;
using DiabloRL.Models.Items;
using DiabloRL.Systems;
using GoRogue;
using GoRogue.DiceNotation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Keyboard = SadConsole.Input.Keyboard;

namespace DiabloRL
{
    // Custom class for the player is used in this example just so we can handle input.  This could be done via a component, or in a main screen, but for simplicity we do it here.
    public class Player : Actor
    {
        private static readonly Dictionary<Keys, Direction> s_movementDirectionMapping = new Dictionary<Keys, Direction>
        {
            {Keys.NumPad7, Direction.UP_LEFT}, {Keys.NumPad8, Direction.UP}, {Keys.NumPad9, Direction.UP_RIGHT},
            {Keys.NumPad4, Direction.LEFT}, {Keys.NumPad6, Direction.RIGHT},
            {Keys.NumPad1, Direction.DOWN_LEFT}, {Keys.NumPad2, Direction.DOWN}, {Keys.NumPad3, Direction.DOWN_RIGHT},
            {Keys.Up, Direction.UP}, {Keys.Down, Direction.DOWN}, {Keys.Left, Direction.LEFT},
            {Keys.Right, Direction.RIGHT}
        };

        public int FOVRadius;
        
        public Stats Stats => GetGoRogueComponent<Stats>();
        public int BaseDamage => Stats[StatTypes.STRENGTH] * GetGoRogueComponent<PlayerLevel>().Level / 100;
        public int MinDamage => Stats[StatTypes.MINDMG] + GetEquipmemtModifiersForStatType(StatTypes.MINDMG);
        public int MaxDamage => Stats[StatTypes.MAXDMG] + GetEquipmemtModifiersForStatType(StatTypes.MAXDMG);
        public int Armour => Stats[StatTypes.DEXTERITY] / 5 + GetEquipmemtModifiersForStatType(StatTypes.ARMOUR);
        public int MaxLife => Stats[StatTypes.MAX_LIFE] + GetEquipmemtModifiersForStatType(StatTypes.MAX_LIFE);
        public int Strength => Stats[StatTypes.STRENGTH] + GetEquipmemtModifiersForStatType(StatTypes.STRENGTH);

        public Player(Coord position)
            : base(Color.White, Color.Black, '@', "Player", position, (int) MapLayer.PLAYER, isWalkable: false,
                isTransparent: true)
        {
            FOVRadius = 10;

            var stats = new Stats();
            stats[StatTypes.STRENGTH] = 30;
            stats[StatTypes.MAGIC] = 10;
            stats[StatTypes.DEXTERITY] = 20;
            stats[StatTypes.VITALITY] = 25;
            stats[StatTypes.MAX_LIFE] = 70;
            stats[StatTypes.LIFE] = 50;
            stats[StatTypes.MAX_MANA] = 10;
            stats[StatTypes.MANA] = 0;
            stats[StatTypes.LGOL] = 2;
            stats[StatTypes.MGOL] = 1;
            AddGoRogueComponent(stats);

            var playerInventory = new Inventory();
            AddGoRogueComponent(playerInventory);
            playerInventory.AddItem(new HealthPotion(10));
            playerInventory.AddItem(new ManaPotion(5));
            playerInventory.AddItem(new Cap());
            playerInventory.AddItem(new SkullCap());
            playerInventory.AddItem(new SmallAxe());
            playerInventory.AddItem(new SmallAxe());

            var playerLevel = new PlayerLevel();
            AddGoRogueComponent(playerLevel);
            playerLevel.Init(1);
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            Direction moveDirection = Direction.NONE;

            // Simplified way to check if any key we care about is pressed and set movement direction.
            foreach (Keys key in s_movementDirectionMapping.Keys)
            {
                if (info.IsKeyPressed(key))
                {
                    moveDirection = s_movementDirectionMapping[key];
                    break;
                }
            }

            if (moveDirection != Direction.NONE)
                Game.InputManager.MovePlayer(this, moveDirection);
            
            return base.ProcessKeyboard(info);
        }

        public override void TakeDamage(int amount)
        {
            // var stats = GetGoRogueComponent<Stats>();

            if (Stats == null)
            {
                System.Console.WriteLine($"{Name} does not have a stats component and therefore cannot take damage.");
                return;
            }
            
            System.Console.WriteLine($"{Name} takes {amount} point(s) of damage.");

            Stats[StatTypes.LIFE] -= amount;

            if (Stats[StatTypes.LIFE] <= 0)
            {
                Die();
                return;
            }
        }

        protected override bool ResolveToHit(Actor defender)
        {
            var monster = defender as Monster;
            var toHit = 50 + (Stats[StatTypes.DEXTERITY] / 2) + Stats[StatTypes.LEVEL]
                        - monster.Stats[MonsterStatTypes.ARMOUR_CLASS];
            return Dice.Roll("1d100") < toHit;
        }

        protected override int ResolveDamage(Actor defender)
        {
            var damage = Game.Random.Next(MinDamage, MaxDamage + 1);
            // Minimum of 1 damage
            damage = Math.Max(1, damage);

            // double damage on critical hit
            if (Dice.Roll("1d100") < Stats[StatTypes.LEVEL])
                damage *= 2;
            
            // other calculations in the future

            return damage;
        }

        private int GetEquipmemtModifiersForStatType(StatTypes type)
        {
            var returnValue = 0;

            var inventory = GetGoRogueComponent<Inventory>();

            foreach (var (key,value) in inventory.EquippedItems)
            {
                if (value == null) continue;
                
                if (value.AffectedStats.ContainsKey(type))
                    returnValue += value.AffectedStats[type];
            }

            return returnValue;
        }
    }
}