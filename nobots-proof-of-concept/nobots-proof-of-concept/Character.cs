using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace nobots_proof_of_concept
{
    class Character : DrawableGameComponent
    {
        public Vector2 position;
        public Vector2 direction;
        public int width;
        public int height;
        public int stepSize;
        public float rotation;

        public bool isHaunted;

        public List<Texture2D> textures;

        public Character(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
