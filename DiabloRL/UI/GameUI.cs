using System;
using DiabloRL.Components;
using DiabloRL.Enums;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Input;
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

        private PotionsBar _potionsBar;
        private InformationConsole _informationConsole;
        private MenuWindow _menuWindow;

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

            CreateInformationConsole();
            CreatePotionsBar();
            CreateButtons();
            CreateHealthAndManaLabels();

            this.AddObserver(OnPlayerHealthChanged, Stats.DidChangeNotification(StatTypes.MAX_LIFE));
            this.AddObserver(OnPlayerHealthChanged, Stats.DidChangeNotification(StatTypes.LIFE));
            
            this.AddObserver(OnPlayerManaChanged, Stats.DidChangeNotification(StatTypes.MAX_MANA));
            this.AddObserver(OnPlayerManaChanged, Stats.DidChangeNotification(StatTypes.MANA));

        }

        private void OnPlayerManaChanged(object sender, object args)
        {
            _manaLabel.DisplayText = $"{Game.Player.Stats?[StatTypes.MANA]}/{Game.Player.Stats?[StatTypes.MAX_MANA]}";
            IsDirty = true;
        }

        private void OnPlayerHealthChanged(object sender, object args)
        {
            _lifeLabel.DisplayText = $"{Game.Player.Stats[StatTypes.LIFE]}/{Game.Player.MaxLife}";
            IsDirty = true;
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
            _inventoryButton.MouseButtonClicked += OnInventoryButtonClicked;
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
            _menuButton.MouseButtonClicked += OnMenuButtonClicked;
            Add(_menuButton);
        }

        private void OnInventoryButtonClicked(object? sender, MouseEventArgs e)
        {
            if (!Game.PlayingScreen.InventoryWindow.IsVisible)
                Game.PlayingScreen.InventoryWindow.Show();
            else
                Game.PlayingScreen.InventoryWindow.Hide();
        }

        private void OnMenuButtonClicked(object? sender, MouseEventArgs e)
        {
            if (!Game.PlayingScreen.MenuWindow.IsVisible)
                Game.PlayingScreen.MenuWindow.Show();
            else
                Game.PlayingScreen.MenuWindow.Hide();

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
                DisplayText = $"{Game.Player.Stats[StatTypes.LIFE]}/{Game.Player.MaxLife}",
                Position = new Point(12, 3)
            };
            Add(_lifeLabel);

            var manaTitle = new Label(7)
            {
                DisplayText = "MANA",
                Alignment = HorizontalAlignment.Center,
                Position = new Point(Width - 19, 1)
            };
            Add(manaTitle);

            _manaLabel = new Label(7)
            {
                Alignment = HorizontalAlignment.Center,
                DisplayText = $"{Game.Player.Stats?[StatTypes.MANA]}/{Game.Player.Stats?[StatTypes.MAX_MANA]}",
                Position = new Point(Width - 19, 3)
            };
            Add(_manaLabel);
        }

        private void CreatePotionsBar()
        {
            _potionsBar = new PotionsBar(new Point(32, 1));
            Children.Add(_potionsBar);
        }

        private void CreateInformationConsole()
        {
            _informationConsole = new InformationConsole(new Point(20, 4));
            Children.Add(_informationConsole);
        }
    }
}