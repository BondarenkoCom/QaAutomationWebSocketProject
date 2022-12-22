using NLog;
using NUnit.Framework;

namespace TestBase
{
    public class LogBase<TClass> where TClass : class
    {
        protected ILogger Logger { get; private set; }

        [SetUp]
        public void Setup()
        {
            Logger = LogManager.GetLogger(typeof(TClass).ToString());
        }
    }
}
