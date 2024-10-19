using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace Galaga.Model
{
    public class BulletsManager : ICollection<GameObject>
    {
        private readonly Collection<GameObject> bullets;


        public BulletsManager()
        {
            this.bullets = new Collection<GameObject>();
            for (int i = 0; i < EnemiesManager.NumOfLvl3Enemies; i++)
            {
                this.bullets.Add(new Bullet());
            }
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            return this.bullets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(GameObject item)
        {
            this.bullets.Add(item);
        }

        public void Clear()
        {
            this.bullets.Clear();
        }

        public bool Contains(GameObject item)
        {
            return this.bullets.Contains(item);
        }

        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            this.bullets.CopyTo(array, arrayIndex);
        }

        public bool Remove(GameObject item)
        {
            return this.bullets.Remove(item);
        }
        /// <summary>
        /// Gets the count of bullets in the collection
        /// </summary>
        public int Count => this.bullets.Count;
        /// <summary>
        /// Checks if the collection is read only
        /// </summary>
        public bool IsReadOnly { get; } = false;
    }
}
