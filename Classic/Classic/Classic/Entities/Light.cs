using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Classic
{
    public class Light : SpellInstance
    {
        public float maxDuration;

        public Light(Vector2 position,  float speed, float maxDuration, double angle)
            : base(position, speed, maxDuration, angle)
        {
            width = 400;
            height = 400;
            this.maxDuration = maxDuration;
            ContentManager content = new ContentManager(Game1.serviceProvider, "Content");
            texture = content.Load<Texture2D>("Textures/light");
        }

        public override bool collidesWith(Entity e)
        {
            return (e is Arrow || e is Spear || e is Enemy);
        }

        public override void doCollision(Entity e) {}
        
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(texture, boundingRectangle, Color.White * ((float) Math.Pow((double) duration / maxDuration, 0.2)));
        }
    }
}
