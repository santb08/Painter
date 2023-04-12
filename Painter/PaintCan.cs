using System;
namespace Painter
{
	public class PaintCan : ThreeColorGameObject
    {
        Color color, targetColor;
        bool shooting;
        float minSpeed;

        public PaintCan(ContentManager Content, float positionOffset, Color target)
            : base(Content, "spr_can_red", "spr_can_green", "spr_can_blue")
        {
            targetColor = target;
            position = new Vector2(positionOffset, -origin.Y);
            minSpeed = 30;
        }

        public override void Reset()
        {
            base.Reset();
            position.Y = -origin.Y;
            velocity = Vector2.Zero;
            shooting = false;
        }

        public void ResetMinSpeed()
        {
            minSpeed = 30;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            minSpeed += 0.01f * dt;

            if (velocity != Vector2.Zero)
            {
                base.Update(gameTime);

                if (Game1.GameWorld.IsOutsideWorld(position - origin))
                {
                    if (Color != targetColor) Game1.GameWorld.LoseLife();
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
    }
}

