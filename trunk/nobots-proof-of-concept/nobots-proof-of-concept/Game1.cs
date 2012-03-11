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
        public Body floor;
        Vertices vertices;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlasmaExplosionParticleSystem plasmaExplosionParticleSystem;
        ExplosionSmokeParticleSystem explosionSmokeParticleSystem;

        public Character currentCharacter;
        public MainCharacter mainCharacter;
        public Mouse mouse;
        public Ghost ghost;
        public Box box;

        Texture2D buttonTexture;
        Texture2D controlsTexture;
        Texture2D backgroundAliveTexture;
        Texture2D grassAliveTexture;
        Texture2D backgroundDeadTexture;
        Texture2D grassDeadTexture;
        float alphaAlive = 1.5f;
        float alphaDead = 0.0f;
        int buttonWidth;
        Song mainTheme;

        public SoundEffect Bomb;
        public SoundEffect Change;

        public static int screenWidth;
        public static int screenHeight;

        public List<Element> elementList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Window.Title = "Nobots - Proof of Concept";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferMultiSampling = true;
            world = new World(new Vector2(0, 9.81f));

            vertices = new Vertices();
            vertices.Add(0.02f * new Vector2(0, 0));
            vertices.Add(0.02f * new Vector2(0, 311));
            vertices.Add(0.02f * new Vector2(23, 323));
            vertices.Add(0.02f * new Vector2(148, 425));
            vertices.Add(0.02f * new Vector2(166, 432));
            vertices.Add(0.02f * new Vector2(213, 434));
            vertices.Add(0.02f * new Vector2(281, 429));
            vertices.Add(0.02f * new Vector2(325, 428));
            vertices.Add(0.02f * new Vector2(375, 428));
            vertices.Add(0.02f * new Vector2(432, 413));
            vertices.Add(0.02f * new Vector2(477, 402));
            vertices.Add(0.02f * new Vector2(525, 385));
            vertices.Add(0.02f * new Vector2(567, 344));
            vertices.Add(0.02f * new Vector2(601, 320));
            vertices.Add(0.02f * new Vector2(635, 292));
            vertices.Add(0.02f * new Vector2(666, 268));
            vertices.Add(0.02f * new Vector2(703, 235));
            vertices.Add(0.02f * new Vector2(773, 204));
            vertices.Add(0.02f * new Vector2(800, 192));
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

            Components.Add(box = new Box(this, world));
            Components.Add(mouse = new Mouse(this, world));
            Components.Add(mainCharacter = new MainCharacter(this, world));
            Components.Add(ghost = new Ghost(this));
            Components.Add(plasmaExplosionParticleSystem = new PlasmaExplosionParticleSystem(this, Content));
            Components.Add(explosionSmokeParticleSystem = new ExplosionSmokeParticleSystem(this, Content));

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

            buttonTexture = Content.Load<Texture2D>("button");
            controlsTexture = Content.Load<Texture2D>("controls");
            backgroundAliveTexture = Content.Load<Texture2D>("alive_nograss");
            grassAliveTexture = Content.Load<Texture2D>("alive_grass");
            backgroundDeadTexture = Content.Load<Texture2D>("dead_nograss");
            grassDeadTexture = Content.Load<Texture2D>("dead_grass");
            mainTheme = Content.Load<Song>("gameconcept1");
            MediaPlayer.Play(mainTheme);
            MediaPlayer.Volume = 1.0f;
            MediaPlayer.IsRepeating = true;
            Bomb = Content.Load<SoundEffect>("bomb");
            Change = Content.Load<SoundEffect>("change");

            buttonWidth = buttonTexture.Width;
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
                plasmaExplosionParticleSystem.AddParticle(new Vector3(100, 50, 0), Vector3.Zero);

            if (Microsoft.Xna.Framework.Input.Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                Console.WriteLine(Microsoft.Xna.Framework.Input.Mouse.GetState().X + " " + Microsoft.Xna.Framework.Input.Mouse.GetState().Y);
            prevMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();


            // TODO: Add your update logic here
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (!ghost.isHaunted)
            {
                alphaAlive = Math.Min(1.5f, alphaAlive + (float)gameTime.ElapsedGameTime.TotalSeconds);
                alphaDead = Math.Max(0.0f, alphaDead - (float)gameTime.ElapsedGameTime.TotalSeconds);
                Console.WriteLine("seconds: " + (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                alphaAlive = Math.Max(0.0f, alphaAlive - (float)gameTime.ElapsedGameTime.TotalSeconds);
                alphaDead = Math.Min(1.5f, alphaDead + (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            checkButtonPressed();
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
            drawButton(gameTime);
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                //DrawLine(50 * vertices[i], 50 * vertices[i + 1], Color.White, 1.0f);
            }
        }

        private void DrawBackground(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundAliveTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * alphaAlive);
            spriteBatch.Draw(backgroundDeadTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * alphaDead);
            spriteBatch.Draw(controlsTexture, new Vector2(screenWidth - controlsTexture.Width, 0), Color.White);
            spriteBatch.End();
        }

        private void DrawGrass(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(grassAliveTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * alphaAlive);
            spriteBatch.Draw(grassDeadTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * alphaDead);
            spriteBatch.End();
        }

        private void drawButton(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(buttonTexture, new Rectangle(0, screenHeight/2 + 20, buttonWidth,
                buttonTexture.Height), Color.White);
            spriteBatch.End();
        }

        private void checkButtonPressed()
        {
            if (buttonTexture.Width >= mouse.body.Position.X * 50 - mouse.texture.Width / 4 && buttonWidth != buttonTexture.Width/2)
            {
                Random random = new Random();
                buttonWidth = buttonTexture.Width / 2;
                for (int i = 0; i < 80; i++)
                {
                    explosionSmokeParticleSystem.AddParticle(new Vector3(box.body.Position.X * 50 - 20,
                           box.body.Position.Y * 50 - 20, 0), new Vector3(random.Next() % 300, random.Next() % 300, 0));
                    explosionSmokeParticleSystem.AddParticle(new Vector3(box.body.Position.X * 50 - 20 - random.Next() % 70,
                           box.body.Position.Y * 50 - 20 - random.Next() % 70, 0), 
                           new Vector3(random.Next() % 100, random.Next() % 100, 0));
                    explosionSmokeParticleSystem.AddParticle(new Vector3(box.body.Position.X * 50 - 20 + random.Next() % 70,
                          box.body.Position.Y * 50 - 20 - random.Next() % 70, 0),
                          new Vector3(random.Next() % 100, random.Next() % 100, 0));
                    explosionSmokeParticleSystem.AddParticle(new Vector3(box.body.Position.X * 50 - 20 - random.Next() % 50,
                          box.body.Position.Y * 50 - 20 + random.Next() % 50, 0),
                          new Vector3(random.Next() % 100, random.Next() % 100, 0));
                    if (i == 40)
                    {
                        box.body.Dispose();
                        box.isDisposed = true;
                    }
                }
                Bomb.Play();
            }
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
