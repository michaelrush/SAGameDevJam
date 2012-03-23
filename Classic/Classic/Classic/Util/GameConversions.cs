using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;


namespace Classic
{
    public static class GameConversions
    {
        public static Vector2 toTilePosition(Vector2 pos)
        {
            return new Vector2((int)Math.Round(pos.X / GameConstants.TileSize) * GameConstants.TileSize, (int)Math.Round(pos.Y / GameConstants.TileSize) * GameConstants.TileSize);
        }
    }
}
