using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DiabloRL.Actors;
using GoRogue;
using Microsoft.Xna.Framework;
using SadConsole;

namespace DiabloRL
{
    internal enum MapLayer
    {
        TERRAIN,
        ITEMS,
        MONSTERS,
        PLAYER
    }

    internal class DiabloRLMap : BasicMap
    {
        // Handles the changing of tile/entity visiblity as appropriate based on Map.FOV.
        public FOVVisibilityHandler FovVisibilityHandler { get; }

        // Since we'll want to access the player as our Player type, create a property to do the cast for us.  The cast must succeed thanks to the ControlledGameObjectTypeCheck
        // implemented in the constructor.
        public new Player ControlledGameObject
        {
            get => (Player) base.ControlledGameObject;
            set => base.ControlledGameObject = value;
        }

        public ReadOnlyCollection<Monster> Monsters => _monsters.AsReadOnly();
        private List<Monster> _monsters = new List<Monster>();

        public DiabloRLMap(int width, int height)
            // Allow multiple items on the same location only on the items layer.  This example uses 8-way movement, so Chebyshev distance is selected.
            : base(width, height, Enum.GetNames(typeof(MapLayer)).Length - 1, Distance.CHEBYSHEV,
                entityLayersSupportingMultipleItems: LayerMasker.DEFAULT.Mask((int) MapLayer.ITEMS))
        {
            ControlledGameObjectChanged +=
                ControlledGameObjectTypeCheck<Player>; // Make sure we don't accidentally assign anything that isn't a Player type to ControlledGameObject
            FovVisibilityHandler = new DefaultFOVVisibilityHandler(this, ColorAnsi.BlackBright);
        }

        public void AddMonster(Monster monster)
        {
            if (_monsters.Contains(monster))
            {
                System.Console.WriteLine($"{monster} has already been added.");
                return;
            }

            _monsters.Add(monster);
            AddEntity(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            if (!_monsters.Contains(monster))
            {
                System.Console.WriteLine($"Could not find {monster} to remove.");
                return;
            }

            _monsters.Remove(monster);
            RemoveEntity(monster);
        }
    }
}