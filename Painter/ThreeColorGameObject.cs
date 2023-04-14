using System;
using System.Reflection.Metadata;

namespace Painter
{
	public class ThreeColorGameObject
	{
		protected Texture2D colorRed, colorBlue, colorGreen;
		Color color;
		protected Vector2 position, origin, velocity;
		protected float rotation;

		protected ThreeColorGameObject(
            ContentManager content,
            string redSprite,
            string greenSprite,
            string blueSprite
        ) {
            colorRed = content.Load<Texture2D>(redSprite);
            colorGreen = content.Load<Texture2D>(greenSprite);
            colorBlue = content.Load<Texture2D>(blueSprite);

            origin = new Vector2(colorRed.Width / 2, colorRed.Height / 2);

            position = Vector2.Zero;
            velocity = Vector2.Zero;
            rotation = 0;

            Reset();
        }

        public virtual void Reset()
        {
            Color = Color.Blue;
        }

        public virtual void HandleInput(InputHelper inputHelper) {}

        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
                rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public Vector2 Position
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

        public Color Color
        {
            get { return color; }
            protected set
            {
                if (value != Color.Red && value != Color.Blue && value != Color.Green)
                    return;
                color = value;

            }
        }
    }
}

