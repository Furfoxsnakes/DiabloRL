using System;
using DiabloRL.Components;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Input;
using SadConsole.Themes;

namespace DiabloRL.UI
{
    public class MenuWindow : Window
    {
        private Button _closeButton;
        
        public MenuWindow() : base(20, 10)
        {
            Title = "Menu";
            TitleAlignment = HorizontalAlignment.Center;
            CanDrag = true;
            CloseOnEscKey = true;

            Components.Add(new BorderComponent(ConnectedLineThin, Color.White, Color.Black));

            _closeButton = new Button(Width)
            {
                TextAlignment = HorizontalAlignment.Center,
                Position = new Point(0, 3),
                Text = "SAVE AND EXIT"
            };
            _closeButton.MouseButtonClicked += OnExitButtonClicked;
            Add(_closeButton);
        }

        private void OnExitButtonClicked(object? sender, MouseEventArgs e)
        {
            SadConsole.Game.Instance.Exit();
        }

        public override void Show(bool modal)
        {
            base.Show(modal);
            Center();
            IsFocused = true;
        }

        public override void Hide()
        {
            base.Hide();
            Game.PlayingScreen.MapConsole.Map.ControlledGameObject.IsFocused = true;
        }
    }
}