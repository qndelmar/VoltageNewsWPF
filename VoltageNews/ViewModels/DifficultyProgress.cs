using System.Text.RegularExpressions;

namespace VoltageNews.ViewModels;

public class DifficultyProgress
{
    public string errorText { get; private set; }
    public int difficultyProgress { get; private set; }

    public DifficultyProgress(string errorText, int difficultyProgress)
    {
        this.errorText = errorText;
        this.difficultyProgress = difficultyProgress;
    }

    public static DifficultyProgress ValidatePassword(string password)
    {
        if (Regex.IsMatch(password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^_&*-]).{8,}$"))
        {
            return new DifficultyProgress("", 100);
        }
        if (Regex.IsMatch(password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^_&*-])"))
        {
            return new DifficultyProgress("Пароль должен быть больше 8 символов.", 70); ;
        }
        if (Regex.IsMatch(password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])"))
        {
            return new DifficultyProgress("Пароль должен быть больше 8 символов,\nи содержать специальный символ.", 40); ;
        }
        if (Regex.IsMatch(password, "^(?=.*?[a-z])(?=.*?[A-Z])|^(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[0-9])"))
        {

            return new DifficultyProgress("Пароль должен быть больше 8 символов,\nи содержать в себе цифру, минимум 1 латинскую маленькую\nи большую букву, и специальный символ.", 20); ;
        }
        if (Regex.IsMatch(password, "^(?=.*?[A-Z])|(?=.*?[a-z])|(?=.*?[0-9])|(?=.*?[#?!@$%^_&*-])"))
        {
            return new DifficultyProgress("Пароль должен быть больше 8 символов,\nи содержать в себе цифру, минимум 1 латинскую маленькую\nи большую букву, и специальный символ.", 10); ;
        }
        return new DifficultyProgress("Пароль должен быть больше 8 символов,\nи содержать в себе цифру, минимум 1 латинскую маленькую\nи большую букву, и специальный символ.", 0);
    }

}