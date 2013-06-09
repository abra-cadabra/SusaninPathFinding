using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentAssertions;

namespace SusaninPathFindingTests.TDDTests
{
    /// <summary>
    /// Родительский класс для тестов, реализующий шаблон TDD теста
    /// </summary>
    public abstract class TDDTestSpecification
    {
        /// <summary>
        /// Инициализация теста
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            Setup();
        }
        /// <summary>
        /// Деинициализация теста
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            CleenUp();
        }

        /// <summary>
        /// В эту функцию впишите действия, которые должны проводиться перед каждым тестом.
        /// Например, подготовку условий тестирования.
        /// </summary>
        public abstract void Setup();
        /// <summary>
        /// В эту функцию впишите всё, что должно производиться после теста.
        /// Например, очистку ресурсов, удаление созданных в ходе тестирования файлов и т.д.
        /// </summary>
        public abstract void CleenUp();

    }
}
