﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding.Graph.PathFinding
{
    /// <summary>
    /// Represents the path of an <see cref="ITraversalStrategy{T}"/> across an <see cref="IGraph2D{TNode}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of all nodes in the graph. If <typeparamref name="T"/> is a reference type, nodes
    /// cannot be null references.</typeparam>
    /// <remarks><para>
    /// <b>IGraphPath</b> tracks the path of an <see cref="ITraversalStrategy{T}"/> instance moving across
    /// an <see cref="IGraph2D{TNode}"/> instance.
    /// </para><para>
    /// In addition to the actual movement path as a sequence of <see cref="IGraph2D{TNode}"/> nodes,
    /// <b>IGraphPath</b> also provides the total cost of this path, and a method to determine the
    /// last path node that can be reached within a given maximum cost.</para></remarks>

    public interface IGraphPath<T>
    {
        #region Nodes

        /// <summary>
        /// Gets a list of all <see cref="IGraph2D{TNode}"/> nodes in the movement path.</summary>
        /// <value>
        /// An <see cref="IList{T}"/> containing all <see cref="IGraph2D{TNode}"/> nodes that constitute
        /// the movement path.</value>
        /// <remarks>
        /// <b>Nodes</b> should never return a null reference, but it may return an empty collection
        /// to signify invalid data.</remarks>

        IList<T> Nodes { get; }

        #endregion
        #region TotalCost

        /// <summary>
        /// Gets the total cost of the <see cref="IGraphPath{T}"/>.</summary>
        /// <value>
        /// The sum of the <see cref="ITraversalStrategy{T}.GetStepCost"/> results for all <see
        /// cref="Nodes"/>.</value>
        /// <remarks>
        /// <b>TotalCost</b> may return a non-positive value to signify invalid data.</remarks>

        double TotalCost { get; }

        #endregion
        #region GetLastNode

        /// <summary>
        /// Returns the last <see cref="IGraph2D{TNode}"/> node in the <see cref="IGraphPath{T}"/> whose
        /// total cost does not exceed the specified maximum cost.</summary>
        /// <param name="maxCost">
        /// The maximum total path cost of the returned <see cref="IGraph2D{TNode}"/> node.</param>
        /// <returns>
        /// The last <see cref="Nodes"/> element whose total path cost does not exceed the specified
        /// <paramref name="maxCost"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxCost"/> is zero or negative.</exception>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="IGraphPath{T}"/> instance contains invalid data.</exception>

        T GetLastNode(double maxCost);

        #endregion
    }
}
