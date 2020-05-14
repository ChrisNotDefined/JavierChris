using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExamenMascotasJavierChris.Converters
{
    public class DateToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Error calculando la edad";
            }

            DateTime fecha = DateTime.Parse(value.ToString());

            if (fecha > DateTime.Now)
            {
                return "Error calculando la edad";
            }
            else
            {
                // Calcular año: año actual - año de nacimiento
                int yearDif = DateTime.Today.Year - fecha.Year;
                // Restar mes actual menos mes de nacimiento
                int monthDif = DateTime.Today.Month - fecha.Month;
                //Restar los dias
                int dayDif = DateTime.Today.Day - fecha.Day;
                // Si esa resta es negativa, quitar un año a la diferencia
                if (monthDif == 0)
                {
                    if (dayDif < 0)
                    {
                        yearDif--;
                    }
                }
                else
                {
                    if (monthDif < 0)
                        yearDif--;
                }

                //7/07/2020 24/07/2000
                // 2020 - 2000 = 20;
                // 07 - 07 = 0
                // 7 - 24 = -17
                // (0 == 0) (true) => (-17 < 0 ) (true) 20 -- => 19;

                //07/08/2020 24/07/2000
                // ====> 20
                // 08 - 07 = 1
                // 7 - 24 = -17
                // (1 == 0) (false) => (1 < 0) (false) 20 => 20;

                //29/02/2020 24/07/2020
                // =====> 20
                // 02 - 07 = -5
                // 29 - 24 = 5
                // (-5 == 0) (false) => (-5 < 0) (true) 20 -- => 19;


                return (yearDif != 1) ? yearDif + " años" : yearDif + " año";

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
