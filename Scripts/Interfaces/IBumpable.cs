using DiabloRL.Scripts.Cartography.Tiles;

namespace DiabloRL.Scripts.Interfaces;

public interface IBumpable {
    bool OnBumped(DiabloGameObject source);
}