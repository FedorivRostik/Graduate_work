namespace Weather.Services.NuclearApi.Dtos.Extensions;

public static class ResponseDtoExtensions
{
    public static ResponseDto SetResult(this ResponseDto responseDto, object obj)
    {
        responseDto.ResultObj = obj;
        return responseDto;
    }
}