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
    public class LightSpell : Spell
    {
        public LightSpell()
        {
            coolDownDuration = 3000;
            spellDuration = 1600;
            speed = 0f;
        }

        protected override void doAction(Vector2 position, double accuracy)
        {
            double angle = GameConstants.random.NextDouble() * (1-accuracy) * Math.PI / 2;
            // Center the light
            Light light = new Light(position, speed, spellDuration, angle);
            light.position.X -= light.width / 2;
            light.position.Y -= light.height / 2;
            Level.addEntity(light);
        }
    }
}
