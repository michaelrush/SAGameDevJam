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
    public class Arrow : Projectile
    {
        public Arrow(Vector2 position, float speed, double angle)
            : base(position, speed, angle)
        {
            width = 20;
            height = 10;
            ContentManager content = new ContentManager(Game1.serviceProvider, "Content");
            texture = content.Load<Texture2D>("Textures/arrow");
        }

        public override bool collidesWith(Entity e)
        {
            return (e is Player || e is Fireball || e is Light);
        }

        public override void doCollision(Entity e)
        {
            Level.removeEntity(this, true);
        }
    }
}
