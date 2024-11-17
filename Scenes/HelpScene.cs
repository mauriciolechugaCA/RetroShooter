using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroShooter.Scenes
{
    internal class HelpScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public HelpScene(SpriteBatch spriteBatch, SpriteFont font)
        {
            _spriteBatch = spriteBatch;
            _font = font;
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add update logic
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();
            string helpText = "Help info";
            var textSize = _font.MeasureString(helpText);
            var textPosition = new Vector2((_spriteBatch.GraphicsDevice.Viewport.Width - textSize.X) / 2, 200);
            _spriteBatch.DrawString(_font, helpText, textPosition, Color.White);

            _spriteBatch.End();
        }
    }
}
