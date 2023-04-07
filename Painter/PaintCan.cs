using System;
namespace Painter
{
	public class PaintCan
    {
        Texture2D colorRed, colorGreen, colorBlue;
        Vector2 position, origin, velocity;
        Color color, targetColor;
        bool shooting;
        float minSpeed;

        public PaintCan(ContentManager Content, float positionOffset, Color target)
        {
            colorRed = Content.Load<Texture2D>("spr_can_red");
            colorGreen = Content.Load<Texture2D>("spr_can_green");
            colorBlue = Content.Load<Texture2D>("spr_can_blue");

            origin = new Vector2(colorRed.Width / 2, colorRed.Height / 2);
            targetColor = target;
            position = new Vector2(positionOffset, -origin.Y);
            minSpeed = 30;

            Reset();
        }

        public void Reset()
        {
            position.Y = -origin.Y;
            Color = Color.Blue;
            velocity = Vector2.Zero;
            shooting = false;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            minSpeed += 0.01f * dt;

            if (velocity != Vector2.Zero)
            {
                position += velocity * dt;

                if (Game1.GameWorld.IsOutsideWorld(position - origin))
                {
                    Reset();
                }

                if (BoundingBox.Intersects(Game1.GameWorld.Ball.BoundingBox))
                {
                    Color = Game1.GameWorld.Ball.Color;
                    Game1.GameWorld.Ball.Reset();
                }
            } else
            {
                velocity = CalculateRandomVelocity();
                Color = (Color)CalculateRandomColor();
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

        Vector2 CalculateRandomVelocity()
        {
            return new Vector2(0.0f, (float)Game1.Random.NextDouble() * 30 + minSpeed);
        }

        Color? CalculateRandomColor()
        {
            return Game1.Random.Next(3) switch
            {
                0 => Color.Red,
                1 => Color.Green,
                2 => Color.Blue,
                _ => null
            };
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

