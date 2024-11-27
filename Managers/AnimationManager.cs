using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RetroShooter.Managers
{
    public class AnimationManager
    {
        private Dictionary<string, Animation> _animations;
        private Animation _currentAnimation;

        public AnimationManager()
        {
            _animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(string name, Animation animation)
        {
            _animations[name] = animation;
        }

        public void Play(string name)
        {
            if (_animations.ContainsKey(name))
            {
                _currentAnimation = _animations[name];
            }
        }

        public void Update(GameTime gameTime)
        {
            _currentAnimation?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 1.0f)
        {
            if (_currentAnimation != null)
            {
                spriteBatch.Draw(
                    _currentAnimation.Texture,
                    position,
                    _currentAnimation.CurrentFrameRectangle,
                    Color.White,
                    0f,
                    new Vector2(_currentAnimation.FrameWidth / 2, _currentAnimation.FrameHeight / 2),
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}