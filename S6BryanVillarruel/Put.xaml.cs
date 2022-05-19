using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace S6BryanVillarruel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Put : ContentPage
    {
        private const string Url = "http://192.168.100.26/moviles/post.php";//Ingresar IP
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<S6BryanVillarruel.Datos> _post;

        public Put()
        {
            InitializeComponent();
        }

        private void btnModificar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                    DisplayAlert("Alerta", "Seleccione un registro.", "Ok");
                else
                {
                    WebClient cliente = new WebClient();

                    string parametros = "";
                    Datos data = new Datos();
                    data.codigo = int.Parse(txtCodigo.Text);

                    if (!string.IsNullOrEmpty(txtNombre.Text))
                        data.nombre = txtNombre.Text;

                    if (!string.IsNullOrEmpty(txtApellido.Text))
                        data.apellido = txtApellido.Text;

                    if (!string.IsNullOrEmpty(txtEdad.Text))
                        data.edad = int.Parse(txtEdad.Text);

                    var content = JsonConvert.SerializeObject(data);

                    parametros += "?codigo=" + txtCodigo.Text;

                    if (!string.IsNullOrEmpty(txtNombre.Text))
                        parametros += "&nombre=" + txtNombre.Text;

                    if (!string.IsNullOrEmpty(txtApellido.Text))
                        parametros += "&apellido=" + txtApellido.Text;

                    if (!string.IsNullOrEmpty(txtEdad.Text))
                        parametros += "&edad=" + txtEdad.Text;

                    var urlCompleta = new Uri(Url + parametros);

                    cliente.UploadString(urlCompleta, "PUT", content);

                    DisplayAlert("Alerta", "Actualizado correctamente.", "Ok");

                    txtCodigo.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtEdad.Text = "";
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Mensaje de alerta " + ex.Message, "Ok");
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Get());
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                int codigo = int.Parse(txtCodigo.Text.ToString());
                var content = await client.GetStringAsync(Url + "?codigo=" + codigo);
                content = "[" + content + "]";
                List<S6BryanVillarruel.Datos> posts = JsonConvert.DeserializeObject<List<S6BryanVillarruel.Datos>>(content);
                _post = new ObservableCollection<S6BryanVillarruel.Datos>(posts);

                if (_post.Count > 0)
                {
                    txtCodigo.IsReadOnly = true;
                    txtNombre.IsReadOnly = false;
                    txtApellido.IsReadOnly = false;
                    txtEdad.IsReadOnly = false;

                    Datos data = new Datos();

                    data = posts.FirstOrDefault();

                    txtNombre.Text = data.nombre.ToString();
                    txtApellido.Text = data.apellido.ToString();
                    txtEdad.Text = data.edad.ToString();
                }
            }
        }
    }
}