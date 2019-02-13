using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstMonoGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Texture2D _dvd;
		private float _targetX = 128;
		private float _targetY;
		private Vector2 _scale;

		private int _speed = 200;
		private Vector2 _position = new Vector2(0, 0);
		private Vector2 _velocity;
		
		private Color _color;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_velocity = new Vector2(_speed, _speed);
			_dvd = this.Content.Load<Texture2D>("dvd");
			_scale = new Vector2(_targetX / _dvd.Width, _targetX / _dvd.Width);
			_targetY = _dvd.Height * _scale.Y;
			ChangeColor();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			_position += _velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;

			if (_position.X <= 0 && _velocity.X < 0)
			{
				ChangeColor();	
				_velocity.X = _speed;
			}
			if (_position.Y <= 0 && _velocity.Y < 0)
			{
				ChangeColor();	
				_velocity.Y = _speed;
			}

			if (_position.X + _targetX >= graphics.GraphicsDevice.Viewport.Width && _velocity.X > 0)
			{
				ChangeColor();	
				_velocity.X = -_speed;
			}
			if (_position.Y + _targetY >= graphics.GraphicsDevice.Viewport.Height && _velocity.Y > 0)
			{
				ChangeColor();	
				_velocity.Y = -_speed;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);
	
			spriteBatch.Begin();
			spriteBatch.Draw(_dvd, _position, color: _color, scale: _scale);
			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void ChangeColor()
		{
			var random = new Random();
			_color = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
		}
	}
}
