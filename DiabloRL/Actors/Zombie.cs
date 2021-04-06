using System.Collections.Generic;
using DiabloRL.Components;
using DiabloRL.Enums;
using DiabloRL.Models;
using GoRogue;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace DiabloRL.Actors
{
    public class Zombie : Monster
    {
        public MonsterTypes Type;
        private MonsterData _data;

        public Zombie(Coord position) : base(Color.Firebrick, Color.Black, 'Z', name: "Zombie", position)
        {
            Type = MonsterTypes.UNDEAD;
        }

        public static Zombie Create(Difficulties difficulty, Coord position)
        {
            var stats = GetAndApplyStatsFromData("Zombie", difficulty);
            var zombie = new Zombie(position);
            zombie.AddGoRogueComponent(stats);
            return zombie;
        }

        private void GenerateJsonFile()
        {
            // var monsterData = new MonsterData()
            // {
            //     DungeonLevelMin = 1,
            //     DungeonLevelMax = 2,
            //     MonsterLevel = 1,
            //     HealthRange = new Dictionary<Difficulties, int[]>()
            //     {
            //         {Difficulties.NORMAL, new[]{2,3}},
            //         {Difficulties.NIGHTMARE, new[]{56,59}},
            //         {Difficulties.HELL, new[]{108,112}}
            //     },
            //     ArmourClass = new Dictionary<Difficulties, int>()
            //     {
            //         {Difficulties.NORMAL, 5},
            //         {Difficulties.NIGHTMARE, 55},
            //         {Difficulties.HELL, 85}
            //     },
            //     ToHit = new Dictionary<Difficulties, int>()
            //     {
            //         {Difficulties.NORMAL, 10},
            //         {Difficulties.NIGHTMARE, 95},
            //         {Difficulties.HELL, 130}
            //     },
            //     DamageRange = new Dictionary<Difficulties, int[]>()
            //     {
            //         {Difficulties.NORMAL, new []{2,5}},
            //         {Difficulties.NIGHTMARE, new []{8,14}},
            //         {Difficulties.HELL, new []{14,26}}
            //     },
            //     MagicResistance = new Dictionary<Difficulties, int>()
            //     {
            //         {Difficulties.NORMAL, ResistanceTypes.IMMUNE},
            //         {Difficulties.NIGHTMARE, ResistanceTypes.NONE},
            //         {Difficulties.HELL, ResistanceTypes.IMMUNE}
            //     },
            //     FireResistance = new Dictionary<Difficulties, int>()
            //     {
            //         {Difficulties.NORMAL, ResistanceTypes.NONE},
            //         {Difficulties.NIGHTMARE, ResistanceTypes.NONE},
            //         {Difficulties.HELL, ResistanceTypes.NONE}
            //     },
            //     LightningResistance = new Dictionary<Difficulties, int>()
            //     {
            //         {Difficulties.NORMAL, ResistanceTypes.NONE},
            //         {Difficulties.NIGHTMARE, ResistanceTypes.NONE},
            //         {Difficulties.HELL, ResistanceTypes.NONE}
            //     },
            //     BaseExperience = new Dictionary<Difficulties, int>()
            //     {
            //         {Difficulties.NORMAL, 54},
            //         {Difficulties.NIGHTMARE, 2108},
            //         {Difficulties.HELL, 4216}
            //     }
            // };
            //
            // var json = JsonConvert.SerializeObject(monsterData, Formatting.Indented);
            // System.Console.WriteLine(json);
            
            // File.WriteAllText(@"../Data", JsonConvert.SerializeObject(monsterData, Formatting.Indented));
            // using (StreamWriter file = File.CreateText("C:/Development/C#/DiabloRL/DiabloRL/Data/ZombieData.json"))
            // {
            //     var serializer = new JsonSerializer();
            //     serializer.Serialize(file, monsterData);
            // }
        }
        
    }
}