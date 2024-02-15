//tuve que importar esto para poder nombrar las vistas. 
using PM02PM012024.Views;

namespace PM02PM012024;

public partial class PageInit : ContentPage
{
    //objetos globales.
	//se Crea una variable del tipo Personacontrollers
	Controllers.PersonaControllers PersonDB;
    FileResult photo;

    //defino un constructor que recibe parametros de BD
   public PageInit(Controllers.PersonaControllers dbpath)
    {
       
      InitializeComponent();
		PersonDB = dbpath;
    }

    public PageInit()
	{
		InitializeComponent();
        //Instancia de clase para acceder a cualquier metodo de persona controller. 
        PersonDB = new Controllers.PersonaControllers();
       
    }

    //creamosuna funcion para convertir la imagen a 64 y oderla almacenar
    //esta la llamamos al momento de guardar el elemento
    public String GetImage64()
    {
        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                //toma la imagen y kla convierte en 74bytes
                byte[] data = ms.ToArray();

                String Base64 = Convert.ToBase64String(data);
                //retorna el valor de la magen que es la que almacena.
                return Base64;

            }
        }
        return null;
    }

    //Creamos una funcion para comnvertir la imagen a arreglo data 
    public byte[] GetImageArray()
    {
        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();

                return data;

            }
        }
        return null;
    }

    async void btn_procesar_Clicked(System.Object sender, System.EventArgs e)
    {
        //Iniciar la base de datos
        //await PersonDB.Init();

        //hacemos uina instancia, esta nos permite ver y trabajar con los objetos de la clase persona.
        var person = new Models.Personas
		{
            //nombre que esta en la clase = nombre que esta en el pageinti.xaml.cs

            //Id debe ir en cero cuando se va a ingresar algo nuevo.
            //Id = 0,
            Nombres = nombres.Text,
            Apellidos = apellidos.Text,
			FechaNac = FechaNac.Date,
			Telefono = Telefono.Text,
            //aqui llamamos la funcion y se la pasamos a la variable foto de nuestro modelo.
            Foto = GetImage64()

        };
        try
        {
            
            if (PersonDB != null)
           
            {
                if (await PersonDB.StorePerson(person) > 0)
                {
                    
                    await DisplayAlert("Aviso", "Registro Almacenado", "OK");
                    //mando a llamar la vista donde tengo los eqtos para mostrar
                   await Navigation.PushAsync(new PageMostrarDatos(PersonDB));

                }
            }
            else
            {
                // Manejar la situación donde PersonDB es null
                await DisplayAlert("Error", $"El controlador de base de datos no está inicializado.", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que ocurra durante el almacenamiento
            await DisplayAlert("Error", $"Ocurrió un error al almacenar el registro: {ex.Message}", "OK");
        }


    }

    async void btn_boton_Clicked(System.Object sender, System.EventArgs e)
    {
        photo = await MediaPicker.CapturePhotoAsync();
        if (photo != null)
        {
            string path = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourcephoto = await photo.OpenReadAsync();
            using FileStream Streamlocal = File.OpenWrite(path);

            //mostrar la imagen en el obejto y lo guardamos en nuestro sistema de archivos de sisema operativo
            foto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);

            await sourcephoto.CopyToAsync(Streamlocal);
        }
    }
}
