using System;
using System.Collections.Generic;
using Assets.Infrastructure.States;
using Assets.Interface;
using Assets.StateMachines;

namespace Assets.Infrastructure
{
    public class GameStateMachine : StateMachineBase
    {
        public GameStateMachine(SceneLoader sceneLoader, SdkLoader sdkLoader)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sdkLoader, sceneLoader),
                [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader),
                [typeof(TrainingState)] = new TrainingState(this, sceneLoader),
                [typeof(GameplayState)] = new GameplayState(this),
            };
        }
    }
}