namespace FinalExam.View;

public partial class Question1 : ContentPage
{
	public Question1()
	{
		InitializeComponent();
	}

    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        double value = args.NewValue;
        label1.Text = value.ToString("F0");
        label2.Text = GetResultText(value);
    }
    private string GetResultText(double value)
    {
        if (value >= 0 && value <= 39)
        {
            return "Failed";
        }
        else if (value >= 40 && value <= 79)
        {
            return "Passed";
        }
        else
        {
            return "Excellent";
        }
    }
}