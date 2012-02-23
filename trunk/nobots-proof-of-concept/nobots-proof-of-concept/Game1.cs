using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using nobots_proof_of_concept.ParticleSystem;

namespace nobots_proof_of_concept
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        World world;
        Body floor;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlasmaExplosionParticleSystem plasmaExplosionParticleSystem;

        Character currentCharacter;
        MainCharacter mainCharacter;
        Mouse mouse;
        Ghost ghost;
        Box box;

        Texture2D backgroundTexture;
        Song mainTheme;

        public static int screenWidth;
        public static int screenHeight;

        public List<Element> elementList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            world = new World(new Vector2(0, 9.81f));
            floor = BodyFactory.CreateRectangle(world, 0.02f * 800, 0.02f * 1, 1);
            floor.Position = new Vector2(0.02f * 400, 0.02f * 481);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

            Components.Add(new Box(this, world));
            Components.Add(new Mouse(this));
            Components.Add(new MainCharacter(this));
            Components.Add(plasmaExplosionParticleSystem = new PlasmaExplosionParticleSystem(this, Content));

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

            backgroundTexture = Content.Load<Texture2D>("AliveBackground");
            mainTheme = Content.Load<Song>("gameconcept1");
            MediaPlayer.Play(mainTheme);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
                plasmaExplosionParticleSystem.AddParticle(new Vector3(100, 50, 0), Vector3.Zero);

            // TODO: Add your update logic here
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawBackground();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
        }

        private void ProcessKeyboard()
        {
        }

        private void DrawElements()
        {
        }

        public void AddElement(Element element)
        {
        }
    }
}
