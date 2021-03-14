using System;
using DiabloRL.Components;
using DiabloRL.Enums;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;

namespace DiabloRL.UI
{
    public class GameUI : ControlsConsole
    {
        private const int _buttonWidth = 10;
        private const int _buttonHeight = 1;

        private readonly ColoredGlyph _globeBackgroundStyle = new ColoredGlyph('#', Color.Gray, Color.Black);
        private readonly ColoredGlyph _healthGlobeStyle = new ColoredGlyph('#', Color.Crimson, Color.Black);

        #region Buttons

        private Button _characterButton;
        private Button _questsButton;
        private Button _mapButton;
        private Button _inventoryButton;
        private Button _spellsButton;
        private Button _menuButton;

        #endregion

        #region Labels

        private Label _lifeLabel;
        private Label _manaLabel;

        #endregion

        public GameUI(int width, int height) : base(width, height)
        {
            Position = new Point(0, Game.GameHeight - height);
            Components.Add(new BorderComponent(ConnectedLineThin, Color.White, Color.Black));

            var colors = Colors.CreateDefault();
            colors.ControlBack = Color.Black;
            colors.Text = Color.White;
            colors.Appearance_ControlOver.Foreground = Color.Aqua;
            colors.RebuildAppearances();
            ThemeColors = colors;

            CreateButtons();
            CreateHealthAndManaLabels();

            this.AddObserver(OnPlayerHealthChanged, Stats.DidChangeNotification(StatTypes.MAX_LIFE));
            this.AddObserver(OnPlayerHealthChanged, Stats.DidChangeNotification(StatTypes.LIFE));
        }

        private void OnPlayerHealthChanged(object sender, object args)
        {
            var stats = sender as Stats;
            _lifeLabel.DisplayText = $"{stats?[StatTypes.LIFE]}/{stats?[StatTypes.MAX_LIFE]}";
            Draw(TimeSpan.MinValue);
        }

        private void CreateButtons()
        {
            _characterButton = new Button(_buttonWidth, 1)
            {
                Position = new Point(0,1),
                Text = "CHAR"
            };
            Add(_characterButton);

            _questsButton = new Button(_buttonWidth, 1)
            {
                Position = new Point(0,3),
                Text = "QUESTS"
            };
            Add(_questsButton);

            _mapButton = new Button(_buttonWidth, 1)
            {
                Position = new Point(0, 5),
                Text = "MAP"
            };
            Add(_mapButton);

            _inventoryButton = new Button(_buttonWidth, 1)
            {
                Position = new Point(Width - _buttonWidth, 1),
                Text = "INV"
            };
            Add(_inventoryButton);

            _spellsButton = new Button(_buttonWidth, 1)
            {
                Position = new Point(Width - _buttonWidth, 3),
                Text = "SPELLS"
            };
            Add(_spellsButton);

            _menuButton = new Button(_buttonWidth, 1)
            {
                Position = new Point(Width - _buttonWidth, 5),
                Text = "MENU"
            };
            Add(_menuButton);
        }

        private void CreateHealthAndManaLabels()
        {
            var lifeTitle = new Label(7)
            {
                DisplayText = "LIFE",
                Alignment = HorizontalAlignment.Center,
                Position = new Point(12, 1)
            };
            Add(lifeTitle);

            _lifeLabel = new Label(7)
            {
                Alignment = HorizontalAlignment.Center,
                Position = new Point(12, 3)
            };
            Add(_lifeLabel);

        }
    }
}