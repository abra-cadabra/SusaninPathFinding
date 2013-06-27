using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;

namespace SusaninPathFinding.Collections
{
    /// <summary>
    /// Represents a storage class for <see cref="CellEdge"/>
    /// </summary>
    public class CellEdgeCollection : ICloneable, ICollection<CellEdge>, ICollection//Dictionary<CellEdgeIndex, CellEdge>
    {
        #region Fields

        //private Dictionary<Node, CellEdge[]> _edgesOfCellCollection;

        private Dictionary<CellEdgeIndex, CellEdge> _commonEdgeCollection;
 
        #endregion

        #region Operators

        public CellEdge this[Cell first, Cell second]
        {
            get
            {
                CellEdge edge;
                if(!TryGetValue(first, second, out edge))
                {
                    edge = new CellEdge(first, second, null);
                    Add(edge);
                }
                    //throw new KeyNotFoundException(String.Format(Strings.KeyNotFound, first.ToString()+second.ToString()));
                return edge;
            }
            set
            {
                if (ContainsKeys(first, second))
                {
                    CellEdgeIndex index = new CellEdgeIndex(first, second);
                    value.From = index.From;
                    value.To = index.To;
                    _commonEdgeCollection[index] = value;
                }

                Add(first, second, value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Provides collection for storing cell edges. Edges are indexed with two nodes, for which they are common.
        /// For example edge 1 is common for cell {1, 0, 0} and cell {0, 0, 0}. Order of specification is insignificant.
        /// </summary>
        public CellEdgeCollection()
        {
            _commonEdgeCollection = new Dictionary<CellEdgeIndex, CellEdge>();
        }

        /// <summary>
        /// Provides collection for storing cell edges. Edges are indexed with two nodes, for which they are common.
        /// For example edge 1 is common for cell {1, 0, 0} and cell {0, 0, 0}. Order of specification is insignificant.
        /// </summary>
        public CellEdgeCollection(CellEdgeCollection other)
        {
            _commonEdgeCollection = new Dictionary<CellEdgeIndex, CellEdge>();
            foreach (var edge in other._commonEdgeCollection)
            {
                _commonEdgeCollection.Add(edge.Key, edge.Value);
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Adds an edge for two provided nodes to the collection. <b>WARNING!</b> Nodes must be neighbors!
        /// </summary>
        /// <param name="first">First node</param>
        /// <param name="second">Second node</param>
        /// <param name="value">Value that should be added to the collection.</param>
        public void Add(Cell first, Cell second, CellEdge value)
        {
            if(first.IsNeighborTo(second) != CellsNeighboringState.Neighbors)
                throw new ArgumentException(Strings.CellsNotNeighbors);

            CellEdgeIndex index = new CellEdgeIndex(first, second);
            value.From = index.From;
            value.To = index.To;
            Add(value);
            //_commonEdgeCollection.Add(index, value);
        }

        /// <summary>
        /// Adds the specified edge to collection
        /// </summary>
        /// <param name="value">Edge that should be added</param>
        public void Add(object value)
        {
            if (!(value.GetType().IsSubclassOf(typeof(CellEdge))))
                throw new ArgumentException(String.Format(Strings.ArgumentTypeMismatch, typeof(CellEdge)), "value");

            Add((CellEdge)value);
            //CellEdge edge = (CellEdge)value;
            //CellEdgeIndex index = new CellEdgeIndex(edge.From, edge.To);
            //_commonEdgeCollection.Add(index, (CellEdge)value);
        }

        /// <summary>
        /// Adds the specified edge to collection
        /// </summary>
        /// <param name="value">Edge that should be added</param>
        public void Add(CellEdge item)
        {
            CellEdgeIndex index = new CellEdgeIndex(item.From, item.To);

            item.From = index.From;
            item.To = index.To;

            if (!ContainsKeys(index.From, index.To))
                _commonEdgeCollection.Add(index, item);
            else
                _commonEdgeCollection[index] = item;
        }

        /// <summary>
        /// Copies the elements of the ICollection to an Array, starting at a particular Array index. (Inherited from ICollection.)
        /// </summary>
        /// <param name="array">The destenation array</param>
        /// <param name="index">Starting index</param>
        public void CopyTo(Array array, int index)
        {
            Array source = _commonEdgeCollection.Values.ToArray();
            source.CopyTo(array, index);
        }


        /// <summary>
        /// Copies the elements of the ICollection to an Array, starting at a particular Array index. (Inherited from ICollection.)
        /// </summary>
        /// <param name="array">The destenation array</param>
        /// <param name="index">Starting index</param>
        public void CopyTo(CellEdge[] array, int index)
        {
            //Array source = _commonEdgeCollection.Values.ToArray();
            //source.CopyTo(array, index);
            CopyTo((Array)array, index);

            //Array.Copy(source, arrayIndex, (Array)array, 0, source.Length);

        }


        /// <summary>
        /// Determines whether the collection contains a common edge of two specified nodes.
        /// </summary>
        /// <param name="first">First node</param>
        /// <param name="second">Second node</param>
        /// <returns>True, if an edge with specified index exists in this collection.</returns>
        public bool ContainsKeys(Cell first, Cell second)
        {
            return _commonEdgeCollection.ContainsKey(new CellEdgeIndex(first, second));
        }

        /// <summary>
        /// Determines whether the IDictionary object contains an element with the specified key.
        /// </summary>
        /// <param name="value">The value to locate in the IDictionary object.</param>
        /// <returns></returns>
        public bool Contains(object value)
        {
            if (!(value.GetType().IsSubclassOf(typeof(CellEdge))))
                throw new ArgumentException(String.Format(Strings.ArgumentTypeMismatch, typeof(CellEdge)), "value");

            return Contains((CellEdge)value);
        }

        /// <summary>
        /// Determines whether the collection contains a specific edge.
        /// </summary>
        /// <param name="item">The value to locate in the collection. The value can be null for reference types.</param>
        /// <returns>true if the collection contains an element with the specified value; otherwise, false.</returns>
        public bool Contains(CellEdge item)
        {
            return _commonEdgeCollection.Values.Contains(item);
        }

        /// <summary>
        /// Searches collection for all edges of specified cell
        /// </summary>
        /// <param name="index">The cell whose edges should be found.</param>
        /// <returns>List edges of specified node.</returns>
        //public List<CellEdge> FindBySingleIndex(Cell index)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

        #region ICollection Implementation


        #region Properties

        /// <summary>
        /// Gets the number of elements contained in the ICollection.
        /// </summary>
        public int Count
        {
            get { return _commonEdgeCollection.Count; }
        }


        /// <summary>
        /// Gets an object that can be used to synchronize access to the ICollection.
        /// </summary>
        public object SyncRoot
        {
            get { return new object(); }
        }

        /// <summary>
        /// Gets a value indicating whether access to the ICollection is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized { get; private set; }


        public bool IsReadOnly { get; private set; }
        public bool IsFixedSize { get; private set; }

        #endregion


        #region Functions

        /// <summary>
        /// Returns an enumerator that iterates through a collection. 
        /// </summary>
        /// <returns>Enumerator for the collection</returns>
        IEnumerator<CellEdge> IEnumerable<CellEdge>.GetEnumerator()
        {
            return ((IEnumerable<CellEdge>)_commonEdgeCollection).GetEnumerator();
        }


        /// <summary>
        /// Gets the common edge of two specified nodes.
        /// </summary>
        /// <param name="first">First node</param>
        /// <param name="second">Second node</param>
        /// <param name="value">Returned edge</param>
        /// <returns>True if edge is in collection</returns>
        public bool TryGetValue(Cell first, Cell second, out CellEdge value)
        {
            CellEdge edge;
            bool result = _commonEdgeCollection.TryGetValue(new CellEdgeIndex(first, second), out edge);
            value = edge;
            return result;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection. 
        /// </summary>
        /// <returns>Enumerator for the collection</returns>
        public IEnumerator GetEnumerator()
        {
            return _commonEdgeCollection.GetEnumerator();
        }

        /// <summary>
        /// Removes the element with the specified key from the ICollection object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(CellEdge item)
        {
            _commonEdgeCollection.Remove(new CellEdgeIndex(item.From, item.To));

            return true;
        }


        /// <summary>
        /// Removes all keys and values from the collection.
        /// </summary>
        public void Clear()
        {
            _commonEdgeCollection.Clear();
        }
        #endregion

        #endregion

        #region ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return new CellEdgeCollection(this);
        }

        #endregion
    }

    /// <summary>
    /// Represents two component index for <see cref="CellEdge"/>
    /// </summary>
    public struct CellEdgeIndex
    {
        #region Fields

        public Cell From;
        public Cell To;

        #endregion

        #region Constructors

        public CellEdgeIndex(Cell first, Cell second)
        {
            if (first.Z < second.Z)
            {
                From = first;
                To = second;
                return;
            }
            if(first.Z > second.Z)
            {
                From = second;
                To = first;
                return;
            }

            if(first.Y < second.Y)
            {
                From = first;
                To = second;
                return;
            }
            if(first.Y > second.Y)
            {
                From = second;
                To = first;
                return;
            }

            if( first.X < second.X)
            {
                From = first;
                To = second;
                return;
            }
            if(first.X > second.X)
            {
                From = second;
                To = first;
                return;
            }

            throw new ArgumentException(Strings.ArgumentsEqual, "first, second");
        }

        public CellEdgeIndex(CellEdgeIndex other)
        {
            From = other.From;
            To = other.To;
        }
        #endregion
    }
}
