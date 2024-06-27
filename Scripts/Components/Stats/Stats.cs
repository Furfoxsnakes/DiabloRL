using DiabloRL.Scripts.Cartography.Tiles;
using Godot;
using GoRogue.Components.ParentAware;

namespace DiabloRL.Scripts.Components;

public partial class Stats : ParentAwareComponentBase<DiabloEntity> {

    public Strength Strength { get; private set; }
    public Magic Magic { get; private set; }
    public Dexterity Dexterity { get; private set; }
    public Vitality Vitality { get; private set; }

    public Stats(int strength, int magic, int dexterity, int vitality) {
        Strength = new Strength {
            Base = strength
        };

        Magic = new Magic {
            Base = magic
        };

        Dexterity = new Dexterity {
            Base = dexterity
        };

        Vitality = new Vitality {
            Base = vitality
        };
    }
}

public class Strength : Stat {
        
}

public class Magic : Stat {
        
}

public class Dexterity : Stat {
        
}

public class Vitality : Stat {
        
}