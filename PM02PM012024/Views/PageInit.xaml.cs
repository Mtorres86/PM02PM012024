//tuve que importar esto para poder nombrar las vistas. 
using PM02PM012024.Views;

namespace PM02PM012024;

public partial class PageInit : ContentPage
{
	//se Crea una variable del tipo Personacontrollers
	Controllers.PersonaControllers PersonDB;

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
			Telefono = Telefono.Text

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
}
