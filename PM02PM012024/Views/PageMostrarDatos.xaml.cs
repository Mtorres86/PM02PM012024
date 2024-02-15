namespace PM02PM012024.Views;

public partial class PageMostrarDatos : ContentPage
{
    Controllers.PersonaControllers PersonDB;
    public PageMostrarDatos(Controllers.PersonaControllers dbpath)
	{

		InitializeComponent();
		PersonDB = dbpath;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

        // Obtener los datos de la base de datos y asignarlos al origen de datos de tu interfaz de usuario
            var personas = await PersonDB.GetListPersons();

       listView.ItemsSource= personas;
    }
}
