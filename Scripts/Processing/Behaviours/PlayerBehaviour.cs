using DiabloRL.Scripts.Cartography.Tiles;

namespace DiabloRL.Scripts.Processing.Behaviours;

public abstract partial class PlayerBehaviour : Behaviour {

    private DiabloEntity _playerEntity;
    public DiabloEntity PlayerEntity => _playerEntity;

    public PlayerBehaviour(DiabloEntity playerEntity) {
        _playerEntity = playerEntity;
    }
}