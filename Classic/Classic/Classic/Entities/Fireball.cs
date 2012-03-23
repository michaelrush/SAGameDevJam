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
    public class Fireball : SpellInstance
    {
        public Fireball(Vector2 position, float speed, float duration, double angle)
            : base(position, speed, duration, angle)
        {
            width = 50;
            height = 20;
            ContentManager content = new ContentManager(Game1.serviceProvider, "Content");
            texture = content.Load<Texture2D>("Textures/fireball");
        }

        public override bool collidesWith(Entity e)
        {
            return (e is Arrow || e is Spear || e is Enemy);
        }

        public override void doCollision(Entity e)
        {
            if (e is Enemy)
                Level.removeEntity(this, true);
        }
    }
}
