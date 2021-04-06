using System;
using System.Collections.Generic;
using System.Drawing;
using GoRogue.Random;
using Troschuetz.Random;
using Rectangle = GoRogue.Rectangle;

namespace DiabloRL.Common.Cartography
{
    public class Leaf
    {
        private const int MinLeafSize = 6;

        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Leaf LeftChild;
        public Leaf RightChild;
        public Rectangle Room;

        public Leaf(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Split()
        {
            var random = SingletonRandom.DefaultRNG;
            
            // Abort if this leaf has already been split
            if (LeftChild != null || RightChild != null) return false;

            // first randomly choose whether leaf gets split horizontally or not
            var splitH = random.Next(100) > 50;
            // if width is > 25% larger than height then split vertically
            // if height is > 25% larger than width then split horizontally
            if (Width > Height && Width / Height >= 1.25f)
                splitH = false;
            else if (Height > Width && Height / Width >= 1.25f)
                splitH = true;

            var max = splitH ? Height : Width - MinLeafSize;
            
            // abort if leaf size is too small for a room
            if (max <= MinLeafSize) return false;

            var splitSize = random.Next(MinLeafSize, max);
            
            // create the children based on how the leaf was split
            if (splitH)
            {
                LeftChild = new Leaf(X, Y, Width, splitSize);
                RightChild = new Leaf(X, Y + splitSize, Width, Height - splitSize);
            }
            else
            {
                LeftChild = new Leaf(X, Y, splitSize, Height);
                RightChild = new Leaf(X + splitSize, Y, Width - splitSize, Height);
            }

            // split was successful
            return true;
        }

        public void CreateRooms()
        {
            var random = SingletonRandom.DefaultRNG;
            var rooms = new List<Rectangle>();
            
            // create the rooms for this leaf if it's already been split
            if (LeftChild != null || RightChild != null)
            {
                LeftChild?.CreateRooms();
                RightChild?.CreateRooms();
            }
            else
            {
                // room can be between 3x3 tiles to the size of the leaf - 2
                var roomSize = new Point(random.Next(3, Width - 2), random.Next(3, Height - 2));
                // place room in the leaf but don't allow it to butt up against side of leaf
                var roomPos = new Point(random.Next(1, Width - roomSize.X - 1),
                    random.Next(1, Height - roomSize.Y - 1));
                Room = new Rectangle(roomPos.X, roomPos.Y, roomSize.X, roomSize.Y);
            }
        }
    }
}