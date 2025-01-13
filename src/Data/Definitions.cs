namespace HunieMod
{
    /// <summary>
    /// Helper class that provides access to all the <see cref="Definition"/> objects in the game.
    /// </summary>
    public static class Definitions
    {
        private static List<T> GetDefinitions<T>(object instance) where T : Definition
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            FieldInfo field = AccessTools.Field(instance.GetType(), "_definitions") ?? throw new InvalidOperationException($"Field _definitions was not found on type {instance.GetType()}");
            return [.. (field.GetValue(instance) as Dictionary<int, T>).Values];
        }

        #region Public methods

        /// <summary>
        /// Gets a random definition of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the definition.</typeparam>
        /// <returns>A random definition of the specified type.</returns>
        public static T GetRandom<T>() where T : Definition
        {
            List<T> definitions = typeof(T).Name switch
            {
                nameof(AbilityDefinition) => Abilities as List<T>,
                nameof(ActionMenuItemDefinition) => ActionMenuItems as List<T>,
                nameof(CellAppDefinition) => CellApps as List<T>,
                nameof(DebugProfile) => DebugProfiles as List<T>,
                nameof(DialogSceneDefinition) => DialogScenes as List<T>,
                nameof(DialogTriggerDefinition) => DialogTriggers as List<T>,
                nameof(EnergyTrailDefinition) => EnergyTrails as List<T>,
                nameof(GirlDefinition) => Girls as List<T>,
                nameof(ItemDefinition) => Items as List<T>,
                nameof(LocationDefinition) => Locations as List<T>,
                nameof(MessageDefinition) => Messages as List<T>,
                nameof(ParticleEmitter2DDefinition) => Particles as List<T>,
                nameof(PuzzleTokenDefinition) => PuzzleTokens as List<T>,
                nameof(SpriteGroupDefinition) => SpriteGroups as List<T>,
                nameof(TraitDefinition) => Traits as List<T>,
                _ => [],
            };
            return definitions[UnityEngine.Random.Range(0, definitions.Count)];
        }

        /// <summary>
        /// Tries to find an instance of a girl's definition that matches with the specified ID.
        /// </summary>
        /// <param name="girlId">The ID of the girl to find.</param>
        /// <returns>The definition of the girl, or default if not found.</returns>
        public static GirlDefinition GetGirl(GirlId girlId) => Girls.FirstOrDefault(girl => (GirlId)girl.id == girlId);

        /// <summary>
        /// Tries to find an instance of a girl's definition with the specified name, case-insensitive.
        /// </summary>
        /// <param name="firstName">The first name of the girl to find.</param>
        /// <returns>The definition of the girl, or default if not found.</returns>
        public static GirlDefinition GetGirl(string firstName) => Girls.FirstOrDefault(girl => string.Equals(girl.firstName, firstName, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Tries to find an instance of a location's definition that matches with the specified ID.
        /// </summary>
        /// <param name="locationId">The ID of the location to find.</param>
        /// <returns>The definition of the location, or default if not found.</returns>
        public static LocationDefinition GetLocation(LocationId locationId) => Locations.FirstOrDefault(loc => (LocationId)loc.id == locationId);

        #endregion

        #region Public properties

        /// <summary>
        /// Instances of all the Ability definitions in the game.
        /// </summary>
        public static List<AbilityDefinition> Abilities => GetDefinitions<AbilityDefinition>(GM.Data.Abilities);

        /// <summary>
        /// Instances of all the Action Menu Item definitions in the game.
        /// </summary>
        public static List<ActionMenuItemDefinition> ActionMenuItems => GetDefinitions<ActionMenuItemDefinition>(GM.Data.ActionMenuItems);

        /// <summary>
        /// Instances of all the Cellphone App definitions in the game.
        /// </summary>
        public static List<CellAppDefinition> CellApps => GetDefinitions<CellAppDefinition>(GM.Data.CellApps);

        /// <summary>
        /// Instances of all the Debug Profile definitions in the game.
        /// </summary>
        public static List<DebugProfile> DebugProfiles => GetDefinitions<DebugProfile>(GM.Data.DebugProfiles);

        /// <summary>
        /// Instances of all the Dialog Scene definitions in the game.
        /// </summary>
        public static List<DialogSceneDefinition> DialogScenes => GetDefinitions<DialogSceneDefinition>(GM.Data.DialogScenes);

        /// <summary>
        /// Instances of all the Dialog Trigger definitions in the game.
        /// </summary>
        public static List<DialogTriggerDefinition> DialogTriggers => GetDefinitions<DialogTriggerDefinition>(GM.Data.DialogTriggers);

        /// <summary>
        /// Instances of all the Energy Trail definitions in the game.
        /// </summary>
        public static List<EnergyTrailDefinition> EnergyTrails => GetDefinitions<EnergyTrailDefinition>(GM.Data.EnergyTrails);

        /// <summary>
        /// Instances of all the Girl definitions in the game.
        /// </summary>
        public static List<GirlDefinition> Girls => GM.Data.Girls.GetAll();

        /// <summary>
        /// Instances of all the Item definitions in the game.
        /// </summary>
        public static List<ItemDefinition> Items => GetDefinitions<ItemDefinition>(GM.Data.Items);

        /// <summary>
        /// Instances of all the Location definitions in the game.
        /// </summary>
        public static List<LocationDefinition> Locations => GetDefinitions<LocationDefinition>(GM.Data.Locations);

        /// <summary>
        /// Instances of all the Message definitions in the game.
        /// </summary>
        public static List<MessageDefinition> Messages => GetDefinitions<MessageDefinition>(GM.Data.Messages);

        /// <summary>
        /// Instances of all the 2D Particle Emitter definitions in the game.
        /// </summary>
        public static List<ParticleEmitter2DDefinition> Particles => GetDefinitions<ParticleEmitter2DDefinition>(GM.Data.Particles);

        /// <summary>
        /// Instances of all the Puzzle Token definitions in the game.
        /// </summary>
        public static List<PuzzleTokenDefinition> PuzzleTokens => [.. GM.Data.PuzzleTokens.GetAll()];

        /// <summary>
        /// Instances of all the Sprite Group definitions in the game.
        /// </summary>
        public static List<SpriteGroupDefinition> SpriteGroups => GetDefinitions<SpriteGroupDefinition>(GM.Data.SpriteGroups);

        /// <summary>
        /// Instances of all the Trait definitions in the game.
        /// </summary>
        public static List<TraitDefinition> Traits => GetDefinitions<TraitDefinition>(GM.Data.Traits);

        #endregion
    }
}
