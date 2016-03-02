using System;
using System.Text;
using System.Collections.Generic;
using Home.Budget;
using Home.Presentation.ButtonGridControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestProject
{
    /// <summary>
    /// Summary description for ButtonGridTest
    /// </summary>
    [TestClass]
    public class ButtonGridTest
    {
        public ButtonGridTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private void TestMatrix(ButtonGridMatrix matrix)
        {
            for (int i = 0; i < matrix.Items.Count; i++)
            {
                Assert.AreEqual(matrix[i/matrix.Dimension, i%matrix.Dimension], matrix.Items[i]);
            }
        }

        private ButtonGridItem CreateIndexItem(int index) => new ButtonGridItem(new Product() {Name = index.ToString()});
        [TestMethod]
        public void TestMatrix()
        {
            var items = new List<ButtonGridItem>()
            {
                CreateIndexItem(0),
                CreateIndexItem(1),
                CreateIndexItem(2),
                CreateIndexItem(3),
                CreateIndexItem(4),
                CreateIndexItem(5),
                CreateIndexItem(6),
                CreateIndexItem(7),
                CreateIndexItem(8), //9
                CreateIndexItem(9),
                CreateIndexItem(10),
                CreateIndexItem(11)
            };
            ButtonGridMatrix matrix = new ButtonGridMatrix();
            Assert.AreEqual(matrix.Dimension, 0);
            matrix.Items.Add(items[0]);
            Assert.AreEqual(matrix.Dimension, 1);
            TestMatrix(matrix);

            matrix.Items.Add(items[1]);
            Assert.AreEqual(matrix.Dimension, 2);
            TestMatrix(matrix);

            matrix.Items.Add(items[2]);
            Assert.AreEqual(matrix.Dimension, 2);
            TestMatrix(matrix);

            matrix.Items.Insert(1,items[3]);
            Assert.AreEqual(matrix.Dimension, 2);
            TestMatrix(matrix);

            matrix.Items.Add(items[4]);
            Assert.AreEqual(matrix.Dimension, 3);


            matrix.Items.Clear();
            Assert.AreEqual(matrix.Dimension,0);

            items.ForEach((x)=>matrix.Items.Add(x));

            /*
            0 1 2 3
            4 5 6 7
            8 9 10 11
            */
            Assert.AreEqual(matrix.Dimension,4);
            TestMatrix(matrix);


            matrix.Items.RemoveAt(11);
            Assert.AreEqual(matrix.Dimension, 4);
            TestMatrix(matrix);

            matrix.Items.RemoveAt(9);
            /*
            0 1 2 3
            4 5 6 7
            8 10
            */
            Assert.AreEqual(matrix.Dimension,4);
            TestMatrix(matrix);

            matrix.Items.Remove(items[8]);
            Assert.AreEqual(matrix.Dimension,3);
            TestMatrix(matrix);

            matrix.Items[3] = items[11];
            Assert.AreEqual(matrix.Dimension,3);
            TestMatrix(matrix);

            matrix.Items.Move(1,4);
            Assert.AreEqual(matrix.Dimension, 3);
            TestMatrix(matrix);

        }
    }
}
