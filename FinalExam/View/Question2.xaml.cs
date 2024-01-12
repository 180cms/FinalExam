namespace FinalExam.View;
using System.Text.RegularExpressions;

public partial class Question2 : ContentPage
{
	public Question2()
	{
		InitializeComponent();
        // Attach event handlers
        PhoneEntry.TextChanged += OnPhoneEntryTextChanged;
        PasswordEntry.TextChanged += OnPasswordEntryTextChanged;
        TermsAndConditionsCheckbox.CheckedChanged += OnCheckboxCheckedChanged;
        RegisterButton.Clicked += OnRegisterButtonClicked;
    }

    private void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Check if PhoneEntry is not null before accessing its properties
            if (PhoneEntry != null)
            {
                // Validate the phone number (simple check for numeric characters)
                Regex phoneRegex = new Regex(@"^(011\d{8}|010\d{7}|012\d{7}|013\d{7}|014\d{7}|015\d{7}|016\d{7}|017\d{7})|018\d{7}|019\d{7}$");

                bool isValidPhoneNumber = phoneRegex.IsMatch(e.NewTextValue);

                // Update the visibility of the corresponding label
                InvalidPhoneNumberLabel.IsVisible = !isValidPhoneNumber;

                // Enable/disable the RegisterButton based on validation status
                RegisterButton.IsEnabled = isValidPhoneNumber && IsPasswordValid() && TermsAndConditionsCheckbox.IsChecked;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in OnPhoneEntryTextChanged: {ex}");
        }

    }

    private void OnPasswordEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Check if PhoneEntry is not null before accessing its properties
            if (PasswordEntry != null)
            {
                // Validate the password length
                bool isPasswordValid = e.NewTextValue.Length > 5;

                // Update the visibility of the corresponding label
                PasswordLengthLabel.IsVisible = !isPasswordValid;

                // Enable/disable the RegisterButton based on validation status
                RegisterButton.IsEnabled = IsPhoneValid() && isPasswordValid && TermsAndConditionsCheckbox.IsChecked;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in OnPasswordEntryTextChanged: {ex}");
        }
    }

    private void OnCheckboxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            // Check if TermsAndConditionsCheckbox is not null before accessing its properties
            if (TermsAndConditionsCheckbox != null)
            {
                RegisterButton.IsEnabled = IsPhoneValid() && IsPasswordValid() && e.Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in OnCheckboxCheckedChanged: {ex}");
        }
        // Enable/disable the RegisterButton based on validation status
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // Handle the registration logic here
    }

    private bool IsPhoneValid()
    {
        // Validate the phone number (simple check for numeric characters)
        return PhoneEntry.Text.All(char.IsDigit);
    }

    private bool IsPasswordValid()
    {
        // Validate the password length
        return PasswordEntry.Text.Length > 5;
    }
}