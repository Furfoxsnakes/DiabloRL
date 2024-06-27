using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Cartography.Tiles.Entities;
using GoRogue.Components.ParentAware;

namespace DiabloRL.Scripts.Processing.Actions.Moves;

/// <summary>
/// A base class applied to Monsters for what kind of Moves they can perform
/// such as a melee attack, range attack, teleport, etc.
/// </summary>
public abstract partial class Move : ParentAwareComponentBase<Monster> {
    public abstract string Description { get; }

    /// <summary>
    /// Override to selectively decide if the monster will use this move
    /// ie: return false if target entity is out of range
    /// </summary>
    public virtual bool WillUseMove(Monster monster, DiabloEntity target) {
        return true;
    }

    /// <summary>
    /// Override to return what action will be returned by this move
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract Action GetAction(Monster monster, DiabloEntity target);
}