using Program.scr.core.dbt;
using System.Drawing.Imaging;

namespace Program.scr.core
{
    public static class Core
    {
        public static DBT_Users ThisUser;


        public static Image Base64ToImage(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (var ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return null;
            }
        }

        public static string GetImageBase64FromFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Читаем файл в байты
                        byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);

                        // Преобразуем в Base64
                        string base64String = Convert.ToBase64String(imageBytes);

                        return base64String;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    return null; // Пользователь отменил выбор
                }
            }
        }

        public static void SaveImageFromBase64(string base64String, string defaultFileName)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                MessageBox.Show("Base64-строка изображения пуста.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Устанавливаем фильтр для типов файлов
                saveFileDialog.Filter = "Изображения (*.png)|*.png|*.jpg|*.jpeg|*.bmp|*.gif|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить изображение";
                saveFileDialog.FileName = defaultFileName; // Имя по умолчанию

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Преобразуем Base64 в Image
                        byte[] imageBytes = Convert.FromBase64String(base64String);
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            Image img = Image.FromStream(ms);

                            // Определяем формат по расширению файла
                            string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                            ImageFormat format = ImageFormat.Png; // По умолчанию

                            switch (extension)
                            {
                                case ".jpg":
                                case ".jpeg":
                                    format = ImageFormat.Jpeg;
                                    break;
                                case ".bmp":
                                    format = ImageFormat.Bmp;
                                    break;
                                case ".gif":
                                    format = ImageFormat.Gif;
                                    break;
                                case ".png":
                                    format = ImageFormat.Png;
                                    break;
                                default:
                                    // Если расширение неизвестно — сохраняем как PNG
                                    format = ImageFormat.Png;
                                    break;
                            }

                            // Сохраняем изображение
                            img.Save(saveFileDialog.FileName, format);

                            MessageBox.Show($"Изображение успешно сохранено в:\r\n{saveFileDialog.FileName}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
