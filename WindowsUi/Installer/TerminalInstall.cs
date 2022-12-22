using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Constants.Windows;
using LibrarySettings;
using MessageGenerator.ClientPinInput;
using MessageGenerator.Pincode;
using MessageGenerator.Sign;
using NUnit.Framework;
using TestBase;
using Application = FlaUI.Core.Application;

namespace AWindowsUi.Installer
{
    [TestFixture , NUnit.Framework.Description("https://tkovacs-dev.github.io/nunit3viewer/# Установка Агента и Ввод пинкода(на крипто про 5 придется вручную ввести пин")]
    public class TerminalInstall : WsTestBase<TerminalInstall>
    {
        private Application _installer;

        [Test, Order(2)]
        public void TestUninstall_Master_Agent()
        {
            try
            {
                var deInstaller =
                    Application.Launch(InstallPathTuple.GetPathUninstallTerminalAgent().pathUninstalTerminalAgent);
                var procToKill = Process.GetProcessById(deInstaller.ProcessId);

                Logger.Info($"Удаление мастер агента - {deInstaller}");

                Thread.Sleep(15000);
                Console.WriteLine(procToKill.ExitTime);
            }
            catch (Win32Exception ave)
            {
                Logger.Info($"ошибка при удалении мастер агента - { ave.Message} ");
            }
            CheckProcessStatus.GetStatusAgent();
        }

        [Test, Order(1)]
        public void TestUninstall_Agent()
        {
            var filePath =
                Environment.ExpandEnvironmentVariables(InstallPathTuple.GetPathUninstallStandardAgent()
                    .pathUninstalStandartAgent);
            Logger.Info($"найти агент по пути  - {filePath}");

            _installer = Application.Launch(filePath);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            
            var procKiller = Process.GetProcessById(_installer.ProcessId);
            procKiller.Kill(true);
        }

        [Test, Order(3)]
        public void Test_Install_Silent_Mode()
        {
            var desktopPath = InstallPathTuple.GetPathTaxcomAgent().TaxComAgentinstallerPath;
            var installerPath = Path.Combine(desktopPath, "TaxcomAgentInstaller.exe");
            
            var typeInstall = InstallationFactory.Build();

            Logger.Info("Установка агента в тихом режиме");

            Process.Start(installerPath, typeInstall);

            Thread.Sleep(35000);
        }

        [Test, Order(5)]
        public async Task TestPinCodeWebSocket()
        {
            
            if (CheckTokenName.GetCardName() == null)
            {
                Logger.Info("проверка USB порта");
                Assert.Pass("Токена нет,вводить пин не надо!");
            }

            await WsAction(async wsAdapter =>
            {
                var processName = Process.GetProcessesByName(Ui.ProcessOnWindowsName);
                Application agentProcess = null;
                foreach (var process in processName)
                {
                    Logger.Info($"найти процесс агента -{process.Id}");

                    agentProcess = Application.Attach(process.Id);
                }

                if (agentProcess is null)
                    throw new Exception(
                        $"Не удалось найти процесс ТакскомАгента с именем [{Ui.ProcessOnWindowsName}]");

                var webSocketPin = ClientSidePinInpiut.Generate(true);
                Logger.Info($"Сгенерировать webSocketPin сокет запрос - {webSocketPin}");
                
                var result = await wsAdapter.SendMessageWithoutCloseSocket(webSocketPin);
                Logger.Info($"Результат сокета запроса - {result}");

                var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать MrSign сокет запрос - {mrSign}");

                var res = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);
                Logger.Info($"Результат сокета запроса - {res}");

                var webSocketPinPassword =
                    ClientPinInputOutput.Generate($"{SqlIteReaderSocketValues.GetLoginValues().TokenLogin}", false);
                Logger.Info($"Сгенерировать webSocketPinPassword сокет запрос - {webSocketPinPassword}");

                var res2 = await wsAdapter.SendMessageWithoutCloseSocket(webSocketPinPassword);
                Logger.Info($"Результат сокета запроса - {res2}");

                Assert.Multiple(() =>
                {
                    Assert.That(res2, Is.Not.Null);
                    Assert.That(res2, Is.Not.Empty);
                    Assert.That(res2, Does.Match("sign_content"));
                });
            });
            Assert.Pass("Пин код введен");
        }
    }
}
