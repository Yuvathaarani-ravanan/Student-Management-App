namespace StudentManagementApp.Helpers
{
    public static class OtpHelper
    {
        public static string GenerateOtp(int length = 6)
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, length).Select(_ => random.Next(0, 10).ToString()));
        }
    }
}
