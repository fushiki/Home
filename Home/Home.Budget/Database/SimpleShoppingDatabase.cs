using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Home.Budget.Database
{
    [Serializable]
    class SimpleShoppingDatabase : ShoppingDatabase
    {
        
        private List<Shop> _shops;
        private SortedList<DateTime, Shopping> _shoppingHistory;

        private ProductCategory _allCategory;

       

        public static SimpleShoppingDatabase Create()
        {
            SimpleShoppingDatabase db;
            if (!File.Exists(BudgetTopic.Get().DatabasePath))
            {
                db = new SimpleShoppingDatabase();
                DefaultInitialize(db);
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(BudgetTopic.Get().DatabasePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);
                db = (SimpleShoppingDatabase) formatter.Deserialize(stream);
                stream.Close();
            }

            return db;
        }

        private static void DefaultInitialize(SimpleShoppingDatabase db)
        {
            db._allCategory = new ProductCategory();
            db._shops = new List<Shop>();
            db._shoppingHistory = new SortedList<DateTime, Shopping>();
            /*
            */
            db._allCategory.SubCategories.Add(new ProductCategory() {Name = "Cat1"});
            db._allCategory.SubCategories[0].SubCategories.Add(new ProductCategory() { Name = "Cat11" });
            db._allCategory.SubCategories.Add(new ProductCategory() { Name = "Cat2" });
            db._allCategory.SubCategories[0].Products.Add(new Product() {Name="Prod1"});
            db._allCategory.SubCategories[1].Products.Add(new Product() {Name = "Prod2"});
            db._allCategory.SubCategories[0].SubCategories[0].Products.Add(new Product() {Name = "Prod3"});
            /*
            */
        }

        public override List<Shop> Shops { get {return _shops;} }
        public override ProductCategory AllCategory => _allCategory;
        public override SortedList<DateTime, Shopping> ShoppingHistory { get {return _shoppingHistory;} }

        public override void Save()
        {
            base.Save();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(BudgetTopic.Get().DatabasePath,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }
    }
}
