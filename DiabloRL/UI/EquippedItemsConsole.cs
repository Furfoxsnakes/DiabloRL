using System.Collections.Generic;
using DiabloRL.Enums;
using DiabloRL.Models.Equipment;
using DiabloRL.Systems;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;

namespace DiabloRL.UI
{
    public class EquippedItemsConsole : ControlsConsole
    {
        private Label _headSlotLabel;
        private Label _neckSlotLabel;
        private Label _torsoSlotLabel;
        private Label _mainHandSlotLabel;
        private Label _offHandSlotLabel;
        private Label _leftRingSlotLabel;
        private Label _rightRingSlotLabel;

        public EquippedItemsConsole(int width, int height) : base(width, height)
        {
            GenerateLabels();
            this.AddObserver(OnItemEquipped, Equippable.EquipNotification);
        }

        private void OnItemEquipped(object sender, object args)
        {
            var equippable = sender as Equippable;

            var displayText = equippable.IsEquipped ? equippable.Name : "";
            
            switch (equippable.EquipSlot)
            {
                case EquipSlots.HEAD:
                {
                    _headSlotLabel.DisplayText = displayText;
                    _headSlotLabel.IsDirty = true;
                    break;
                }
                case EquipSlots.NECK:
                {
                    _neckSlotLabel.DisplayText = displayText;
                    _neckSlotLabel.IsDirty = true;
                    break;
                }
                case EquipSlots.TORSO:
                {
                    _torsoSlotLabel.DisplayText = displayText;
                    _torsoSlotLabel.IsDirty = true;
                    break;
                }
                case EquipSlots.MAIN_HAND:
                {
                    _mainHandSlotLabel.DisplayText = displayText;
                    _mainHandSlotLabel.IsDirty = true;
                    break;
                }
                case EquipSlots.OFF_HAND:
                {
                    _offHandSlotLabel.DisplayText = displayText;
                    _offHandSlotLabel.IsDirty = true;
                    break;
                }
                case EquipSlots.LRING:
                {
                    _leftRingSlotLabel.DisplayText = displayText;
                    _leftRingSlotLabel.IsDirty = true;
                    break;
                }
                case EquipSlots.RRING:
                {
                    _rightRingSlotLabel.DisplayText = displayText;
                    _rightRingSlotLabel.IsDirty = true;
                    break;
                }
            }
        }

        private void GenerateLabels()
        {
            // HEAD
            var headTitleLabel = new Label("Head     :")
            {
                Position = new Point(1, 1)
            };
            Add(headTitleLabel);

            _headSlotLabel = new Label(20)
            {
                Position = new Point(11, 1)
            };
            Add(_headSlotLabel);

            // NECK
            var neckTitleLabel = new Label("Neck     :")
            {
                Position = new Point(1, 2)
            };
            Add(neckTitleLabel);

            _neckSlotLabel = new Label(20)
            {
                Position = new Point(11,2)
            };
            Add(_neckSlotLabel);

            // TORSO
            var torsoTitleLabel = new Label("Torso    :")
            {
                Position = new Point(1,3)
            };
            Add(torsoTitleLabel);

            _torsoSlotLabel = new Label(20)
            {
                Position = new Point(11, 3)
            };
            Add(_torsoSlotLabel);

            // MAIN HAND
            var mainHandTitleLabel = new Label("Main Hand:")
            {
                Position = new Point(1,4)
            };
            Add(mainHandTitleLabel);
            
            _mainHandSlotLabel = new Label(20)
            {
                Position = new Point(11, 4)
            };
            Add(_mainHandSlotLabel);

            // OFF HAND
            var offHandTitleLabel = new Label("Off Hand :")
            {
                Position = new Point(1,5)
            };
            Add(offHandTitleLabel);

            _offHandSlotLabel = new Label(20)
            {
                Position = new Point(11, 5)
            };

            Add(_offHandSlotLabel);
            
            // LEFT RING
            var leftRingTitleLabel = new Label("Lt Ring  :")
            {
                Position = new Point(1,6)
            };
            Add(leftRingTitleLabel);

            _leftRingSlotLabel = new Label(20)
            {
                Position = new Point(11,6)
            };
            Add(_leftRingSlotLabel);

            // RIGHT RING
            var rightRingTitleLabel = new Label("Rt Ring  :")
            {
                Position = new Point(1,7)
            };
            Add(rightRingTitleLabel);

            _rightRingSlotLabel = new Label(20)
            {
                Position = new Point(11,7)
            };
            Add(_rightRingSlotLabel);
        }
    }
}