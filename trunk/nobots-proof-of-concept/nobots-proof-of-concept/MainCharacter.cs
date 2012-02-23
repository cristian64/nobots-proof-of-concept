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
    class MainCharacter : Character
    {
        SpriteBatch spriteBatch;

        Rectangle rectangle;
        Body body;
        World world;
        SpriteEffects effect;

        public MainCharacter(Game game, World world) : base(game)
        {
            this.world = world;
        }

        public override void Initialize()
        {
            effect = SpriteEffects.None;
            isHaunted = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("Girl");
            rectangle = new Rectangle((int)(Game1.screenWidth / 5), (int)(Game1.screenHeight / 1.7), texture.Width, texture.Height);
            body = BodyFactory.CreateRectangle(world, 0.02f * rectangle.Width, 0.02f * rectangle.Height, 1.0f);
            body.Position = new Vector2(0.02f * rectangle.X, 0.02f * rectangle.Y);
            body.BodyType = BodyType.Dynamic;
            body.FixedRotation = true;
            body.Friction = 10f;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ProcessKeyboard();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, 50.0f * body.Position, null, Color.White, body.Rotation, new Vector2(texture.Width/2,texture.Height/2), 1.0f, effect, 0);
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
                    effect = SpriteEffects.FlipHorizontally;
                    body.ApplyForce(new Vector2(-120, 0));
                }

                if (keybState.IsKeyDown(Keys.Right))
                {
                    effect = SpriteEffects.None;
                    body.ApplyForce(new Vector2(120, 0));
                }
            }
        }

        public void Raise(Character character)
        {
        }

        public void Drop(Character character)
        {
        }

        public void PutOnBox(Character character)
        {
        }

        public void Die()
        {
            isHaunted = false;
        }
    }
}
