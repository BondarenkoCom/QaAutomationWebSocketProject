namespace LibrarySettings
{
    public static class ConstantsForMatch
    {
        public const string StoreName = "MY";
        public const string BeginPatternContent = "MI";
        public const string StatusResponseText = "OK";
        public const int StatusResponseCode = 200;
        public const string VersionPattern = "version";
        public const bool CheckChain = false;
        public const bool CheckCrl = false;
        public const int ResultTokenInit = 1;
        public const string CertResult = "{\"certificates\":";
        public const string ResultTokenInitFull = "{\"result\":1}";
        public const string ResultPatternSign = "MII";
        public const string ResultNameSign = "sign_content";
        public const string ResultNameSignSecond = "sign_content\":2";
        public const string ForMatchNameRecip = "recipients";
        public const string ResultPatternSignName = "{\"sign_content\":";
        public const string ResultNameRecipients = "recipients";
        public const string ResultNameUnpack = "unpack_data";
        public const string ResultVerfSoapCount = "{\"sgnsCount\":";
        public const string ResultVerfSoapBool = "\"signOK\":true";
        public const string DecryptContentForMatch = "dec_content";
        public const string AnswerTrueForMatch = "True";
        public const string ResultMrDecryptForMatch = "{\"sgnsCount\":1,\"isSignOK\":true,\"sgnsInfo\":";
        public static readonly string XmlSigners = $"{{\"xml_signers\":[{{\"thumb\":\"{SqlIteReaderSocketValues.GetLoginValues().TokenThumb}\",\"id\":\"REGNO_0000000013\",\"actor\":\"http://eln.fss.ru/actor/accountant/0000000013\"}}]}}";
    }
}
