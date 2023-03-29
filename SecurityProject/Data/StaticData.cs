namespace SecurityProject
{
    public static class StaticData
    {
        public static string? DefaultFileAESEncrypted { get; set; } = "C:\\Users\\sholv\\Documents\\Nieuwe map";
        public static string? DefaultFileAESDecrypted { get; set; } = "C:\\Users\\sholv\\Documents\\Nieuwe map";
        public static string? DefaultFileRSAEncrypted { get; set; } = "C:\\Users\\Dimi\\source\\repos\\EncryptionProject_DimaBelshin\\Layout\\Default Encrypted files RSA\\";
        public static string? DefaultFileRSADecrypted { get; set; } = "C:\\Users\\Dimi\\source\\repos\\EncryptionProject_DimaBelshin\\Layout\\Default Decrypted files RSA\\";
        public static string? DefaultFileToOpen { get; set; } = "C:\\Users\\Dimi\\source\\repos\\EncryptionProject_DimaBelshin\\Layout\\Default Files for encryption\\";
        public static string? DefaultAESKeys { get; set; }
        public static string? DefaultRSAKeys { get; set; } = "C:\\Users\\Dimi\\source\\repos\\EncryptionProject_DimaBelshin\\RSA\\RSA Keys Default\\";
        public static string? SelectedAESKey { get; set; } = "C:\\Users\\sholv\\Documents\\Nieuwe map";
        public static string? SelectedAESFile { get; set; } = "C:\\Users\\sholv\\Documents\\Nieuwe map";
    }
}
