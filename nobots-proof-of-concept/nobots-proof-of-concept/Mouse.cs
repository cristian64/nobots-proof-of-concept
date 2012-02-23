using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Input;

namespace nobots_proof_of_concept
{
    class Mouse : Character
    {
        SpriteBatch spriteBatch;

        Rectangle rectangle;
        Body body;
        World world;

        public Mouse(Game game, World world)
            : base(game)
        {
            this.world = world;
            isHaunted = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("mouse");
            rectangle = new Rectangle((int)(Game1.screenWidth / 3), (int)(Game1.screenHeight / 1.3), texture.Width/2, texture.Height/2);

            body = BodyFactory.CreateRectangle(world, 0.02f * rectangle.Width, 0.02f * rectangle.Height, 1.0f);
            body.Position = new Vector2(0.02f * rectangle.X, 0.02f * rectangle.Y);
            body.BodyType = BodyType.Dynamic;
            //body.FixedRotation = true;
            body.Friction = 0.01f;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, 50.0f * body.Position, null, Color.White, body.Rotation, new Vector2(texture.Width / 2, texture.Height / 2),
                0.5f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();

            if (isHaunted)
            {
                if (keybState.IsKeyDown(Keys.Left))
                {
                    body.ApplyForce(new Vector2(-15, 0));
                }

                if (keybState.IsKeyDown(Keys.Right))
                {
                    body.ApplyForce(new Vector2(15, 0));
                }

                if (keybState.IsKeyDown(Keys.Up))
                {
                    body.ApplyForce(new Vector2(-15, 0));
                }
            }
        }
    }
}
