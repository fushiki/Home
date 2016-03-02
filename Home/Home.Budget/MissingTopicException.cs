using System;
using System.Runtime.Serialization;

namespace Home.Budget
{
    [Serializable]
    internal class MissingTopicException : Exception
    {
        public string TopicName { get; private set; }
        public MissingTopicException(string topicName)
        {
            TopicName = topicName;
        }

        public MissingTopicException(string topicName, string message) : base(message)
        {
            TopicName = topicName;
        }

        public MissingTopicException(string topicName,string message, Exception innerException) : base(message, innerException)
        {
            TopicName = topicName;
        }

        protected MissingTopicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}