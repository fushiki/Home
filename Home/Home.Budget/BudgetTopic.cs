using Home.Budget.Database;
using Home.Core;

namespace Home.Budget
{
    public class BudgetTopic : Topic
    {
        private static string _budgetTopicName = "BudgetTopic";
        private ShoppingDatabase _database;

        public string DatabasePath { get; private set; }

        public ShoppingDatabase Database => _database;
        public override string Name => _budgetTopicName;

        public override void Initialize()
        {
            DatabasePath = "simple_database.txt";
            //database = new SimpleShoppingDatabase();
            //database.Save();
            //TODO
           
           _database = SimpleShoppingDatabase.Create();
            
        }
        public static BudgetTopic Get() {
            var topic = Source.Get().Topics[_budgetTopicName] as BudgetTopic;
            if (topic == null) throw new MissingTopicException("BudgetTopic");
            return topic;
        }
    }
}
