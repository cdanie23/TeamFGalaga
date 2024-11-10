namespace Galaga.Model
{
    /// <summary>
    ///     The enemy class which inherits game object
    /// </summary>
    public class Enemy : GameObject
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the points associated with the enemy
        ///     <param name="value">the value to set points to</param>
        /// </summary>
        public int Points { get; set; }

        #endregion
    }
}