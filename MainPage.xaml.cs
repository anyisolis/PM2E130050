namespace PM2E130050;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalcularEnvioClicked(object sender, EventArgs e)
    {
        // 1. Validar que la descripción no esté vacía
        if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            await DisplayAlert("Error de validación", "Por favor ingrese una descripción para el paquete.", "OK");
            return;
        }

        // 2. Validar que el peso sea numérico y mayor a cero
        if (!double.TryParse(txtPeso.Text, out double peso) || peso <= 0)
        {
            await DisplayAlert("Error de validación", "Por favor ingrese un peso válido mayor a cero (0).", "OK");
            return;
        }

        // 3. Validar que el usuario haya seleccionado una zona en el Picker
        if (pckDestino.SelectedIndex == -1)
        {
            await DisplayAlert("Error de validación", "Debe seleccionar una zona de destino en el listado.", "OK");
            return;
        }

        // 4. Definir tarifa base por kg según la zona seleccionada (Usando if-else tradicional sin errores)
        double tarifaBaseKg = 0.0;
        if (pckDestino.SelectedIndex == 0)
        {
            tarifaBaseKg = 50.00;   // Zona Local
        }
        else if (pckDestino.SelectedIndex == 1)
        {
            tarifaBaseKg = 85.00;   // Zona Nacional
        }
        else if (pckDestino.SelectedIndex == 2)
        {
            tarifaBaseKg = 150.00;  // Zona Internacional
        }

        // 5. Calcular recargo exprés (si el Switch está activado, suma 100.00)
        double recargoExpres = swtExpres.IsToggled ? 100.00 : 0.00;

        // 6. Fórmula: (Peso * Tarifa_de_Zona) + Recargo_Expres
        double costoTotal = (peso * tarifaBaseKg) + recargoExpres;

        // 7. Configurar colores de la tarjeta según el monto
        if (costoTotal < 300.00)
        {
            cardResultado.BackgroundColor = Color.FromArgb("#DCFCE7"); // Verde claro
        }
        else
        {
            cardResultado.BackgroundColor = Color.FromArgb("#E0F2FE"); // Azul/Gris claro
        }

        // Mostrar detalles en la tarjeta
        lblDetalle.Text = "Paquete: " + txtDescripcion.Text + "\nPeso: " + peso + " kg\nZona: " + pckDestino.SelectedItem;
        lblTotal.Text = "Total a pagar: L " + costoTotal.ToString("F2");

        // Hacer visible la tarjeta de resultados
        cardResultado.IsVisible = true;
    }
}
