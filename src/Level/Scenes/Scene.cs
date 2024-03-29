﻿using Blip.src.Engine.Level;
using Blip.src.Manager.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Level.Scenes;
public abstract class Scene
{
    public GameStateManager gameStateManager;

    public Camera camera;
    
    public Scene(GameStateManager gameStateManager, Camera camera)
    {
        this.gameStateManager = gameStateManager;
        this.camera = camera;
    }

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch levelSpriteBatch);
}
