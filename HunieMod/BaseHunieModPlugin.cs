using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace HunieMod
{
    /// <summary>
    /// The base plugin type that adds HuniePop-specific functionality over the default <see cref="BaseUnityPlugin"/>
    /// </summary>
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class BaseHunieModPlugin : BaseUnityPlugin
    {
        /// <summary>
        /// The identifier of this plugin
        /// </summary>
        public const string PLUGIN_GUID = "com.lounger.huniemod";

        /// <summary>
        /// The name of this plugin
        /// </summary>
        public const string PLUGIN_NAME = "HunieMod";

        /// <summary>
        /// The version of this plugin
        /// </summary>
        public const string PLUGIN_VERSION = "0.1.0.0";

        #region Game core

        /// <summary>
        /// General game manager containing all other managers and general game settings
        /// </summary>
        public static GameManager Game => GameManager.System;

        /// <summary>
        /// General data class containing nearly all of the game's assets definitions
        /// </summary>
        public static GameData Data => GameManager.Data;

        /// <summary>
        /// The game's main camera
        /// </summary>
        public static Camera MainCam => GameManager.System.gameCamera.mainCamera;

        /// <summary>
        /// The root container on which all visible elements are placed
        /// </summary>
        public static Stage GameStage => GameManager.Stage;

        /// <summary>
        /// Manages location traveling, arrivals and departures
        /// </summary>
        public static LocationManager Location => Game.Location;

        /// <summary>
        /// The main player's settings, stats and active game variables
        /// </summary>
        public static PlayerManager Player => Game.Player;

        /// <summary>
        /// Manages going to puzzle locations and puzzle game logic, whereas <see cref="PuzzleManager.Game"/>
        /// manages the more visual aspects of the puzzle game
        /// </summary>
        public static PuzzleManager Puzzle => Game.Puzzle;

        #endregion

        #region Game Data

        /// <summary>
        /// Instances of all the Ability definitions in the game
        /// </summary>
        public static List<AbilityDefinition> AllAbilities => GetDefinitions<AbilityDefinition>(Data.Abilities);

        /// <summary>
        /// Instances of all the Action Menu Item definitions in the game
        /// </summary>
        public static List<ActionMenuItemDefinition> AllActionMenuItems => GetDefinitions<ActionMenuItemDefinition>(Data.ActionMenuItems);

        /// <summary>
        /// Instances of all the cellphone app definitions in the game
        /// </summary>
        public static List<CellAppDefinition> AllCellApps => GetDefinitions<CellAppDefinition>(Data.CellApps);

        /// <summary>
        /// Instances of all the Debug Profile definitions in the game
        /// </summary>
        public static List<DebugProfile> AllDebugProfiles => GetDefinitions<DebugProfile>(Data.DebugProfiles);

        /// <summary>
        /// Instances of all the Dialog Scene definitions in the game
        /// </summary>
        public static List<DialogSceneDefinition> AllDialogScenes => GetDefinitions<DialogSceneDefinition>(Data.DialogScenes);

        /// <summary>
        /// Instances of all the Dialog Trigger definitions in the game
        /// </summary>
        public static List<DialogTriggerDefinition> AllDialogTriggers => GetDefinitions<DialogTriggerDefinition>(Data.DialogTriggers);

        /// <summary>
        /// Instances of all the Energy Trail definitions in the game
        /// </summary>
        public static List<EnergyTrailDefinition> AllEnergyTrails => GetDefinitions<EnergyTrailDefinition>(Data.EnergyTrails);

        /// <summary>
        /// Instances of all the girl definitions in the game
        /// </summary>
        public static List<GirlDefinition> AllGirls => Data.Girls.GetAll();

        /// <summary>
        /// Instances of all the Item definitions in the game
        /// </summary>
        public static List<ItemDefinition> AllItems => GetDefinitions<ItemDefinition>(Data.Items);

        /// <summary>
        /// Instances of all the location definitions in the game
        /// </summary>
        public static List<LocationDefinition> AllLocations => GetDefinitions<LocationDefinition>(Data.Locations);

        /// <summary>
        /// Instances of all the Message definitions in the game
        /// </summary>
        public static List<MessageDefinition> AllMessages => GetDefinitions<MessageDefinition>(Data.Messages);

        /// <summary>
        /// Instances of all the 2D Particle Emitter definitions in the game
        /// </summary>
        public static List<ParticleEmitter2DDefinition> AllParticles => GetDefinitions<ParticleEmitter2DDefinition>(Data.Particles);

        /// <summary>
        /// Instances of all the Sprite Group definitions in the game
        /// </summary>
        public static List<SpriteGroupDefinition> AllSpriteGroups => GetDefinitions<SpriteGroupDefinition>(Data.SpriteGroups);

        /// <summary>
        /// Instances of all the Trait definitions in the game
        /// </summary>
        public static List<TraitDefinition> AllTraits => GetDefinitions<TraitDefinition>(Data.Traits);

        #endregion

        #region Locations

        /// <summary>
        /// The definition of the location that is currently active
        /// </summary>
        public static LocationDefinition CurrentLocationDef => Location?.currentLocation;

        /// <summary>
        /// The ID of the location that is currently active
        /// </summary>
        public static LocationId? CurrentLocation => (LocationId?)CurrentLocationDef?.id;

        #endregion

        #region Girls

        /// <summary>
        /// The definition of the girl that is currently active
        /// </summary>
        public static GirlDefinition CurrentGirlDef => Location?.currentGirl;

        /// <summary>
        /// The visual object of the main girl currently on the stage
        /// </summary>
        public static Girl CurrentStageGirlObject => GameStage.girl;

        /// <summary>
        /// The visual object of the alt. girl currently on the stage
        /// </summary>
        public static Girl CurrentStageAltGirlObject => GameStage.altGirl;

        /// <summary>
        /// The ID of the girl that is currently active
        /// </summary>
        public static GirlId? CurrentGirl => (GirlId?)CurrentGirlDef?.id;

        /// <summary>
        /// The ID of the main girl currently on the stage
        /// </summary>
        public static GirlId? CurrentStageGirl => (GirlId?)CurrentStageGirlObject?.definition.id;

        /// <summary>
        /// The ID of the alt. girl currently on the stage
        /// </summary>
        public static GirlId? CurrentStageAltGirl => (GirlId?)CurrentStageAltGirlObject?.definition.id;

        /// <summary>
        /// Tries to find an instance of a girl's definition that matches with the specified ID
        /// </summary>
        /// <param name="girlId">The ID of the girl to find</param>
        /// <returns>The definition of the girl, or default if not found</returns>
        public static GirlDefinition GetGirl(GirlId girlId) => AllGirls.FirstOrDefault(girl => (GirlId)girl.id == girlId);

        /// <summary>
        /// Tries to find an instance of a girl's definition with the specified name, case-insensitive
        /// </summary>
        /// <param name="firstName">The first name of the girl to find</param>
        /// <returns>The definition of the girl, or default if not found</returns>
        public static GirlDefinition GetGirl(string firstName) => AllGirls.FirstOrDefault(girl => string.Equals(girl.firstName, firstName, StringComparison.OrdinalIgnoreCase));

        #endregion

        #region Events

        private static EventManager events;

        /// <summary>
        /// Event helper that wraps certain key game events
        /// </summary>
        protected static EventManager Events => events = events ?? new EventManager();

        /// <summary>
        /// Event helper that wraps certain key game events
        /// </summary>
        protected class EventManager
        {
            /// <summary>
            /// Fires after <see cref="GameManager.Pause"/> has frozen all game elements but the cellphone
            /// </summary>
            public event GameManager.GameManagerDelegate GamePause
            {
                add { Game.GamePauseEvent += value; }
                remove { Game.GamePauseEvent -= value; }
            }

            /// <summary>
            /// Fires after <see cref="GameManager.Unpause"/> has unfrozen all game elements
            /// </summary>
            public event GameManager.GameManagerDelegate GameUnpause
            {
                add { Game.GameUnpauseEvent += value; }
                remove { Game.GameUnpauseEvent -= value; }
            }

            /// <summary>
            /// Fires after LocationManager.OnLocationArrival() has initialized the arrival sequence and before the location is "settled"
            /// </summary>
            public event LocationManager.LocationDelegate LocationArrive
            {
                add { Location.LocationArriveEvent += value; }
                remove { Location.LocationArriveEvent -= value; }
            }

            /// <summary>
            /// Fires after LocationManager.OnLocationDeparture() has set up the new location and transition screen.
            /// Shortly before LocationManager.ArriveLocation() fires
            /// </summary>
            public event LocationManager.LocationDelegate LocationDepart
            {
                add { Location.LocationDepartEvent += value; }
                remove { Location.LocationDepartEvent -= value; }
            }

            /// <summary>
            /// Fires when <see cref="Stage.OnStart"/> has setup all it's child elements
            /// </summary>
            public event Stage.StageDelegate StageStarted
            {
                add { GameStage.StageStartedEvent += value; }
                remove { GameStage.StageStartedEvent -= value; }
            }
        }

        #endregion

        #region Internal Methods

        private static List<T> GetDefinitions<T>(object instance) where T : Definition
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var field = AccessTools.Field(instance.GetType(), "_definitions") ?? throw new InvalidOperationException($"Field _definitions was not found on type {instance.GetType()}");
            return (field.GetValue(instance) as Dictionary<int, T>).Values.ToList();
        }

        #endregion
    }
}
