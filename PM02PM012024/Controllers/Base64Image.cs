using System;
using System.Globalization;

namespace PM02PM012024.Controllers

    //esta clase nos sirve para convertir los bytes de una imagen que se ha guardado en nuestra BD a una imagen como tal para poder mostrarl 
{
    public class Base64Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource imageSource = null;
            if(value != null)
            {
                String Base64 = (String)value;
                byte[] fotobyte = System.Convert.FromBase64String(Base64);
                var stream = new MemoryStream(fotobyte);

                imageSource = ImageSource.FromStream(() => stream);
            }
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

