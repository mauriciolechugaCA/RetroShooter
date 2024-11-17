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
        private SpriteFont _hudFont;
        private SpriteFont _creditsTitleFont;
        private SpriteFont _menuTitle;
        private SpriteFont _menuItems;

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
            _font = Content.Load<SpriteFont>("fonts/gameFont"); // Used in credits
            _hudFont = Content.Load<SpriteFont>("fonts/hudFont"); // Used in the game HUD
            _creditsTitleFont = Content.Load<SpriteFont>("fonts/creditsTitle"); // Used in the credits title
            _menuTitle = Content.Load<SpriteFont>("fonts/menuTitle"); // Used in the menu title
            _menuItems = Content.Load<SpriteFont>("fonts/menuItems"); // Used in the menu items

            _startScene = new StartScene(_spriteBatch, _font, _menuItems, _menuTitle,  this);
            //_playScene = new PlayScene(_spriteBatch, _font);
            _helpScene = new HelpScene(_spriteBatch, _font);
            _aboutScene = new AboutScene(_spriteBatch, _creditsTitleFont, _menuItems, _menuTitle, this);

            // Sets the initial scene
            SceneManager.ChangeScene(new StartScene(_spriteBatch, _font, _menuItems, _menuTitle, this));

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
