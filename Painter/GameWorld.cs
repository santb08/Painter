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
        Texture2D background, gameOver, life;
        PaintCan can1, can2, can3;
        int lifes = 3;

        public GameWorld(ContentManager Content)
        {
            cannon = new Cannon(Content);
            background = Content.Load<Texture2D>("spr_background");
            gameOver = Content.Load<Texture2D>("spr_gameover");
            life = Content.Load<Texture2D>("spr_lives");

            ball = new Ball(Content);

            can1 = new PaintCan(Content, 480.0f, Color.Red);
            can2 = new PaintCan(Content, 610.0f, Color.Green);
            can3 = new PaintCan(Content, 740.0f, Color.Blue);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (!IsGameOver)
            {
                cannon.HandleInput(inputHelper);
                ball.HandleInput(inputHelper);

                return;
            }

            if (inputHelper.KeyPressed(Keys.Space)) Reset();
        }

        void Reset()
        {
            lifes = 3;
            cannon.Reset();
            can1.Reset();
            can2.Reset();
            can3.Reset();
            can1.ResetMinSpeed();
            can2.ResetMinSpeed();
            can3.ResetMinSpeed();
        }

        public void Update(GameTime gametime) {
            if (IsGameOver)
            {
                return;
            }

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

            for (int i = 0; i < lifes; i++)
            {
                spriteBatch.Draw(life, new Vector2(i * life.Width + 15, 20), Color.White);
            }

            if (IsGameOver)
            {
                spriteBatch.Draw(
                    gameOver,
                    new Vector2(
                        Game1.ScreenSize.X - gameOver.Width,
                        Game1.ScreenSize.Y - gameOver.Height
                    ) / 2,
                    Color.White
                );
            }

            spriteBatch.End();
        }

        public void LoseLife()
        {
            lifes--;
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

        bool IsGameOver
        {
            get { return lifes <= 0; }
        }
    }
}
