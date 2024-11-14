using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Scenes;

namespace RetroShooter.Scenes
{
    public class StartScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private SpriteFont _menuItemsfont;
        private SpriteFont _menuTitlefont;
        private List<string> _menuItems;
        private int _selectedMenuItem;
        private Game _game;
        private KeyboardState _previousKeyboardState;

        public StartScene(SpriteBatch spriteBatch, SpriteFont font, SpriteFont menuItemsfont, SpriteFont menuTitlefont, Game game)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _menuItemsfont = menuItemsfont;
            _menuTitlefont = menuTitlefont;
            _menuItems = new List<string> { "Play", "Help", "About", "Exit" };
            _selectedMenuItem = 0;
            _game = game;
            _previousKeyboardState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) && !_previousKeyboardState.IsKeyDown(Keys.Up))
            {
                _selectedMenuItem--;
                if (_selectedMenuItem < 0)
                {
                    _selectedMenuItem = _menuItems.Count - 1;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Down) && !_previousKeyboardState.IsKeyDown(Keys.Down))
            {
                _selectedMenuItem++;
                if (_selectedMenuItem >= _menuItems.Count)
                {
                    _selectedMenuItem = 0;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                switch (_selectedMenuItem)
                {
                    case 0:
                        // TODO: Play
                        break;
                    case 1:
                        // TODO: Help
                        break;
                    case 2:
                        // Switch to AboutScene
                        SceneManager.ChangeScene(new AboutScene(_spriteBatch, _font, _menuItemsfont, _menuTitlefont, _game));
                        break;
                    case 3:
                        // Exit
                        _game.Exit();
                        break;
                }
            }

            _previousKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw the title
            string title = "Retro Shooter";
            var titleSize = _menuTitlefont.MeasureString(title);
            var titlePosition = new Vector2((_game.GraphicsDevice.Viewport.Width - titleSize.X) / 2, 75);

            _spriteBatch.DrawString(_menuTitlefont, title, titlePosition, Color.White);

            // Draw the menu items
            for (int i = 0; i < _menuItems.Count; i++)
            {
                var text = _menuItems[i];
                var textSize = _menuItemsfont.MeasureString(text);
                var textPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - textSize.X) / 2, 200 + i * 100);
                var color = (i == _selectedMenuItem) ? Color.Yellow : Color.White;
                _spriteBatch.DrawString(_menuItemsfont, text, textPosition, color);
            }

            _spriteBatch.End();
        }
    }
}
