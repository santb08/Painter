
using System;
using System.Reflection.Metadata;

namespace Painter
{
    public class Ball : ThreeColorGameObject
    {
        Color color;
        bool shooting;
        SoundEffect soundEffect;

        public Ball(ContentManager Content)
            : base(Content, "spr_ball_red", "spr_ball_green", "spr_ball_blue") {
            soundEffect = Content.Load<SoundEffect>("snd_shoot_paint");
        }

        public override void Reset()
        {
            base.Reset();
            position = new Vector2(65, 390);
            velocity = Vector2.Zero;
            shooting = false;
        }

        public override void HandleInput(InputHelper inputHelper) {
            if (inputHelper.MouseLeftButtonPressed() && !shooting)
            {
                shooting = true;
                velocity = (inputHelper.MousePosition - Game1.GameWorld.Cannon.Position) * 1.2f;
                soundEffect.Play();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (shooting)
            {
                float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
                velocity.Y += 400.0f * dt;

                base.Update(gameTime);
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
    }
}
