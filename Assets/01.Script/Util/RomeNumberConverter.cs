public static class RomeNumberConverter
{
    public static string[] Numbers = { "N", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX" };
    public static string GetRomeNumber(int number) => Numbers[number];
}
