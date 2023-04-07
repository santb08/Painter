using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Painter
{
    public class GameWorld
    {
        Cannon cannon;
        Ball ball;
        Texture2D background;
        PaintCan can1, can2, can3;

        public GameWorld(ContentManager Content)
        {
            cannon = new Cannon(Content);
            background = Content.Load<Texture2D>("spr_background");
            ball = new Ball(Content);

            can1 = new PaintCan(Content, 480.0f, Color.Red);
            can2 = new PaintCan(Content, 610.0f, Color.Green);
            can3 = new PaintCan(Content, 740.0f, Color.Blue);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            cannon.HandleInput(inputHelper);
            ball.HandleInput(inputHelper);
        }

        public void Update(GameTime gametime) {
            ball.Update(gametime);
            can1.Update(gametime);
            can2.Update(gametime);
            can3.Update(gametime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            ball.Draw(gameTime, spriteBatch);
            cannon.Draw(gameTime, spriteBatch);
            can1.Draw(gameTime, spriteBatch);
            can2.Draw(gameTime, spriteBatch);
            can3.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public bool IsOutsideWorld(Vector2 position)
        {
            return position.X < 0 || position.X > Game1.ScreenSize.X || position.Y > Game1.ScreenSize.Y;
        }

        public Cannon Cannon
        {
            get { return cannon; }
        }

        public Ball Ball
        {
            get { return ball; }
        }
    }
}
