using System;
using System.Collections.Generic;
using System.IO;
using DiabloRL.Actions;
using DiabloRL.Components;
using DiabloRL.Enums;
using DiabloRL.Models;
using GoRogue;
using GoRogue.DiceNotation;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using SadConsole.Actions;

namespace DiabloRL.Actors
{
    public class Monster : Actor
    {
        private const string FilePath = "C:/Development/C#/DiabloRL/DiabloRL/Data/Monsters/";
        
        public ActionBase PlayerBumpAction;
        
        public static Dictionary<Difficulties, int[]> DungeonLevels;
        public static Dictionary<Difficulties, int> MonsterLevel;

        public MonsterStats Stats => GetGoRogueComponent<MonsterStats>();
        
        public Monster(Color foreground, Color background, int glyph, string name, Coord position) : base(foreground, background, glyph, name, position, (int)MapLayer.MONSTERS, false, true)
        {
            // PlayerBumpAction = new BumpActor(this, Game.MapScreen.Map.ControlledGameObject);
            AddGoRogueComponent(new MoveToPlayer());
        }

        private static MonsterData? GetDataFromJson(string monsterType)
        {
            var pathString = $"{FilePath}{monsterType}.json";

            if (!File.Exists(pathString))
            {
                System.Console.WriteLine($"File at {pathString} does not exist.");
                return null;
            }

            using var file = File.OpenText(pathString);
            var serializer = new JsonSerializer();
            var monsterData = (MonsterData)serializer.Deserialize(file, typeof(MonsterData));

            return monsterData;
        }

        protected static MonsterStats GetAndApplyStatsFromData(string monsterType, Difficulties difficulty)
        {
            var random = new Random((int)System.DateTime.UtcNow.Ticks);
            
            var data = GetDataFromJson(monsterType);

            var stats = new MonsterStats();

            stats[MonsterStatTypes.DUNGEON_LEVEL_MIN] = data.DungeonLevelMin;
            stats[MonsterStatTypes.DUNGEON_LEVEL_MAX] = data.DungeonLevelMax;
            stats[MonsterStatTypes.MONSTER_LEVEL] = data.MonsterLevel;

            var minHealth = data.HealthRange[difficulty][0];
            var maxHealth = data.HealthRange[difficulty][1];
            // System.Console.WriteLine($"Min: {minHealth}, Max: {maxHealth}");
            stats[MonsterStatTypes.LIFE] = random.Next(minHealth, maxHealth + 1);

            stats[MonsterStatTypes.ARMOUR_CLASS] = data.ArmourClass[difficulty];
            stats[MonsterStatTypes.TO_HIT] = data.ToHit[difficulty];
            stats[MonsterStatTypes.DAMAGE_MIN] = data.DamageRange[difficulty][0];
            stats[MonsterStatTypes.DAMAGE_MAX] = data.DamageRange[difficulty][1];
            stats[MonsterStatTypes.MAGIC_RESIST] = data.MagicResistance[difficulty];
            stats[MonsterStatTypes.FIRE_RESIST] = data.FireResistance[difficulty];
            stats[MonsterStatTypes.LIGHTNING_RESIST] = data.LightningResistance[difficulty];
            stats[MonsterStatTypes.BASE_EXP] = data.BaseExperience[difficulty];

            return stats;

        }

        public override void TakeDamage(int amount)
        {
            var stats = GetGoRogueComponent<MonsterStats>();

            if (stats == null)
            {
                System.Console.WriteLine($"{Name} does not have a stats component and therefore cannot take damage.");
                return;
            }
            
            System.Console.WriteLine($"{Name} takes {amount} point(s) of damage.");

            stats[MonsterStatTypes.LIFE] -= amount;

            if (stats[MonsterStatTypes.LIFE] <= 0)
            {
                Die();
                return;
            }

            // System.Console.WriteLine($"{Name} has {stats[MonsterStatTypes.LIFE]} life remaining");
        }

        protected override bool ResolveToHit(Actor defender)
        {
            var player = defender as Player;
            var toHit = 30 + Stats[MonsterStatTypes.TO_HIT] +
                        (2 * (Stats[MonsterStatTypes.MONSTER_LEVEL] - player.Stats[StatTypes.LEVEL]))
                        - player.Armour;
            return Dice.Roll("1d100") < toHit;
        }

        protected override int ResolveDamage(Actor defender)
        {
            var damage = Game.Random.Next(Stats[MonsterStatTypes.DAMAGE_MIN], Stats[MonsterStatTypes.DAMAGE_MAX] + 1);
            // other calculations in the future
            return damage;
        }

        protected override void Die()
        {
            base.Die();
            (CurrentMap as DungeonMap)?.RemoveMonster(this);
        }
    }
}