using System.Collections.Generic;

namespace Home.Core
{
    public class Source
    {
        private User _user;
        public User ActualUser { get { if (_user == null) throw new MissingUserException(); return _user; } }

        public Dictionary<string,Topic> Topics { get; private set; }



        static Source _instance;

        static Source()
        {
            _instance = new Source();
            

        }
        private Source()
        {
            //TODO
            //Hardcoded user
            _user = new User("RandomNr17", new Address("Poland", "Cracow", "Ogrodowe", Address.AddressType.Settlement, 2, 50));
            //Hardcoded databases
            Topics = new Dictionary<string, Topic>();
        }

        public void AddTopic(Topic topic)
        {

            Topics[topic.Name] = topic;
            topic.Initialize();
        }

        

        public static Source Get() { return _instance; }
    }
}
