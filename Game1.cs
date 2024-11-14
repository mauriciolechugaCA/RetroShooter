using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Scenes;

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
        private SpriteFont _font;

        private GameScene _currentScene = GameScene.Start;

        private StartScene _startScene;
        private PlayScene _playScene;
        private AboutScene _aboutScene;
        private HelpScene _helpScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Set the window size
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 1200;
            _graphics.ApplyChanges();

            // Initialize the SceneManager
            SceneManager.Initialize(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("fonts/gameFont");

            _startScene = new StartScene(_spriteBatch, _font, this);
            //_playScene = new PlayScene(_spriteBatch, _font);
            //_helpScene = new HelpScene(_spriteBatch, _font);
            _aboutScene = new AboutScene(_spriteBatch, _font, this);

            // Sets the initial scene
            SceneManager.ChangeScene(new StartScene(_spriteBatch, _font, this));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update the current scene using the SceneManager
            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SceneManager.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
