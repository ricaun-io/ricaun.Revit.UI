using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ricaun.Revit.UI.Example.Proprieties
{
    public static class Pack
    {
        #region Private
        private static string Assembly => "UIFrameworkRes";
        private static string BaseUri => @"pack://application:,,,/{0};component/Ribbon/images/{1}.ico";
        #endregion
        public static BitmapSource Icon([CallerMemberName] string name = null) => string.Format(BaseUri, Assembly, name.ToLower()).GetBitmapSource();
        public static BitmapSource Power => Icon("system_electrical_circuit_power_create");
        public static BitmapSource Communication => Icon("system_electrical_circuit_communication_create");
        public static BitmapSource Control => Icon("system_electrical_circuit_control_create");
        public static BitmapSource Data => Icon("system_electrical_circuit_data_create");
        public static BitmapSource Alarm => Icon("system_electrical_circuit_fire_alarm_create");
        public static BitmapSource Nurce => Icon("system_electrical_circuit_nurse_call_create");
        public static BitmapSource Security => Icon("system_electrical_circuit_security_create");
        public static BitmapSource Telephone => Icon("system_electrical_circuit_telephone_create");

    }
}
