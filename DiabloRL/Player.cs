using System.Collections.Generic;
using DiabloRL.Actors;
using DiabloRL.Components;
using GoRogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;
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

        public Player(Coord position)
            : base(Color.White, Color.Black, '@', "Player", position, (int) MapLayer.PLAYER, isWalkable: false,
                isTransparent: true)
        {
            FOVRadius = 10;

            var stats = new Stats(30, 10, 20, 25, 70, 10);
            AddGoRogueComponent(stats);
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
    }
}