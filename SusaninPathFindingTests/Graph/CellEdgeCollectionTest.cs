using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SusaninPathFinding.Collections;
using SusaninPathFinding.Geometry;
using SusaninPathFinding.Graph;
using SusaninPathFinding.Graph.NodeInfoTypes;

namespace SusaninPathFindingTests.Graph
{
    [TestFixture]
    class CellEdgeCollectionTest
    {
        public CellEdgeCollection Tester;
        public Cell c1 = new Cell(0, 0, 0, new Cell3D(10, 10, 10), null);
        public Cell c2 = new Cell(1, 0, 0, new Cell3D(10, 10, 10), null);
        public Cell c3 = new Cell(0, 1, 0, new Cell3D(10, 10, 10), null);
        public Cell c4 = new Cell(1, 1, 0, new Cell3D(10, 10, 10), null);

        [SetUp]
        public void Setup()
        {
            Tester = new CellEdgeCollection();

            Tester.Add(new CellEdge(c1, c3, new Passable()));
            Tester.Add(new CellEdge(c1, c4, new Passable()));

            Tester.Add(new CellEdge(c2, c3, new Impassable()));
            Tester.Add(new CellEdge(c2, c4, new Passable()));

            Tester.Add(new CellEdge(c3, c4, new Passable()));
        }

        [Test]
        public void AddByTwoIndecesAndValueTest()
        {
            CellEdge edge = new CellEdge(c1, c2, new Passable());

            Tester.Add(c1, c2, edge);

            Tester[c1, c2].Should().BeSameAs(edge);
            Tester[c2, c1].Should().BeSameAs(edge);
        }

        [Test]
        public void AddValueTest()
        {
            CellEdge edge = new CellEdge(c1, c2, new Passable());

            Tester.Add(edge);

            Tester[c1, c2].Should().BeSameAs(edge);
            Tester[c2, c1].Should().BeSameAs(edge);

            CellEdge edge2 = new CellEdge(c1, c3, new Impassable());

            Tester.Add(edge2);

            Tester[c1, c3].Should().BeSameAs(edge2);
            Tester[c3, c1].Should().BeSameAs(edge2);
        }

        [Test]
        public void CopyToTest()
        {
            CellEdge[] arr = new CellEdge[Tester.Count];
            Tester.CopyTo(arr, 0);
            arr[0].Should().BeSameAs(Tester[c1, c3]);
        }

        [Test]
        public void ContainsIndexTest()
        {
            Tester.ContainsKeys(c1, c3).Should().BeTrue();
            Tester.ContainsKeys(c1, c2).Should().BeFalse();
        }

        [Test]
        public void ContainsTest()
        {

            Tester.Contains(Tester[c1, c3]).Should().BeTrue();

            CellEdge edge = new CellEdge(c1, c2, new Passable());

            Tester.Contains(edge).Should().BeFalse();

            CellEdge edge2 = new CellEdge(c1, c3, new Passable());

            Tester.Contains(edge2).Should().BeFalse();
        }

        [Test]
        public void TryGetValueTest()
        {

            CellEdge value;

            Tester.TryGetValue(c1, c3, out value).Should().BeTrue();
            value.Should().BeSameAs(Tester[c3, c1]);

            Tester.TryGetValue(c1, c2, out value).Should().BeFalse();
            value.Should().BeNull();
        }
    }
}
