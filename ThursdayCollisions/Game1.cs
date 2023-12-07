using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThursdayCollisions
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _hero, _bullet;
        private float _heroX, _heroY, _heroScale, _bulletX, _bulletY, _bulletScale, _heroSpeed;
        private Color _heroColor;
        private bool _bulletIsAlive;

        private int _mouseX, _mouseY;
        private SpriteFont _gameFont;

        private int _damageTimer;

        private Rectangle _heroBounds, _bulletBounds;

        private Texture2D _box;

        private Vector2 _controllerLeftStick;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _heroX = 10;
            _heroY = 125;
            _bulletX = 600;
            _bulletY = 200;
            _bulletScale = 0.25f;
            _heroScale = 0.5f;
            _heroColor = Color.White;
            _bulletIsAlive = true;
            _damageTimer = 0;
            _heroSpeed = 10;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _hero = Content.Load<Texture2D>("hero");
            _bullet = Content.Load<Texture2D>("bullet");

            _gameFont = Content.Load<SpriteFont>("GameFont");

            //_box.SetData(new Color[] { Color.White });
            //_box = new Texture2D(GraphicsDevice, 1, 1);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _mouseX = Mouse.GetState().X;
                _mouseY = Mouse.GetState().Y;
            }



            _controllerLeftStick = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;



            _heroBounds = new Rectangle((int)_heroX, (int)_heroY, (int)((_hero.Width-75) * _heroScale), (int)(_hero.Height * _heroScale));
            _bulletBounds = new Rectangle((int)_bulletX, (int)_bulletY, (int)(_bullet.Width * _bulletScale), (int)(_bullet.Height * _bulletScale));


            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                _heroScale *= 1.01f;

            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.B))
                _heroScale *= 0.99f;


            if (_heroBounds.Intersects(_bulletBounds))
            {
                _heroColor = Color.Red;
                _bulletIsAlive = false;
            }

            if(_heroColor == Color.Red)
            {
                _damageTimer++;
            }

            if(_damageTimer >= 90)
            {
                _damageTimer = 0;
                _heroColor = Color.White;
            }

            _heroX += _controllerLeftStick.X * _heroSpeed;
            _heroY -= _controllerLeftStick.Y * _heroSpeed;

            //_bulletX--;
            _bulletX = _mouseX;
            _bulletY = _mouseY;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_hero,
                new Vector2(_heroX, _heroY),
                null,
                _heroColor,
                0,
                new Vector2(0, 0),
                _heroScale,
                SpriteEffects.None,
                0
                );
            //_spriteBatch.Draw(_box, _heroBounds, Color.White * 0.5f);

            if (_bulletIsAlive)
            {
                _spriteBatch.Draw(_bullet,
                    new Vector2(_bulletX, _bulletY),
                    null,
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    _bulletScale,
                    SpriteEffects.FlipHorizontally,
                    0
                    );

                //_spriteBatch.Draw(_box, _bulletBounds, Color.Yellow * 0.5f);
            }

            _spriteBatch.DrawString(_gameFont, "Mouse X,Y: " + _mouseX + ", " + _mouseY, new Vector2(10, 350), Color.White);
            _spriteBatch.DrawString(_gameFont, "Controller X,Y: " + _controllerLeftStick.X + ", " + _controllerLeftStick.Y, new Vector2(10, 400), Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
