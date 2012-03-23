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
    public abstract class Entity
    {
        public Texture2D texture;
        public Vector2 position;
        public int width;
        public int height;
        public CollisionType collisionType;
        public bool colliding;

        // The rectangle is calculated from the tile's current position. Useful for drawing
        public Rectangle boundingRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, width, height); }
        }

        public abstract bool collidesWith(Entity other);
        public abstract void doCollision(Entity other);
        public abstract void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics);
    }
}
