using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    /// Encapsulates the enemies in the game of Galaga
    /// </summary>
    public class EnemiesManager : ICollection<GameObject>
    {
        private readonly Collection<GameObject> enemies;
        
        /// <summary>
        /// The amount of level 1 enemies
        /// </summary>
        public const int NumOfLvl1Enemies = 2;
        /// <summary>
        /// The amount of level 2 enemies
        /// </summary>
        public const int NumOfLvl2Enemies = 3;
        /// <summary>
        /// The amount of level 3 enemies
        /// </summary>
        public const int NumOfLvl3Enemies = 4;
        /// <summary>
        /// Gets or sets the number of steps taken by the enemies
        /// </summary>
        public int StepsTaken { get; set; } 
        /// <summary>
        /// Gets or sets the number of steps for the enemies to take in each direction
        /// </summary>
        public int NumOfStepsInEachDirection { get; set; }
        /// <summary>
        /// Creates an instance of the Enemies object
        /// Post-condition: this.enemies == enemies
        /// </summary>
        public EnemiesManager()
        {
            Collection<GameObject> enemies = new Collection<GameObject>();

            for (int i = 0; i < NumOfLvl1Enemies; i++)
            {
                enemies.Add(new Lvl1Enemy());
            }

            for (int i = 0; i < NumOfLvl2Enemies; i++)
            {
                enemies.Add(new Lvl2Enemy());
            }

            for (int i = 0; i < NumOfLvl3Enemies; i++)
            {
                enemies.Add(new Lvl3Enemy());
            }

            this.enemies = enemies;
            this.StepsTaken = 0;
            this.NumOfStepsInEachDirection = 5;
        }

        
        /// <summary>
        /// Moves all enemies to the left one step
        /// PostCondition: StepsTaken == @prev + 1
        /// </summary>
        public void MoveEnemiesLeft()
        {
            foreach (GameObject enemy in this.enemies)
            {
                enemy.MoveLeft();
            }

            this.StepsTaken++;
        }
        /// <summary>
        /// Moves all enemies to the right one step
        /// PostCondition: StepsTaken == @prev + 1
        /// </summary>
        public void MoveEnemiesRight()
        {
            foreach (GameObject enemy in this.enemies)
            {
                enemy.MoveRight(); 
            }

            this.StepsTaken++;
        }
        

        public void ShootWeapons()
        {
            //TODO implement and comments
        }
        /// <summary>
        /// Gets the enumerator of the collection
        /// </summary>
        /// <returns>the enumerator</returns>
       
        public IEnumerator<GameObject> GetEnumerator()
        {
            //TODO dispose of enumerator somehow
            return this.enemies.GetEnumerator();
        }
        
        /// <summary>
        /// Adds a game object to the collection
        /// </summary>
        /// <param name="item">the item to add</param>
        /// <exception cref="ArgumentException">item cannot be null</exception>
        public void Add(GameObject item)
        {
            if (item == null)
            {
                throw new ArgumentException(nameof(item));
            }

            this.enemies.Add(item);

        }
        /// <summary>
        /// Clears the collection of all elements
        /// </summary>
        public void Clear()
        {
            this.enemies.Clear();
        }
        /// <summary>
        /// Checks if the collection contains the specific item
        /// </summary>
        /// <param name="item">the item to check for</param>
        /// <returns>true if item was found, false if not</returns>
        /// <exception cref="ArgumentException">item cannot be null</exception>
        public bool Contains(GameObject item)
        {
            if (item == null)
            {
                throw new ArgumentException(nameof(item));
            }
            foreach(var enemy in this.enemies)
            {
                if (enemy == item)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Copies the collection to an array
        /// </summary>
        /// <param name="array">the array to copy to </param>
        /// <param name="arrayIndex">the index of the collection to begin copying at</param>
        /// <exception cref="ArgumentException">array cannot be null</exception>
        /// <exception cref="IndexOutOfRangeException">the index must be greater than 0 and not out of range of the collection</exception>
        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentException(nameof(array));
            }

            if (arrayIndex < 0 && arrayIndex > this.enemies.Count - 1)
            {
                throw new IndexOutOfRangeException("index must be greater than 0 and in range of collection");
            }

            this.enemies.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Removes the item from the collection
        /// </summary>
        /// <param name="item">the item to remove</param>
        /// <returns>true if the item was removed false otherwise</returns>
        /// <exception cref="ArgumentException">the item cannot be null</exception>
        public bool Remove(GameObject item)
        {
            if (item == null)
            {
                throw new ArgumentException(nameof(item));
            }

            foreach (var enemy in this.enemies)
            {
                if (enemy == item)
                {
                    this.enemies.Remove(item);
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Supports a simple iteration of a non-generic collection
        /// </summary>
        /// <returns>the enumerator of the collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the count
        /// </summary>
        public int Count => this.enemies.Count;
        /// <summary>
        /// Checks if the collection is read only
        /// </summary>
        public bool IsReadOnly => false;
    }
}
