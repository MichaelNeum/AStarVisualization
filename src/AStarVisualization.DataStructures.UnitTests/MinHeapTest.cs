﻿using NUnit.Framework;
using System.Linq;
using System;

namespace AStarVisualization.DataStructures.UnitTests
{
    [TestFixture]
    public class MinHeapTest
    {
        [Test]
        [TestCase(100)]
        [TestCase(25)]
        [TestCase(25000)]
        [TestCase(4508)]
        public void Constructor_SetsCapacity_SetsRightCapacity(int capacity)
        {
            var minHeap = new MinHeap<int>(capacity);
            int actualCapacity = minHeap.Capacity;

            Assert.AreEqual(actualCapacity, capacity);
        }

        private static object[] Pop_GetsPushedMultipleValues_ReturnsSmallestValue_Cases =
        {
            new object[] { 1, 10, 38, 1, 2183, 8372, 293 },
            new object[] { 112, 0131, 8392, 290, 128, 112, 238 },
            new object[] { 1023, 10381234, 823984137, 1992, 1902, 1023, 2839 },
        };
        [Test, TestCaseSource("Pop_GetsPushedMultipleValues_ReturnsSmallestValue_Cases")]
        public void Pop_GetsPushedMultipleValues_ReturnsSmallestValue(object[] values)
        {
            int expectedSmallestValue = (int)values[0];
            int[] numbers = values.Skip(1).Cast<int>().ToArray<int>();

            var minHeap = new MinHeap<int>(values.Length);
            foreach (int num in numbers)
                minHeap.Add(num);
            int actualSmallestValue = minHeap.GetMinimumElement();

            Assert.AreEqual(actualSmallestValue, expectedSmallestValue);
        }
        [Test, TestCaseSource("Pop_GetsPushedMultipleValues_ReturnsSmallestValue_Cases")]
        public void Peek_GetsPushedMultipleValues_ReturnsSmallestValue(object[] values)
        {
            int expectedSmallestValue = (int)values[0];
            int[] numbers = values.Skip(1).Cast<int>().ToArray<int>();

            var minHeap = new MinHeap<int>(values.Length);
            foreach (int num in numbers)
                minHeap.Add(num);
            int actualSmallestValue = minHeap.Peek();

            Assert.AreEqual(actualSmallestValue, expectedSmallestValue);
        }
        [Test]
        public void Add_AddsElementToHeap_CountIncreases()
        {
            int[] numbers = { 10, 123, 352, 1024 };
            var minHeap = new MinHeap<int>(10);

            foreach (var number in numbers)
                minHeap.Add(number);
            int previousCount = minHeap.Count;
            minHeap.Add(18324);
            int actualCount = minHeap.Count;

            Assert.That(previousCount < actualCount);
        }
        [Test]
        public void GetMinimumElement_RemovesElement_DecreasesCount()
        {
            int[] numbers = { 10, 123, 352, 1024 };
            var minHeap = new MinHeap<int>(10);

            foreach (var number in numbers)
                minHeap.Add(number);
            int previousCount = minHeap.Count;
            var num = minHeap.GetMinimumElement();
            int actualCount = minHeap.Count;

            Assert.That(previousCount > actualCount);
        }
        [Test]
        public void Add_ExceedsCapacity_IncreasesCapacity()
        {
            int[] numbers = { 10, 1234, 583, 28, 28 };
            var minHeap = new MinHeap<int>(5);

            foreach (var num in numbers)
                minHeap.Add(num);
            int previousCapacity = minHeap.Capacity;

            minHeap.Add(103);
            int actualCapacity = minHeap.Capacity;

            Assert.That(actualCapacity == previousCapacity * 2);
        }
    }
}
