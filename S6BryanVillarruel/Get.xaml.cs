using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace S6BryanVillarruel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Get : ContentPage
    {
        private const string Url = "http://192.168.100.26/moviles/post.php";//Ingresar IP
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<S6BryanVillarruel.Datos> _post;

        public Get()
        {
            InitializeComponent();
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            var content = await client.GetStringAsync(Url);
            List<S6BryanVillarruel.Datos> posts = JsonConvert.DeserializeObject<List<S6BryanVillarruel.Datos>>(content);
            _post = new ObservableCollection<S6BryanVillarruel.Datos>(posts);

            MyListView.ItemsSource = _post;
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Post());
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Put());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Delete());
        }
    }

}