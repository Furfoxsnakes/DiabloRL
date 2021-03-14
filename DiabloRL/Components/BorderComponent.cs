using System;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using Console = SadConsole.Console;

namespace DiabloRL.Components
{
    
    /// <summary>
    /// Added to a console to give it a border appearance
    /// </summary>
    public class BorderComponent : IConsoleComponent
    {
        private Console _borderConsole;
        private readonly Cell _borderCellStyle;
        private readonly int[] _borderGlyphs;
        
        public int SortOrder => 0;
        public bool IsUpdate => false;
        public bool IsDraw => false;
        public bool IsMouse => false;
        public bool IsKeyboard => false;

        public BorderComponent(int[] connectedLineStyle, Color fg, Color bg)
        {
            _borderGlyphs = connectedLineStyle;
            _borderCellStyle = new Cell(fg, bg);
        }
        
        public void OnAdded(Console console)
        {
            _borderConsole = new Console(console.Width, console.Height, console.Font);
            _borderConsole.DrawBox(new Rectangle(0, 0, _borderConsole.Width, _borderConsole.Height),
                _borderCellStyle, null, _borderGlyphs);
            _borderConsole.Position = new Point(-1, -1);
            _borderConsole.UseMouse = false;
            _borderConsole.UseKeyboard = false;
            console.Children.Add(_borderConsole);
            console.Resize(console.Width - 2, console.Height - 2, true);
            console.Position += new Point(1, 1);
        }

        public void OnRemoved(Console console)
        {
            if (_borderConsole.Parent != null) _borderConsole.Parent = null;

            _borderConsole = null;
        }
        
        public void Update(Console console, TimeSpan delta) => throw new NotImplementedException();

        public void Draw(Console console, TimeSpan delta) => throw new NotImplementedException();

        public void ProcessMouse(Console console, MouseConsoleState state, out bool handled) => throw new NotImplementedException();

        public void ProcessKeyboard(Console console, Keyboard info, out bool handled) => throw new NotImplementedException();
    }
}