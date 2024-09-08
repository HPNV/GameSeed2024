using System.Collections.Generic;
using Plant.States.Aloecure;
using Plant.States.Bamburst;
using Plant.States.Boomkin;
using Plant.States.Cactharn;
using Plant.States.Cobcorn;
using Plant.States.Duricane;
using Plant.States.Weisshooter;

namespace Plant.Factory
{
    public class PlantStateFactory
    {
        public static Dictionary<EPlantState, PlantState> CreateStates(EPlant type, Plant plant)
        {
            return type switch
            {
               EPlant.Cactharn => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new CactharnAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Cobcorn => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new CobcornAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Weisshooter => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new WeisshooterAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Duricane => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new DuricaneAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Boomkin => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new BoomkinIdleState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new BoomkinDieState(plant)}
                },
                EPlant.Bamburst => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new BamburstAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Aloecure => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new AloecureIdleState(plant)},
                    { EPlantState.Attack , new AloecureAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                _ => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new PlantAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                }
            };
        }
    }
}