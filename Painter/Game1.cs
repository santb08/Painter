namespace Painter;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    InputHelper inputHelper;

    static GameWorld gameWorld;
    public static GameWorld GameWorld
    {
        get { return gameWorld; }
    }

    public Game1()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
        inputHelper = new InputHelper();
        Random = new Random();

        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(Content.Load<Song>("snd_music"));
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        gameWorld = new GameWorld(Content);

        ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update();
        gameWorld.HandleInput(inputHelper);
        gameWorld.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
    }

    public static Vector2 ScreenSize
    {
        get;
        private set;
    }

    public static Random Random
    {
        get;
        private set;
    }
}

