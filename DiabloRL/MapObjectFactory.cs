using DiabloRL.AI;
using DiabloRL.Behaviors;
using DiabloRL.Entities;
using SadRogue.Integration;
using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Integration.Keybindings;
using SadRogue.Primitives;

namespace DiabloRL
{
    /// <summary>
    /// Simple class with some static functions for creating map objects.
    /// </summary>
    /// <remarks>
    /// CUSTOMIZATION:  This demonstrates how to create objects based on "composition"; using components.  The integration
    /// library offers a robust component system that integrates both SadConsole's and GoRogue's components into one
    /// interface to support this.  You can either add more functions to create more objects, or remove this and
    /// implement the "factory" system in the GoRogue.Factories namespace, which provides a more robust interface for it.
    ///
    /// Note that SadConsole components cannot be attached directly to `RogueLikeCell` or `MemoryAwareRogueLikeCell`
    /// instances for reasons pertaining to performance.
    ///
    /// Alternatively, you can remove this system and choose to use inheritance to create your objects instead - the
    /// integration library also supports creating subclasses or RogueLikeCell and Entity.
    /// </remarks>
    internal static class MapObjectFactory
    {
        public static MemoryAwareRogueLikeCell Floor(Point position)
            => new(position, Color.White, Color.Black, '.', (int) GameMap.Layer.Terrain);

        public static MemoryAwareRogueLikeCell Wall(Point position)
            => new(position, Color.White, Color.Black, '#', (int) GameMap.Layer.Terrain, false, transparent: false);

        public static Player Player()
        {
            // Create entity with appropriate attributes
            var player = new Player()
            {
                Name = "Player"
            };
            
            player.SetBehavior(new OneShotBehavior(null));

            return player;
        }

        public static Enemy Enemy()
        {
            var enemy = new Enemy(Color.Red, Color.Black, 'g')
            {
                Name = "Goblin"
            };
            
            enemy.SetBehavior(new MonsterBehavior(enemy));

            return enemy;
        }
    }
}