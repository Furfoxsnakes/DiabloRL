using System;
using System.Collections.Generic;
using GoRogue;
using GoRogue.MapViews;
using GoRogue.Random;

namespace DiabloRL.Common.Cartography
{

    
    
    public static class MapGenerators
    {
        private const int MaxLeafSize = 20;
        private static List<Leaf> _leaves;
        
        public static void GenerateBSPDungeon(ISettableMapView<bool> map)
        {
            _leaves = new List<Leaf>();

            var random = SingletonRandom.DefaultRNG;
            
            // start with a root leaf the size of the entire dungeon to start the process
            var rootLeaf = new Leaf(0, 0, map.Width, map.Height);
            _leaves.Add(rootLeaf);
            
            // loop until no longer able to split
            var didSplit = true;
            while (didSplit)
            {
                didSplit = false;
                // TODO: BUG: Unhandled exception. System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
                foreach (var leaf in _leaves)
                    if (leaf.LeftChild == null && leaf.RightChild == null)
                        if (leaf.Width > MaxLeafSize || leaf.Height > MaxLeafSize || random.NextDouble() > 0.25f)
                            if (leaf.Split())
                            {
                                _leaves.Add(leaf.LeftChild);
                                _leaves.Add(leaf.RightChild);
                                didSplit = true;
                            }
            }
            
            rootLeaf.CreateRooms();
            
            // carve out the rooms
            foreach (var leaf in _leaves)
            {
                CarveRoom(map, leaf.Room);
            }
        }

        private static void CarveRoom(ISettableMapView<bool> map, Rectangle room)
        {
            foreach (var position in map.Positions())
            {
                map[position] = true;
            }
        }
    }
}