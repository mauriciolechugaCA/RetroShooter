using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RetroShooter.Managers
{
    public class Animation
    {
        public Texture2D Texture { get; }
        public int FrameCount { get; }
        public float FrameSpeed { get; }
        public bool IsLooping { get; }

        private int _currentFrame;
        private float _timer;

        public int FrameWidth => Texture.Width / 9;
        public int FrameHeight => Texture.Height / 7;

        public Animation(Texture2D texture, int frameCount, float frameSpeed, bool isLooping)
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameSpeed = frameSpeed;
            IsLooping = isLooping;
            _currentFrame = 0;
            _timer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > FrameSpeed)
            {
                _timer = 0f;
                _currentFrame++;

                if (_currentFrame >= FrameCount)
                {
                    if (IsLooping)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        _currentFrame = FrameCount - 1;
                    }
                }
            }
        }

        public Rectangle CurrentFrameRectangle => new Rectangle(FrameWidth * _currentFrame, 0, FrameWidth, FrameHeight);

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int frameWidth = Texture.Width / 9;
            int frameHeight = Texture.Height / 7;
            Rectangle sourceRectangle = new Rectangle(frameWidth * _currentFrame, 0, frameWidth, frameHeight);
            spriteBatch.Draw(Texture, position, sourceRectangle, Color.White);
        }
    }
}