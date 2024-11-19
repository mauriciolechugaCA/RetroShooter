﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Scenes;
using RetroShooter.Entities;

namespace RetroShooter
{
    public enum GameScene
    {
        Start,
        Play,
        Help,
        About,
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Fonts
        private SpriteFont _font;
        private SpriteFont _hudFont;
        private SpriteFont _creditsTitleFont;
        private SpriteFont _menuTitle;
        private SpriteFont _menuItems;

        // Game objects
        public Player player; // Make player public to access it in AboutScene
        private Texture2D playerTexture;

        // Scene management
        private GameScene _currentScene;
        private StartScene _startScene;
        private PlayScene _playScene;
        private HelpScene _helpScene;
        private AboutScene _aboutScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set the window size
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            // Initialize the SceneManager
            SceneManager.Initialize(this);
            _currentScene = GameScene.Start;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load fonts
            _font = Content.Load<SpriteFont>("fonts/gameFont");
            _hudFont = Content.Load<SpriteFont>("fonts/hudFont");
            _creditsTitleFont = Content.Load<SpriteFont>("fonts/creditsTitle");
            _menuTitle = Content.Load<SpriteFont>("fonts/menuTitle");
            _menuItems = Content.Load<SpriteFont>("fonts/menuItems");

            // Load game textures
            playerTexture = Content.Load<Texture2D>("images/player/playerShip");

            // Initialize player
            player = new Player(
                new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height - 100),
                100,  // Initial health
                playerTexture,
                0.5f  // Scale
            );

            // Initialize scenes
            _startScene = new StartScene(_spriteBatch, _font, _menuItems, _menuTitle, this, player);
            _playScene = new PlayScene(_spriteBatch, _hudFont, player);
            _helpScene = new HelpScene(_spriteBatch, _font);
            _aboutScene = new AboutScene(_spriteBatch, _creditsTitleFont, _menuItems, _menuTitle, this);

            // Set initial scene
            SceneManager.ChangeScene(_startScene);
        }

        protected override void Update(GameTime gameTime)
        {
            // Exit game if escape is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Update current scene
            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw current scene
            SceneManager.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        // Helper method to change scenes
        public void ChangeScene(GameScene newScene)
        {
            _currentScene = newScene;
            switch (newScene)
            {
                case GameScene.Start:
                    SceneManager.ChangeScene(_startScene);
                    break;
                case GameScene.Play:
                    SceneManager.ChangeScene(_playScene);
                    break;
                case GameScene.Help:
                    SceneManager.ChangeScene(_helpScene);
                    break;
                case GameScene.About:
                    SceneManager.ChangeScene(_aboutScene);
                    break;
            }
        }

        // Method to get current scene
        public GameScene GetCurrentScene()
        {
            return _currentScene;
        }
    }
}

