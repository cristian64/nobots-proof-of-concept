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
using FarseerPhysics.Common;

namespace nobots_proof_of_concept
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        World world;
        Body floor;
        Vertices vertices;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlasmaExplosionParticleSystem plasmaExplosionParticleSystem;

        Character currentCharacter;
        MainCharacter mainCharacter;
        Mouse mouse;
        Ghost ghost;
        Box box;

        Texture2D backgroundAliveTexture;
        Texture2D grassAliveTexture;
        Texture2D backgroundDeadTexture;
        Texture2D grassDeadTexture;
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

            vertices = new Vertices();
            vertices.Add(0.02f * new Vector2(0, 0));
            vertices.Add(0.02f * new Vector2(0, 301));
            vertices.Add(0.02f * new Vector2(23, 313));
            vertices.Add(0.02f * new Vector2(148, 415));
            vertices.Add(0.02f * new Vector2(166, 427));
            vertices.Add(0.02f * new Vector2(213, 429));
            vertices.Add(0.02f * new Vector2(281, 434));
            vertices.Add(0.02f * new Vector2(325, 423));
            vertices.Add(0.02f * new Vector2(375, 428));
            vertices.Add(0.02f * new Vector2(432, 413));
            vertices.Add(0.02f * new Vector2(477, 402));
            vertices.Add(0.02f * new Vector2(525, 375));
            vertices.Add(0.02f * new Vector2(567, 334));
            vertices.Add(0.02f * new Vector2(601, 310));
            vertices.Add(0.02f * new Vector2(635, 282));
            vertices.Add(0.02f * new Vector2(666, 258));
            vertices.Add(0.02f * new Vector2(703, 225));
            vertices.Add(0.02f * new Vector2(773, 194));
            vertices.Add(0.02f * new Vector2(800, 182));
            vertices.Add(0.02f * new Vector2(800, 0));
            floor = BodyFactory.CreateLoopShape(world, vertices);
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
            Components.Add(new Mouse(this, world));
            Components.Add(new MainCharacter(this, world));
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

            backgroundAliveTexture = Content.Load<Texture2D>("alive_nograss");
            grassAliveTexture = Content.Load<Texture2D>("alive_grass");
            backgroundDeadTexture = Content.Load<Texture2D>("dead_nograss");
            grassDeadTexture = Content.Load<Texture2D>("dead_grass");
            mainTheme = Content.Load<Song>("gameconcept1");
            MediaPlayer.Play(mainTheme);
            MediaPlayer.Volume = 0.3f;
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


        Microsoft.Xna.Framework.Input.MouseState prevMouseState;

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

            if (Microsoft.Xna.Framework.Input.Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                Console.WriteLine(Microsoft.Xna.Framework.Input.Mouse.GetState().X + " " + Microsoft.Xna.Framework.Input.Mouse.GetState().Y);
            prevMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();


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

            DrawBackground(gameTime);

            base.Draw(gameTime);

            DrawGrass(gameTime);
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                DrawLine(50 * vertices[i], 50 * vertices[i + 1], Color.White, 1.0f);
            }
        }

        private void DrawBackground(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundAliveTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            spriteBatch.End();
        }

        private void DrawGrass(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(grassAliveTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            spriteBatch.End();
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

        public void DrawLine(Vector2 startPoint, Vector2 endPoint, Color color, float thickness)
        {
            float angle = (float)Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);
            float length = Vector2.Distance(startPoint, endPoint);
 
            Texture2D blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[]{Color.White});
            spriteBatch.Begin();
            spriteBatch.Draw(blank, startPoint, null, color, angle, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
