using System.Collections.Generic;
using System.IO;
using DiabloRL.Actions;
using DiabloRL.Components;
using DiabloRL.Enums;
using DiabloRL.Models;
using GoRogue;
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
        
        public Monster(Color foreground, Color background, int glyph, string name, Coord position) : base(foreground, background, glyph, name, position, (int)MapLayer.MONSTERS, false, true)
        {
            // PlayerBumpAction = new BumpActor(this, Game.MapScreen.Map.ControlledGameObject);
            AddGoRogueComponent(new MoveToPlayer());
        }

        protected MonsterData? GetDataFromJson(string monsterType)
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
    }
}