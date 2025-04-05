using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Web;
namespace ПОГОДА
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string APIKey = "b4e6f18d69ede354c82cee14d59cdb4a";

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", TBCity.Text, APIKey);
                try
                {
                    string json = web.DownloadString(url);

                    Weatherinfo.root Info = JsonConvert.DeserializeObject<Weatherinfo.root>(json);

                    List<Weatherinfo.weather> weather = Info.weather;
                    picIcon.ImageLocation = "https://openweathermap.org/img/w/" + weather[0].icon + ".png";
                    labCondition.Text = weather[0].Main;
                    labDetails.Text = weather[0].description;
                    labSunset.Text = convertDateTime(Info.sys.sunset).ToString();
                    labSunrise.Text = convertDateTime(Info.sys.sunrise).ToString();
                    labWindSpeed.Text = Info.wind.speed.ToString();
                    labPressure.Text = Info.main.pressure.ToString();
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
                DateTime convertDateTime(long millisec)
                {
                    DateTime day = new DateTime(2025, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
                    day = day.AddMilliseconds(millisec).ToLocalTime();
                    return day;
                }
            }
            
        }
    }
}
