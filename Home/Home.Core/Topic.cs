namespace Home.Core
{
    public abstract class Topic
    {
        public abstract string Name { get; }
        public abstract void Initialize();
        public virtual void Finish() { }
    }
}
