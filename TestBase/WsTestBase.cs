using System;
using System.Threading.Tasks;
using Constants;
using NLog;
using NUnit.Framework;
using WsAdapter.Interface;

namespace TestBase
{
    public abstract class WsTestBase<TClass> where TClass : class
    {
        protected ILogger Logger { get; private set; }

        [SetUp]
        public void Setup()
        {
            Logger = LogManager.GetLogger(typeof(TClass).ToString());
        }


        protected async Task WsAction(Func<IWsAdapter, Task> action,string WssAddress)
        {
            //IWsAdapter wsAdapter = new WsAdapter.WsAdapter(CommonInfo.AgentMainAddress);
            IWsAdapter wsAdapter = new WsAdapter.WsAdapter(WssAddress);
            try
            {
                await wsAdapter.Open();
                await action(wsAdapter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }
            finally
            {
                wsAdapter.Close();
            }
        }
    }

}