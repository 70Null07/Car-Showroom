using System;
using System.IO;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace Car_Showroom
{

    public sealed partial class MainPage : Page
    {
        private readonly XmlDocument CarsXML;
        private readonly XmlNodeList CarsXMLList;

        public MainPage()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader(File.OpenRead("XMLFile1.xml"));

            string strXML = sr.ReadToEnd();

            CarsXML = new XmlDocument();
            CarsXML.LoadXml(strXML);

            CarsXMLList = CarsXML.GetElementsByTagName("Car");

            foreach (IXmlNode Car in CarsXMLList)
            {
                CarsList.Items.Add(Car.Attributes.GetNamedItem("Brand").InnerText + " " + Car.Attributes.GetNamedItem("Title").InnerText);
            }
        }

        private void CarsList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            object SelectedCar = CarsList.SelectedItem;
            Brand.Text = SelectedCar.ToString();

            foreach (IXmlNode Car in CarsXMLList)
            {
                if (SelectedCar.ToString() == Car.Attributes.GetNamedItem("Brand").InnerText + " " + Car.Attributes.GetNamedItem("Title").InnerText)
                {
                    Brand.Text = Car.Attributes.GetNamedItem("Brand").InnerText;
                    Title.Text = Car.Attributes.GetNamedItem("Title").InnerText;
                    ProducingCountry.Text = Car.Attributes.GetNamedItem("ProducingCountry").InnerText;
                    Price.Text = Car.Attributes.GetNamedItem("Price").InnerText;

                    CarImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + Car.Attributes.GetNamedItem("Image").InnerText));
                }
            }

        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                string CarToFind = SearchCar.Text;

                foreach (IXmlNode Car in CarsXMLList)
                {
                    if ((Car.Attributes.GetNamedItem("Brand").InnerText + " " + Car.Attributes.GetNamedItem("Title").InnerText).Contains(CarToFind))
                    {
                        CarsList.SelectedIndex = -1;
                        Brand.Text = Car.Attributes.GetNamedItem("Brand").InnerText;
                        Title.Text = Car.Attributes.GetNamedItem("Title").InnerText;
                        ProducingCountry.Text = Car.Attributes.GetNamedItem("ProducingCountry").InnerText;
                        Price.Text = Car.Attributes.GetNamedItem("Price").InnerText;

                        CarImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + Car.Attributes.GetNamedItem("Image").InnerText));
                    }
                }
            }
        }
    }
}
