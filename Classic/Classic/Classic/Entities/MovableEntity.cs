using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Classic
{
    public abstract class MovableEntity : Entity
    {
        public Vector2 prevPosition;
        public Vector2 velocity = Vector2.Zero;
        public float speed = 0;

        // The rectangle is calculated from the tile's current position. Useful for drawing
        public Rectangle prevBoundingRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, width, height); }
        }

        public abstract void Update(GameTime gameTime);
    }
}
