using DiabloRL.Components;
using DiabloRL.Systems;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;

namespace DiabloRL.UI
{
    public class InventoryWindow : Window
    {
        private ListBox _itemsList;
        private EquippedItemsConsole _equippedItemsConsole;
        private Player _player => Game.Player;
        private Inventory _inventory => _player.GetGoRogueComponent<Inventory>();

        public InventoryWindow(int width, int height, Point pos) : base(width, height)
        {
            Position = pos;
            Components.Add(new BorderComponent(ConnectedLineThin, Color.White, Color.Black));

            Title = "Inventory";
            TitleAlignment = HorizontalAlignment.Center;
            CanDrag = false;
            CloseOnEscKey = true;

            var theme = new WindowTheme();
            theme.FillStyle.Background = Color.Black;
            Theme = theme;

            _equippedItemsConsole = new EquippedItemsConsole(Width, 10);
            Children.Add(_equippedItemsConsole);

            // TODO: should be moved to it's own class to handle the functionality
            _itemsList = new ListBox(Width - 2, Height - _equippedItemsConsole.Height - 1)
            {
                Position = new Point(1, _equippedItemsConsole.Height)
            };
            _itemsList.SelectedItemExecuted += OnItemClicked;
            Add(_itemsList);
            // Show();
        }

        private void OnItemClicked(object? sender, ListBox.SelectedItemEventArgs e)
        {
            var item = _inventory.Items[_itemsList.SelectedIndex];
            // var inventory = _player.GetGoRogueComponent<Inventory>();
            
            if (_inventory == null) return;

            if (!_inventory.UseItem(item)) return;
            
            PopulateItemList();
        }

        public override void Show(bool modal)
        {
            base.Show(modal);
            PopulateItemList();
        }

        private void PopulateItemList()
        {
            _itemsList.Items.Clear();
            _itemsList.IsDirty = true;
            
            foreach (var item in _inventory.Items)
            {
                var display = item.IsEquipped ? $"(E) {item.Name}" : item.Name;
                _itemsList.Items.Add(display);
            }
        }
    }
}