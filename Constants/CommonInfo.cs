using System;

namespace Constants
{
    public static class CommonInfo
    {
        public static string AgentMainAddress => "wss://127.0.0.1:4823/commandWS";
        public static string WebSocketLocalHost => "wss://localhost:4920";
        public static string RestApiAgentRequestVersion => "api/v1/agent/version";
        public static string RestApiPing => "https://localhost:4923/ping";
        public static string WssApiPortAgent => "wss://localhost:4920";
        public static string WssApiAgentTerminal => "wss://localhost:4923";
        public static string WssApi => "wss://localhost:";

        public static string LocalHostAddress = "https://localhost:";

        public static int PortAgent = 4920;

        public static int PortTerminalAgent = 4923;
        public static TimeSpan TimeOfMaxWaitWs => TimeSpan.FromMinutes(2);
    }
}