using System.Runtime.InteropServices;
using System.Text;

namespace Shared;

public class IniManager
{
    //Конструктор, принимающий путь к INI-файлу
    public IniManager(string? aPath)
    {
        Path = aPath;
    }

    //Конструктор без аргументов (путь к INI-файлу нужно будет задать отдельно)
    public IniManager() : this("") { }

    //Возвращает значение из INI-файла (по указанным секции и ключу) 
    public string GetPrivateString(string aSection, string aKey)
    {
        //Для получения значения
        StringBuilder buffer = new StringBuilder(Size);

        //Получить значение в buffer
        GetPrivateString(aSection, aKey, null, buffer, Size, Path);

        //Вернуть полученное значение
        return buffer.ToString();
    }

    //Пишет значение в INI-файл (по указанным секции и ключу) 
    public void WritePrivateString(string aSection, string aKey, string aValue)
    {
        //Записать значение в INI-файл
        WritePrivateString(aSection, aKey, aValue, Path);
    }

    //Возвращает или устанавливает путь к INI файлу
    public string? Path { get; set; } = null;

    //Поля класса
    private const int Size = 1024; //Максимальный размер (для чтения значения из файла)

    //Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
    private static extern int GetPrivateString(string section, string key, string? def, StringBuilder buffer, int size, string? path);

    //Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
    private static extern int WritePrivateString(string section, string key, string str, string? path);
}