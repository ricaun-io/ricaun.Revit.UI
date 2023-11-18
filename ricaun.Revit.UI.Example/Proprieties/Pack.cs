using System;
using System.Runtime.CompilerServices;

namespace ricaun.Revit.UI.Example.Proprieties
{
    public static class Pack
    {
        #region Private
        private static string Assembly => "UIFrameworkRes";
        private static string BaseUri => @"pack://application:,,,/{0};component/Ribbon/images/{1}.ico";
        #endregion
        public static string Icon([CallerMemberName] string name = null) => string.Format(BaseUri, Assembly, name.ToLower());
        public static string Revit => Icon();
        public static string Power => Icon("system_electrical_circuit_power_create");
        public static string Communication => Icon("system_electrical_circuit_communication_create");
        public static string Control => Icon("system_electrical_circuit_control_create");
        public static string Data => Icon("system_electrical_circuit_data_create");
        public static string Alarm => Icon("system_electrical_circuit_fire_alarm_create");
        public static string Nurce => Icon("system_electrical_circuit_nurse_call_create");
        public static string Security => Icon("system_electrical_circuit_security_create");
        public static string Telephone => Icon("system_electrical_circuit_telephone_create");
        public static string Switch => Icon("system_switch_create");
    }
}
