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
    public abstract class Actor : MovableEntity
    {
        public int maxHealthPoints = 1;
        public int healthPoints = 1;
        public float invTimer = 0;
        public List<Ability> abilities = new List<Ability>();
        public double accuracy = 1;
        
        public override void Update(GameTime gameTime)
        {
            if (healthPoints == 0)
                Level.removeEntity(this, true);
            invTimer = Math.Max(invTimer - gameTime.ElapsedGameTime.Milliseconds, 0);

            // update cooldowns
            foreach (Ability a in abilities)
                a.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Texture2D redPixel = new Texture2D(graphics, 1, 1);
            Texture2D blackPixel = new Texture2D(graphics, 1, 1);
            Color[] redData = { Color.Red };
            Color[] blackData = { Color.Black };
            redPixel.SetData<Color>(redData);
            blackPixel.SetData<Color>(blackData);
            Rectangle healthContainer = new Rectangle((int)position.X, (int)position.Y - 10, width, 5);
            int borderWidth = 1;

            // Draw health segments inside of health container
            for (int i = 0; i < healthPoints; i++)
            {
                int segmentWidth = (int)Math.Ceiling((double)width / maxHealthPoints);
                int left = (int)position.X + (int)(i * segmentWidth);
                if (i + 1 == maxHealthPoints)
                    segmentWidth = width - (i * segmentWidth);
                Rectangle segment = new Rectangle(left, (int)position.Y - 10, segmentWidth, 5);
                spriteBatch.Draw(redPixel, segment, Color.White);
            }
            spriteBatch.Draw(blackPixel, new Rectangle(healthContainer.Left, healthContainer.Top, borderWidth, healthContainer.Height), Color.White); // Left
            spriteBatch.Draw(blackPixel, new Rectangle(healthContainer.Right, healthContainer.Top, borderWidth, healthContainer.Height), Color.White); // Right
            spriteBatch.Draw(blackPixel, new Rectangle(healthContainer.Left, healthContainer.Top, healthContainer.Width, borderWidth), Color.White); // Top
            spriteBatch.Draw(blackPixel, new Rectangle(healthContainer.Left, healthContainer.Bottom, healthContainer.Width, borderWidth), Color.White); // Bottom
        }
    }
}
