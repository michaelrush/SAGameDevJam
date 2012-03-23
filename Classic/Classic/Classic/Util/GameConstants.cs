using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;


namespace Classic
{
    public static class GameConstants
    {
        public const int PlayableWidth = 960;
        public const int PlayableHeight = 560;
        public static Rectangle PlayableArea 
        {
            get 
            {
                return new Rectangle(0, 50, PlayableWidth, PlayableHeight);
            }
        }
        public const int BackBufferWidth = 960;
        public const int BackBufferHeight = 600;
        public static Random random = new Random();
        public const int TileSize = 100;

        // used for float comparions
        // TODO: is this necessary?
        public static float Epsilon = 0.001f;
    }
}
