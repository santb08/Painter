using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Graphics;

namespace Painter
{
    public class GameWorld
    {
        Cannon cannon;
        Ball ball;
        Texture2D background;

        public GameWorld(ContentManager Content)
        {
            cannon = new Cannon(Content);
            background = Content.Load<Texture2D>("spr_background");
            ball = new Ball(Content);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            cannon.HandleInput(inputHelper);
            ball.HandleInput(inputHelper);
        }

        public void Update(GameTime gametime) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            ball.Draw(gameTime, spriteBatch);
            cannon.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public Cannon Cannon
        {
            get { return cannon; }
        }
    }
}
