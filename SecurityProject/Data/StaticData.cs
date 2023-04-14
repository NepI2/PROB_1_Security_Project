namespace SecurityProject
{
    public static class StaticData
    {
        public static string? DefaultFileEncrypted { get; set; }
        public static string? DefaultFileDecrypted { get; set; }
        public static string? DefaultFileToOpen { get; set; }
        public static string? DefaultAESKeys { get; set; }
        public static string? DefaultRSAKeys { get; set; }
        public static string? SelectedRSAKey { get; set; }
        public static string? SelectedAESKey { get; set; }
        public static string? SelectedAESCipher { get; set; }
        public static string? SelectedAESFile { get; set; }
    }
}