using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using RetroShooter.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroShooter.Scenes
{
    public class GameOverScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _menuTitlefont;
        private SpriteFont _menuItemsfont;
        private Game1 _game;
        private string[] _menuItems = { "Try Again", "Main Menu", "Exit" };
        private int _selectedIndex = 0;
        private InputManager _inputManager;
        private BackgroundManager _backgroundManager;

        public GameOverScene(SpriteBatch spriteBatch, SpriteFont menuTitlefont, SpriteFont menuItemsfont, Game1 game)
        {
            _spriteBatch = spriteBatch;
            _menuTitlefont = menuTitlefont;
            _menuItemsfont = menuItemsfont;
            _game = game;
            _inputManager = new InputManager();
            _backgroundManager = new BackgroundManager(_game.backgroundTexture, _game.floatingMeteorsTextures, 1000, 1500, _spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            if (_inputManager.IsKeyPressed(Keys.Up))
            {
                _selectedIndex = (_selectedIndex - 1 + _menuItems.Length) % _menuItems.Length;
            }
            if (_inputManager.IsKeyPressed(Keys.Down))
            {
                _selectedIndex = (_selectedIndex + 1) % _menuItems.Length;
            }
            if (_inputManager.IsKeyPressed(Keys.Enter))
            {
                if (_selectedIndex == 0)
                {
                    _game.ChangeScene(GameScene.Play);
                }
                if (_selectedIndex == 1)
                {
                    _game.ChangeScene(GameScene.Start);
                }
                else if (_selectedIndex == 2)
                {
                    _game.Exit();
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _backgroundManager.Draw(spriteBatch);

            string gameOverText = "Game Over";
            Vector2 gameOverSize = _menuTitlefont.MeasureString(gameOverText);
            Vector2 gameOverPosition = new Vector2(
                (_spriteBatch.GraphicsDevice.Viewport.Width - _menuTitlefont.MeasureString(gameOverText).X) / 2,
                175
            );
            var shadowOffset = new Vector2(4, 4);

            spriteBatch.DrawString(_menuTitlefont, gameOverText, gameOverPosition + shadowOffset, Color.Black);
            spriteBatch.DrawString(_menuTitlefont, gameOverText, gameOverPosition, Color.LightGray);

            for (int i = 0; i < _menuItems.Length; i++)
            {
                Color color = (i == _selectedIndex) ? Color.IndianRed : Color.LightGray;
                Vector2 textSize = _menuItemsfont.MeasureString(_menuItems[i]);
                Vector2 position = new Vector2(
                    (_spriteBatch.GraphicsDevice.Viewport.Width - _menuItemsfont.MeasureString(_menuItems[i]).X) / 2,
                    275 + i * 100
                );

                spriteBatch.DrawString(_menuItemsfont, _menuItems[i], position + shadowOffset, Color.Black);
                spriteBatch.DrawString(_menuItemsfont, _menuItems[i], position, color);
            }

            spriteBatch.End();
        }
    }
}