using Weather.Services.AuthApi.Enums;

namespace Weather.Services.AuthApi.Dtos;

public class UpdateUserPersonalParamsDto
{
    public double Weight { get; set; }
    public double Age { get; set; }
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public Pressure Pressure { get; set; } = Pressure.None;
    public double AvgUpSystolicPressure { get; set; }
    public double AvgDownSystolicPressure { get; set; }
    public double AvgUpDialysticPressure { get; set; }
    public double AvgDonwDialysticPressure { get; set; }
}
