using DiabloRL.Components;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;

namespace DiabloRL.UI
{
    public class InformationConsole : ControlsConsole
    {
        private Label _firstLabel;
        private Label _secondLabel;
        private Label _thirdLabel;
        private Label _fourthLabel;
        
        public InformationConsole(Point pos) : base(38, 11)
        {
            Position = pos;
            Components.Add(new BorderComponent(ConnectedLineThin, Color.White, Color.Black));

            var colors = Colors.CreateDefault();
            colors.ControlBack = Color.Black;
            colors.Text = Color.White;
            colors.Appearance_ControlOver.Foreground = Color.Aqua;
            colors.RebuildAppearances();
            ThemeColors = colors;
            
            CreateLabels();
        }

        private void CreateLabels()
        {
            _firstLabel = new Label(Width - 2)
            {
                Alignment = HorizontalAlignment.Center,
                DisplayText = "Line # 1",
                Position = new Point(1, 1)
            };
            Add(_firstLabel);

            _secondLabel = new Label(Width - 2)
            {
                Alignment = HorizontalAlignment.Center,
                DisplayText = "Line # 2",
                Position = new Point(1, 3)
            };
            Add(_secondLabel);

            _thirdLabel = new Label(Width - 2)
            {
                Alignment = HorizontalAlignment.Center,
                DisplayText = "Line # 3",
                Position = new Point(1, 5)
            };
            Add(_thirdLabel);

            _fourthLabel = new Label(Width - 2)
            {
                Alignment = HorizontalAlignment.Center,
                DisplayText = "Line # 4",
                Position = new Point(1, 7)
            };
            Add(_fourthLabel);
        }
    }
}