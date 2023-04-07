﻿
using System;
using System.Reflection.Metadata;

namespace Painter
{
    public class Ball
    {
        Texture2D colorRed, colorGreen, colorBlue;
        Vector2 position, origin, velocity;
        Color color;
        bool shooting;

        public Ball(ContentManager Content)
        {
            colorRed = Content.Load<Texture2D>("spr_ball_red");
            colorGreen = Content.Load<Texture2D>("spr_ball_green");
            colorBlue = Content.Load<Texture2D>("spr_ball_blue");

            origin = new Vector2(colorRed.Width / 2, colorRed.Height / 2);

            Reset();
        }

        public void Reset()
        {
            position = new Vector2(65, 390);
            Color = Color.Blue;
            velocity = Vector2.Zero;
            shooting = false;
        }

        public void HandleInput(InputHelper inputHelper) {
            if (inputHelper.MouseLeftButtonPressed() && !shooting)
            {
                shooting = true;
                velocity = (inputHelper.MousePosition - Game1.GameWorld.Cannon.Position) * 1.2f;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (shooting)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity.Y += 400.0f * dt;
                position += velocity * dt;
            } else
            {
                Color = Game1.GameWorld.Cannon.Color;
                position = Game1.GameWorld.Cannon.BallPosition;
            }

            if (Game1.GameWorld.IsOutsideWorld(position))
            {
                Reset();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // determine the sprite based on the current color
            Texture2D currentSprite;
            if (Color == Color.Red)
                currentSprite = colorRed;
            else if (Color == Color.Green)
                currentSprite = colorGreen;
            else
                currentSprite = colorBlue;

            // draw that sprite
            spriteBatch.Draw(currentSprite, position, null, Color.White,
                0f, origin, 1.0f, SpriteEffects.None, 0);

        }

        public Color Color
        {
            get { return color; }
            private set
            {
                if (value != Color.Red && value != Color.Blue && value != Color.Green)
                    return;
                color = value;

            }
        }

        Vector2 Position
        {
            get { return position; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                Rectangle spriteBounds = colorRed.Bounds;
                spriteBounds.Offset(position - origin);
                return spriteBounds;
            }
        }
    }
}
