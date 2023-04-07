using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Graphics;

namespace Painter
{
    public class Cannon
    {
        Texture2D cannonBarrel, colorRed, colorBlue, colorGreen;
        Vector2 barrelPosition, barrelOrigin, colorOrigin;
        Color currentColor;
        float angle;

        public Cannon(ContentManager Content)
        {
            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
            colorRed = Content.Load<Texture2D>("spr_cannon_red");
            colorBlue = Content.Load<Texture2D>("spr_cannon_blue");
            colorGreen = Content.Load<Texture2D>("spr_cannon_green");

            barrelOrigin = new Vector2(cannonBarrel.Width, cannonBarrel.Height) / 2;
            colorOrigin = new Vector2(colorRed.Width / 2, colorRed.Height / 2);
            barrelPosition = new Vector2(72, 405);

            currentColor = Color.Blue;

        }

        public void Reset()
        {
            currentColor = Color.Blue;
            angle = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1f, SpriteEffects.None, 0);

            // determine the sprite based on the current color
            Texture2D currentSprite;
            if (currentColor == Color.Red)
                currentSprite = colorRed;
            else if (currentColor == Color.Green)
                currentSprite = colorGreen;
            else
                currentSprite = colorBlue;

            // draw that sprite
            _spriteBatch.Draw(currentSprite, barrelPosition, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.R))
            {
                Color = Color.Red;
            }
            else if (inputHelper.KeyPressed(Keys.G))
            {
                Color = Color.Green;
            }
            else if (inputHelper.KeyPressed(Keys.B))
            {
                Color = Color.Blue;
            }

            double opposite = inputHelper.MousePosition.Y - barrelPosition.Y;
            double adjacent = inputHelper.MousePosition.X - barrelPosition.X;
            Angle = (float)Math.Atan2(opposite, adjacent);
        }

        float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public Color Color
        {
            get { return currentColor; }
            private set
            {
                if (value != Color.Red && value != Color.Blue && value != Color.Green)
                    return;
                currentColor = value;

            }
        }

        public Vector2 Position
        {
            get { return barrelPosition; }
        }

        public Vector2 BallPosition
        {
            get
            {
                float opposite = (float)Math.Sin(angle) * cannonBarrel.Width * 0.75f;
                float adjacent = (float)Math.Cos(angle) * cannonBarrel.Width * 0.75f;
                return barrelPosition + new Vector2(adjacent, opposite);
            }
        }
    }
}
