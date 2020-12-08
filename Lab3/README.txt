
Класс OptionsManager загружает файлы конфигурации config.xml и appsettings.json из указанного в конструкторе класса пути. Если существуют оба этих файла, то по умолчанию
используется config.xml. Если ни один из файлов не найден, в директории исполняемого файла создается файл congig.xml со стандартными настройками (в директории AppDomain создаются директории source и target, а также файл Log.txt).
Класс OptionsManager реализует метод Options GetOptions<T>(), который возвращает настройки отдельных частей системы:

ArchiverOptions - содержит опцию CompressionLevel - степень сжатия файла (по умолчанию - normal)
EncryptorOptions - содержит опцию EnableEncryption - необходимость шифровать файл (по умолчанию - true)
LoggerOptions - содержит опцию LogPath - путь к файлу (по умолчанию - AppDomain/Log.txt), и опцию EnableLogging.
TrackerOptions - содержит опцию Filter - тип файлов для перемещения (по умолчанию - txt), опцию Path - директория для мониторинга, и опцию notifyFilters - тип события для перемещения файла (по умолчанию - LastWrite)
