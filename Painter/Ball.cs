
using System;
using System.Reflection.Metadata;

namespace Painter
{
    public class Ball
    {
        Texture2D colorRed, colorGreen, colorBlue;
        Vector2 position, origin;
        Color color;

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
        }

        public void HandleInput(InputHelper inputHelper) { }

        public void Update(GameTime gameTime)
        {
            Color = Game1.GameWorld.Cannon.Color;
            position = Game1.GameWorld.Cannon.BallPosition;
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
    }
}
